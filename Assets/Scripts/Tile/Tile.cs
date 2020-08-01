using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [ReadOnly] public int xpos;
    [ReadOnly] public int zpos;
    [ReadOnly] public int height;
    [ReadOnly] public GameObject content;
    public Plane plane;

    private void Awake()
    {
         InstantiatePlane();
    }

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

    public void ShowPlane(bool value) { plane.Show(value); }

    private void InstantiatePlane()
    {
        Vector3 position = this.transform.position + new Vector3(0, 0.01f, 0);
        plane = Instantiate(plane, position, Quaternion.identity, this.transform);
        plane.tile = this;
        plane.Show(false);
    }

    public bool IsWalkable() { return content == null; }
}
