using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDecorationRenderer : DecorationRenderer
{
    public MeshRenderer[] decorations;
    public int frequency = 100;

    private void Awake()
    {
        // if 100 always render, if 0 never render
        bool mustRender = frequency < Random.Range(0, 100);

        if (mustRender) {
            Render(
               decorations[Random.Range(0, decorations.Length)],
               Quaternion.Euler(0, Random.Range(0, 4) * 90, 0)); //0, 90, 180, 270
        }
           
    }
}
