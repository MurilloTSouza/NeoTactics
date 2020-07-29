using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Instantiate the tiles based on the
// GridInfo[,] Received from the IGridGenerator
public class MapSpawner : MonoBehaviour
{
    // map tiles instances
    public Grid grid;

    public float scale = 1;
    public Vector3 origin = Vector3.zero;

    // sequence of generators to generate grid
    public GridGenerator[] generators;

    private void Awake()
    {
        GridInfo[,] fullGenerated = GenerateChain(new GridInfo[grid.xsize, grid.zsize]);
        grid.grid = Spawn(fullGenerated);
    }

    private Tile[,] Spawn(GridInfo[,] gridInfos)
    {
        if(gridInfos == null) return null;

        // tile instances
        Tile[,] instances = new Tile[grid.xsize, grid.zsize]; 

        for(int x=0; x<grid.xsize; x++)
        {
            for(int z=0; z<grid.zsize; z++)
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

                Tile tile = Instantiate(info.prefab, position, Quaternion.identity, grid.transform);
                tile.AdjustInfo(info); // used to set variables and spam columns
                instances[x, z] = tile;
            }
        }
        return instances;
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
