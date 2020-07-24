using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateRange : GridGenerator
{
    public int min;
    public int max;

    // Generating all tiles and then removing
    public override GridInfo[,] Generate(GridInfo[,] grid)
    {
        int xsize = grid.GetLength(0);
        int zsize = grid.GetLength(1);
        for (int x = 0; x < xsize; x++)
        {
            for (int z = 0; z < zsize; z++)
            {
                grid[x, z] = GridInfo.Builder().WithXPos(x).WithZPos(z).Build();
            }
        }
        return RemoveRandom(grid);
    }

    private GridInfo[,] RemoveRandom(GridInfo[,] grid)
    {
        List<GridInfo> remaining = AsList(grid); // list of all tiles

        int total = grid.GetLength(0) * grid.GetLength(1);
        int toRemove = total - Random.Range(min, max);  // random number of tiles to remove

        for (int i=0; i<toRemove; i++)
        {
            int randomIndex = Random.Range(0, remaining.Count);
            GridInfo info = remaining[randomIndex]; // random tile of remaining tiles
            remaining.RemoveAt(randomIndex); // remove random tile from the remaining tiles
            grid[info.xpos, info.zpos] = null; // removing info from grid
        }

        return grid;
    }
}
