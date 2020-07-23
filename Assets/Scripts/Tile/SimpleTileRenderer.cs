using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTileRenderer : TileRenderer
{
    public MeshRenderer plane;
    public MeshRenderer side;

    public override void Render(int height)
    {
        Render(plane, side, height);
    }
}
