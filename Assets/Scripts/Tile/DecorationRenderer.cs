using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecorationRenderer : MonoBehaviour
{
    public MeshRenderer Render(MeshRenderer decoration, Quaternion rotation)
    {
        return Instantiate(
            decoration, //prefab
            this.transform.position, //position
            rotation, //rotation
            this.transform); //parent
    }
}
