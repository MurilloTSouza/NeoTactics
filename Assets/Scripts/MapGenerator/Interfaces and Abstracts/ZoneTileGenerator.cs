using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Iterate in a zone and passes responsability to implementing class
public abstract class ZoneTileGenerator : GridGenerator
{
    public int xStart;
    public int zStart;
    public int xEnd;
    public int zEnd;

    public override GridInfo[,] Generate(GridInfo[,] grid)
    {
        for (int x = xStart; x <= xEnd; x++)
        {
            for (int z = zStart; z <= zEnd; z++)
            {
                GridInfo info = grid[x, z];
                if (info != null) GeneratePrefabAndHeight(info);
            }
        }
        return grid;
    }

    protected abstract GridInfo GeneratePrefabAndHeight(GridInfo info);
}
