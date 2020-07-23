using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileRenderer : MonoBehaviour
{
    public const float tileHeight = 0.5f;
    public abstract void Render(int height);
    protected void Render(MeshRenderer plane, MeshRenderer side, int height)
    {
        Instantiate(plane, this.transform);
        Instantiate(side, this.transform);
        SpamHeight(height, side);
    }

    private void SpamHeight(int height, MeshRenderer side)
    {
        Vector3 lastPosition = this.transform.position;

        // number of tiles to spam is equal the height-1
        // ex: i=2, i=1, height = 3
        for (int i = 0; i < height; i++)
        {
            lastPosition -= new Vector3(0, tileHeight, 0);
            Instantiate(
                side, //prefab
                lastPosition, //position
                this.transform.rotation, //rotation
                this.transform); //parent
        }
    }
}
