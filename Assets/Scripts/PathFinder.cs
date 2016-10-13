using UnityEngine;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour
{
    public Vector2 pivotOffset;
    public float moveSpeed = 50.0f;
    public float moveThreshold = 1;

    public Vector3 Direction { get; private set; }

    private int currentPosX;
    private int currentPosY;

    Rect mapRect;

    private int gridWidth;
    private int gridHeight;

    private Animator anim = null;
    private int velXId = 0;
    private int velYId = 0;

    private enum State
    {
        NoPath = 0,
        HeadingFirst,
        InPath,
        Reached
    }

    State state = State.NoPath;
    int currentPathNode = -1;
    float targetPosX = 0.0f;
    float targetPosY = 0.0f;

    // Use this for initialization
    void Start()
    {
        gridWidth = MapManager.TileMap.TileWidth;
        gridHeight = MapManager.TileMap.TileHeight;

        mapRect = MapManager.TileMap.GetMapRect();
        
        currentPosX = Mathf.FloorToInt((transform.position.x + pivotOffset.x - mapRect.xMin) / gridWidth);
        currentPosY = Mathf.FloorToInt((-transform.position.y - pivotOffset.y - mapRect.yMax) / gridHeight);

        anim = GetComponent<Animator>();
        velXId = Animator.StringToHash("velX");
        velYId = Animator.StringToHash("velY");

        Direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosX = Mathf.FloorToInt((transform.position.x + pivotOffset.x - mapRect.xMin) / gridWidth);
        currentPosY = Mathf.FloorToInt((-transform.position.y - pivotOffset.y - mapRect.yMax) / gridHeight);

        switch (state)
        {
            case State.Reached:
                state = State.NoPath;
                currentPathNode = -1;
                Direction = Vector3.zero;
                break;
            case State.HeadingFirst:
                targetPosX = pathX[currentPathNode] * gridWidth + gridWidth * 0.5f;
                targetPosY = -pathY[currentPathNode] * gridHeight - gridHeight * 0.5f;
                state = State.InPath;
                Direction = Vector3.zero;
                break;
            case State.InPath:
                if (Mathf.Abs(targetPosX - transform.position.x) < moveThreshold && Mathf.Abs(targetPosY - transform.position.y) < moveThreshold)
                {
                    if (currentPathNode == 0)
                    {
                        state = State.Reached;
                    }
                    else
                    {
                        currentPathNode--;
                        targetPosX = pathX[currentPathNode] * gridWidth + gridWidth * 0.5f;
                        targetPosY = -pathY[currentPathNode] * gridHeight - gridHeight * 0.5f;
                    }
                    Direction = Vector3.zero;
                }
                else if (Mathf.Abs(targetPosX - transform.position.x) > moveThreshold)
                {
                    if (targetPosX < transform.position.x)
                    {
                        Direction = new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
                    }
                    else
                    {
                        Direction = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                    }
                }
                else
                {
                    if (targetPosY < transform.position.y)
                    {
                        Direction = new Vector3(0, -moveSpeed * Time.deltaTime, 0);
                    }
                    else
                    {
                        Direction = new Vector3(0, moveSpeed * Time.deltaTime, 0);
                    }
                }
                transform.Translate(Direction);
                break;
            case State.NoPath:
            default:
                Direction = Vector3.zero;
                break;
        }

        if (null != anim)
        {
            anim.SetFloat(velXId, Direction.x * 100.0f);
            anim.SetFloat(velYId, Direction.y * 100.0f);
        }
    }

    #region A*

    private List<int> pathX = new List<int>();
    private List<int> pathY = new List<int>();

    uint HashCoord(int x, int y)
    {
        uint ret = (uint)x;
        ret <<= 16;
        ret |= ((uint)y & 0xffffu);
        return ret;
    }

    void UnhashCoord(uint hash, out int x, out int y)
    {
        x = (int)((hash >> 16) & 0xffff);
        y = (int)(hash & 0xffff);
    }

    static readonly int[] deltaX = { 1, 0, -1, 0 };
    static readonly int[] deltaY = { 0, 1, 0, -1 };

    public void FindPath(float x, float y)
    {
        int ix = Mathf.FloorToInt((x - mapRect.xMin) / gridWidth);
        int iy = Mathf.FloorToInt((-y - mapRect.yMax) / gridHeight);
        FindPath(ix, iy);
    }

    public void FindPath(int x, int y)
    {
        Dictionary<uint, int> gScore = new Dictionary<uint, int>();
        Dictionary<uint, int> fScore = new Dictionary<uint, int>();
        Dictionary<uint, uint> cameFrom = new Dictionary<uint, uint>();
        HashSet<uint> closedSet = new HashSet<uint>();
        HashSet<uint> openSet = new HashSet<uint>();

        uint startHash = HashCoord(currentPosX, currentPosY);

        openSet.Add(startHash);
        gScore[startHash] = 0;
        while (openSet.Count > 0)
        {
            uint curHash = 0;
            int minScore = int.MaxValue;

            // find the node with lowest score
            foreach (uint hash in openSet)
            {
                int s = int.MaxValue;
                if (fScore.ContainsKey(hash)) s = fScore[hash];

                if (s <= minScore)
                {
                    minScore = s;
                    curHash = hash;
                }
            }

            openSet.Remove(curHash);
            closedSet.Add(curHash);

            int curX = -1, curY = -1;
            UnhashCoord(curHash, out curX, out curY);

            if (curX == x && curY == y)
            {
                pathX.Clear();
                pathY.Clear();

                pathX.Add(x);
                pathY.Add(y);

                uint lastHash = HashCoord(x, y);
                while (cameFrom.ContainsKey(lastHash))
                {
                    uint nextHash = cameFrom[lastHash];
                    int nextX = -1, nextY = -1;
                    UnhashCoord(nextHash, out nextX, out nextY);
                    pathX.Add(nextX);
                    pathY.Add(nextY);
                    lastHash = nextHash;
                }

                state = State.HeadingFirst;
                currentPathNode = pathX.Count - 1;
                targetPosX = pathX[currentPathNode] * gridWidth + gridWidth * 0.5f;
                targetPosY = -pathY[currentPathNode] * gridHeight - gridHeight * 0.5f;
                state = State.InPath;
                Direction = Vector3.zero;
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                int nx = curX + deltaX[i];
                int ny = curY + deltaY[i];

                if (nx < 0 || nx >= MapManager.Instance.MapWidth || ny < 0 || ny >= MapManager.Instance.MapHeight || MapManager.Instance.Map[ny, nx] > 0)
                    continue;

                uint nHash = HashCoord(nx, ny);

                if (closedSet.Contains(nHash))
                {
                    continue;
                }

                int nScore = gScore[curHash] + 1;
                if (!openSet.Contains(nHash))
                {
                    openSet.Add(nHash);
                }
                else if (nScore >= gScore[nHash])
                {
                    continue;
                }

                cameFrom[nHash] = curHash;
                gScore[nHash] = nScore;
                fScore[nHash] = nScore + Mathf.Abs(curX - x) + Mathf.Abs(curY - y);
            }
        }

        state = State.NoPath;
        return;
    }

    #endregion

    //Rect rect = new Rect(0, 0, 200, 200);
    //void OnGUI()
    //{
    //    GUI.Label(rect, currentPosX + ", " + currentPosY);
    //}
}
