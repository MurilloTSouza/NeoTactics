using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Render Tiles bases on quantity
public abstract class TileGridGenerator : GridGenerator
{
    public int minquant;
    public int maxquant;
    public bool fillRemaining;

    public override GridInfo[,] Generate(GridInfo[,] grid)
    {
        // setting the quantity of remaining tiles to fill with prefabs
        List<GridInfo> remaining = YetToFill(grid);
        int quantToFill = Random.Range(minquant, maxquant);
        if (quantToFill > remaining.Count) quantToFill = remaining.Count;
        if (fillRemaining) quantToFill = remaining.Count;

        for(int i=0; i<quantToFill; i++)
        {

            // getting and removing random tile from the remaining tiles to fill
            int index = Random.Range(0, remaining.Count);
            GridInfo toFill = remaining[index];
            remaining.Remove(toFill);

            // filling tile with prefab
            grid[toFill.xpos, toFill.zpos] = GeneratePrefabAndHeight(grid[toFill.xpos, toFill.zpos]);
        }

        return grid;
    }

    protected abstract GridInfo GeneratePrefabAndHeight(GridInfo info);
}
