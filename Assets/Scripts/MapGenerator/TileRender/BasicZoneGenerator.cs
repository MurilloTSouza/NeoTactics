using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZoneGenerator : ZoneTileGenerator
{
    public Tile prefab;
    public int height;
    protected override GridInfo GeneratePrefabAndHeight(GridInfo info)
    {
        info.prefab = prefab;
        info.height = height;
        return info;
    }
}
