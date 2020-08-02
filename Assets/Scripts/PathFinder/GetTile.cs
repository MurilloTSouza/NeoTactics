using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTile
{
    public static Tile Get(Tile[,] grid, int xpos, int zpos)
    {
        try
        {
            Tile t = grid[xpos, zpos];
            return t;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    public static Tile GetUp(Tile[,] grid, Tile current)
    {
        return Get(grid, current.xpos, current.zpos+1);
    }
    public static Tile GetDown(Tile[,] grid, Tile current)
    {
        return Get(grid, current.xpos, current.zpos-1);
    }
    public static Tile GetRight(Tile[,] grid, Tile current)
    {
        return Get(grid, current.xpos+1, current.zpos);
    }
    public static Tile GetLeft(Tile[,] grid, Tile current)
    {
        return Get(grid, current.xpos-1, current.zpos);
    }
    public static List<Tile>GetAdjactents(Tile[,] grid, Tile current)
    {
        List<Tile> adjacents = new List<Tile>();

        Tile t = GetUp(grid, current);
        if (t != null) adjacents.Add(t);

        t = GetDown(grid, current);
        if (t != null) adjacents.Add(t);

        t = GetRight(grid, current);
        if (t != null) adjacents.Add(t);

        t = GetLeft(grid, current);
        if (t != null) adjacents.Add(t);

        return adjacents;
    }
    public static List<Tile> GetAdjactents(Tile[,] grid, int currentx, int currentz)
    {
        return GetAdjactents(grid, grid[currentx, currentz]);   
    }
}
