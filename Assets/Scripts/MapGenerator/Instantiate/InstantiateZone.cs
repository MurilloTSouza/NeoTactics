using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateZone : GridGenerator
{
    public Zone zone;

    public override GridInfo[,] Generate(GridInfo[,] grid)
    {
        for(int x=zone.xStart; x<=zone.xEnd; x++)
        {
            for(int z=zone.zStart; z<=zone.zEnd; z++)
            {
                grid[x, z] = GridInfo.Builder().WithXPos(x).WithZPos(z).Build();
            }
        }
        return grid;
    }
}
