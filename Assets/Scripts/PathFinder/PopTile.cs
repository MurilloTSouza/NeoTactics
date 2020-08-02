using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopTile
{
    public static Tile Pop(Tile[,] grid, int xpos, int zpos)
    {
        try
        {
            Tile t = grid[xpos, zpos];
            grid[xpos, zpos] = null;
            return t;
        }
        catch (Exception e)
        {
            // Debug.Log(e);
            return null;
        }
    }
    public static Tile PopUp(Tile[,] grid, Tile tile)
    {
        return Pop(grid, tile.xpos, tile.zpos + 1);
    }
    public static Tile PopDown(Tile[,] grid, Tile tile)
    {
        return Pop(grid, tile.xpos, tile.zpos - 1);
    }
    public static Tile PopRight(Tile[,] grid, Tile tile)
    {
        return Pop(grid, tile.xpos + 1, tile.zpos);
    }
    public static Tile PopLeft(Tile[,] grid, Tile tile)
    {
        return Pop(grid, tile.xpos - 1, tile.zpos);
    }
    public static List<Tile> PopAdjacents(Tile[,] grid, Tile tile)
    {
        List<Tile> adjacents = new List<Tile>();

        Tile t = PopUp(grid, tile);
        if (t != null) adjacents.Add(t);

        t = PopDown(grid, tile);
        if (t != null) adjacents.Add(t);

        t = PopRight(grid, tile);
        if (t != null) adjacents.Add(t);

        t = PopLeft(grid, tile);
        if (t != null) adjacents.Add(t);

        return adjacents;
    }

    // --- with jump ---
    public static Tile Pop(Tile[,] grid, int xpos, int zpos, int jump, int currentHeight)
    {
        try
        {
            Tile t = grid[xpos, zpos];
            if (currentHeight+jump>=t.height && //if is not too high
                currentHeight-jump<=t.height) //if is not too low
            {
                
                grid[xpos, zpos] = null;
                return t;
            }
            else { return null; }
        }
        catch (Exception e)
        {
            //Debug.Log(e);
            return null;
        }
    }
    public static Tile PopUp(Tile[,] grid, Tile tile, int jump)
    {
        return Pop(grid, tile.xpos, tile.zpos + 1, jump, tile.height);
    }
    public static Tile PopDown(Tile[,] grid, Tile tile, int jump)
    {
        return Pop(grid, tile.xpos, tile.zpos - 1, jump, tile.height);
    }
    public static Tile PopRight(Tile[,] grid, Tile tile, int jump)
    {
        return Pop(grid, tile.xpos + 1, tile.zpos, jump, tile.height);
    }
    public static Tile PopLeft(Tile[,] grid, Tile tile, int jump)
    {
        return Pop(grid, tile.xpos - 1, tile.zpos, jump, tile.height);
    }
    public static List<Tile> PopAdjacents(Tile[,] grid, Tile tile, int jump)
    {
        List<Tile> adjacents = new List<Tile>();

        Tile t = PopUp(grid, tile, jump);
        if (t != null) adjacents.Add(t);

        t = PopDown(grid, tile, jump);
        if (t != null) adjacents.Add(t);

        t = PopRight(grid, tile, jump);
        if (t != null) adjacents.Add(t);

        t = PopLeft(grid, tile, jump);
        if (t != null) adjacents.Add(t);

        return adjacents;
    }
}
