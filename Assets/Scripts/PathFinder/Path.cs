using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Path
{
    public int distance;
    public Tile tile;
    public Path previous;
    public Path(int distance, Tile tile, Path previous)
    {
        this.distance = distance;
        this.tile = tile;
        this.previous = previous;
    }
}