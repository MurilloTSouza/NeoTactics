using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Basic generator used for test
public class BasicTileGridGenerator : TileGridGenerator
{
    public Tile[] prefabs;

    public int minHeight;
    public int maxHeight;

    protected override GridInfo GeneratePrefabAndHeight(GridInfo info)
    {
        info.prefab = prefabs[Random.Range(0, prefabs.Length)];
        info.height = Random.Range(minHeight, maxHeight);
        return info;
    }
}
