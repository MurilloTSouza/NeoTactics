using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDecorationRenderer : DecorationRenderer
{
    public MeshRenderer decoration;

    private void Awake()
    {
        Render(
            decoration,
            Quaternion.Euler(0, Random.Range(0, 4) * 90, 0)); //0, 90, 180, 270
    }
}
