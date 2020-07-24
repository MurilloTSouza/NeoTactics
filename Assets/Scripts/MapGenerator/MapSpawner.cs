﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Instantiate the tiles based on the
// GridInfo[,] Received from the IGridGenerator
public class MapSpawner : MonoBehaviour
{
    // map tiles instances
    public Tile[,] grid;

    public float scale = 1;
    public Vector3 origin = Vector3.zero;

    public int xsize;
    public int zsize;

    // sequence of generators to generate grid
    public GridGenerator[] generators;

    private void Awake()
    {
        GridInfo[,] fullGenerated = GenerateChain(new GridInfo[xsize, zsize]);
        grid = Spawn(fullGenerated);
    }

    private Tile[,] Spawn(GridInfo[,] gridInfos)
    {
        if(gridInfos == null) return null;

        xsize = gridInfos.GetLength(0);
        zsize = gridInfos.GetLength(1);

        // tile instances
        Tile[,] grid = new Tile[xsize, zsize]; 

        for(int x=0; x<xsize; x++)
        {
            for(int z=0; z<zsize; z++)
            {
                GridInfo info = gridInfos[x, z];

                if(info == null) { continue; } //if null skip iteration

                // position gains offset of origin
                // the y position must be divided by 2
                // since the height value is measured by 1 in the game
                // but in the world the y value of each tile is 0.5
                Vector3 position = new Vector3(
                    x * scale,
                    (float)info.height/2,
                    z * scale) + origin;

                Tile tile = Instantiate(info.prefab, position, Quaternion.identity, this.transform);
                tile.AdjustInfo(info); // used to set variables and spam columns
                grid[x, z] = tile;
            }
        }
        return grid;
    }

    private GridInfo[,] GenerateChain(GridInfo[,] grid)
    {
        if (grid == null) return null;

        foreach(IGridGenerator generator in generators)
        {
            generator.Generate(grid);
        }
        return grid; //check later if is not necessary, since is passed by reference
    }
}
