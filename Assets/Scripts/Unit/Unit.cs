using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [ReadOnly] public int xpos;
    [ReadOnly] public int zpos;

    public UnitStats stats;

    protected static TurnManager manager;

    public static void SetTurnManager(TurnManager tm) { manager = tm; }

    public abstract IEnumerator OnStartPhase();

    protected List<Path> ShowPath()
    {
        List<Path> paths = PathFinder.Find(
            manager.grid.GetTile(xpos, zpos),
            stats.move,
            manager.grid.grid);

        paths.ForEach(path => { path.tile.ShowPlane(true); });
        return paths;
    }

    protected void HidePath(List<Path> paths)
    {
        paths.ForEach(path => { path.tile.ShowPlane(false); });
    }

}
