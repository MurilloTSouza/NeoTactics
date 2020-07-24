using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Structure used to return data to the MapSpawner
// so the MapSpawner use this variables to instantiate the grid of Tiles
public class GridInfo
{
    public Tile prefab;
    public int xpos;
    public int zpos;
    public int height;

    public static GridInfoBuilder Builder() { return new GridInfoBuilder(); }
}

public class GridInfoBuilder
{
    private GridInfo info;
    
    public GridInfoBuilder()
    {
        Reset();
    }
    public void Reset() { info = new GridInfo(); }
    public GridInfoBuilder WithPrefab(Tile prefab) { info.prefab = prefab; return this; }
    public GridInfoBuilder WithXPos(int xpos) { info.xpos = xpos; return this; }
    public GridInfoBuilder WithZPos(int zpos) { info.zpos = zpos; return this; }
    public GridInfoBuilder WithHeight(int height) { info.height = height; return this; }
    public GridInfo Build() { return info; }
}
