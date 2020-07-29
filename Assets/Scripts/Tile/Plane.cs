using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public Tile tile;
    public void Show(bool value) { this.gameObject.SetActive(value); }
}
