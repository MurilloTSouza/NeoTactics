using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileRenderer : TileRenderer
{
    public MeshRenderer[] planes;
    public MeshRenderer[] sides;

    public override void Render(int height)
    {
        Render(
            planes[Random.Range(0, planes.Length)],
            sides[Random.Range(0, sides.Length)],
            height);
    }
}
