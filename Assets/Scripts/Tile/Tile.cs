using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int xpos;
    public int zpos;
    public int height;

    // Set variables based on the info
    // also spam columns based on the height
    public virtual void AdjustInfo(GridInfo info)
    {
        xpos = info.xpos;
        zpos = info.zpos;
        height = info.height;

        TileRenderer renderer = GetComponent<TileRenderer>();
        if (renderer != null) renderer.Render(height);
    }

}
