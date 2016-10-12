using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour
{
    public static Tiled2Unity.TiledMap TileMap { get; private set; }
    public static MapManager Instance { get; private set; }
    public TextAsset gridMapData;

    public int MapWidth { get; private set; }
    public int MapHeight { get; private set; }

    public byte[,] Map { get; private set; }

    void Awake()
    {
        TileMap = GetComponent<Tiled2Unity.TiledMap>();

        if (gridMapData != null)
        {
            string[] lines = gridMapData.text.Replace('\r', '\n').Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length > 0)
            {
                string[] firstRow = lines[0].Split(',');
                MapWidth = firstRow.Length;
                MapHeight = lines.Length;

                Map = new byte[MapHeight, MapWidth];

                for (int i = 0; i < firstRow.Length; i++)
                {
                    Map[0, i] = byte.Parse(firstRow[i]);
                }

                for (int r = 1; r < MapHeight; r++)
                {
                    string[] line = lines[r].Split(',');
                    for (int c = 0; c < MapWidth; c++)
                    {
                        Map[r, c] = byte.Parse(line[c]);
                    }
                }
            }
        }

        Instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
