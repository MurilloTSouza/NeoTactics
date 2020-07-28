using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateZone : GridGenerator
{
    public int xStart;
    public int zStart;
    public int xEnd;
    public int zEnd;

    public override GridInfo[,] Generate(GridInfo[,] grid)
    {
        for(int x=xStart; x<=xEnd; x++)
        {
            for(int z=zStart; z<=zEnd; z++)
            {
                grid[x, z] = GridInfo.Builder().WithXPos(x).WithZPos(z).Build();
            }
        }
        return grid;
    }
}
