using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridGenerator : MonoBehaviour, IGridGenerator
{
    public abstract GridInfo[,] Generate(GridInfo[,] grid);

    protected List<GridInfo> AsList(GridInfo[,] grid)
    {
        List<GridInfo> list = new List<GridInfo>();
        foreach(GridInfo info in grid)
        {
            if(info != null) list.Add(info);
        }
        return list;
    }

    protected List<GridInfo> YetToFill(GridInfo[,] grid)
    {
        List<GridInfo> list = new List<GridInfo>();
        foreach (GridInfo info in grid)
        {
            if (info != null && info.prefab == null) list.Add(info);
        }
        return list;
    }
}
