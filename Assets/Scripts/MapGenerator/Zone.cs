using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int xStart;
    public int zStart;
    public int xEnd;
    public int zEnd;

    public void ForEach(Tile[,] tiles, Action<Tile> action)
    {
        for(int x=xStart; x<=xEnd; x++)
        {
            for (int z=zStart; z<=zEnd; z++)
            {
                action(tiles[x, z]);
            }
        }
    }
}
