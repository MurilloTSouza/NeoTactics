using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Basic generator used for test
public class BasicGridGenerator : MonoBehaviour, IGridGenerator
{
    public Tile[] prefabs;

    public int xsize;
    public int zsize;

    public int minHeight;
    public int maxHeight;

    public GridInfo[,] Generate()
    {
        GridInfo[,] grid = new GridInfo[xsize, zsize];

        for(int x=0; x<xsize; x++)
        {
            for(int z=0; z<zsize; z++)
            {
                // The prefab model and height are purely random
                GridInfoBuilder builder = new GridInfoBuilder();
                GridInfo info = builder
                    .WithPrefab(prefabs[Random.Range(0, prefabs.Length)])
                    .WithXPos(x).WithZPos(z)
                    .WithHeight(Random.Range(minHeight, maxHeight))
                    .Build();
                grid[x, z] = info;
            }
        }
        return grid;
    }
}
