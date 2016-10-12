using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour
{
    public static Tiled2Unity.TiledMap TileMap { get; private set; }

    void Awake()
    {
        TileMap = GetComponent<Tiled2Unity.TiledMap>();
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
