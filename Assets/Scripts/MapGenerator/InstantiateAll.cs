using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAll : GridGenerator
{
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
        return grid;
    }
}
