using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Instantiate the tiles based on the
// GridInfo[,] Received from the IGridGenerator
public class MapSpawner : MonoBehaviour
{
    public Tile[,] grid; // map tiles instances

    public BasicGridGenerator generator; // strategy to generate

    public float scale = 1;
    public Vector3 origin = Vector3.zero;

    public int xsize; // don't set it, public just to inspector
    public int zsize; // don't set it, public just to inspector

    private void Awake()
    {
        grid = Spawn(generator);
    }

    private Tile[,] Spawn(IGridGenerator generator)
    {
        // result from generate
        GridInfo[,] infos = generator.Generate(); 

        xsize = infos.GetLength(0);
        zsize = infos.GetLength(1);

        // tile instances
        Tile[,] grid = new Tile[xsize, zsize]; 

        for(int x=0; x<xsize; x++)
        {
            for(int z=0; z<zsize; z++)
            {
                GridInfo info = infos[x, z];

                // position gains offset of origin
                // the y position must be divided by 2
                // since the height value is measured by 1 in the game
                // but in the world the y value of each tile is 0.5
                Vector3 position = new Vector3(
                    x * scale,
                    (float)info.height/2,
                    z * scale) + origin;

                Tile tile = Instantiate(info.prefab, position, Quaternion.identity);
                tile.AdjustInfo(info); // used to set variables and spam columns
                grid[x, z] = tile;
            }
        }
        return grid;
    }
}
