using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface IGridGenerator
{
    GridInfo[,] Generate(GridInfo[,] grid);
}
