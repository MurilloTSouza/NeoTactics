using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Iterate in a zone and passes responsability to implementing class
public abstract class ZoneTileGenerator : GridGenerator
{
    public Zone zone;

    public override GridInfo[,] Generate(GridInfo[,] grid)
    {
        for (int x = zone.xStart; x <= zone.xEnd; x++)
        {
            for (int z = zone.zStart; z <= zone.zEnd; z++)
            {
                GridInfo info = grid[x, z];
                if (info != null) GeneratePrefabAndHeight(info);
            }
        }
        return grid;
    }

    protected abstract GridInfo GeneratePrefabAndHeight(GridInfo info);
}
