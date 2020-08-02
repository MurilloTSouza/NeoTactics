using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int xsize;
    public int zsize;
    public Tile[,] grid;

    public MapSpawner mapSpawner;

    public GridListener[] inspectorListeners;
    private List<GridListener> listeners;

    private void Awake() {
        // Setting listeners
        listeners = new List<GridListener>(inspectorListeners);

        // Spawning and calling listeners
        grid = mapSpawner.GenerateAndSpawn(xsize, zsize, this.transform);
        listeners.ForEach(l => l.OnGridSpawn());
    }

    //Instantiate content at the grid[x,z] position and return it
    public GameObject SpawnContent(Tile tile, GameObject prefab, Transform parent)
    {
        GameObject content = Instantiate(
            prefab,
            tile.transform.position,
            Quaternion.identity,
            parent);

        tile.content = content;
        return content;
    }
    public GameObject SpawnContent(int xpos, int zpos, GameObject prefab, Transform parent)
    {
        return SpawnContent(grid[xpos, zpos], prefab, parent);
    }

    // Same as SpawnContent, but set unit xpos and zpos at the end
    public UnitBattle SpawnUnit(Tile tile, UnitBattle prefab, Transform parent)
    {
        GameObject instance = SpawnContent(tile, prefab.gameObject, parent);
        UnitBattle unit = instance.GetComponent<UnitBattle>();
        unit.xpos = tile.xpos;
        unit.zpos = tile.zpos;
        return unit;
    }
    public UnitBattle SpawnUnit(int xpos, int zpos, UnitBattle prefab, Transform parent)
    {
        return SpawnUnit(grid[xpos, zpos], prefab, parent);
    }

    public void SetContent(Tile target, GameObject content) { target.content = content; }

    public void SetUnit(Tile target, UnitBattle unit)
    {
        target.content = unit.gameObject;
        unit.transform.position = target.transform.position;
        unit.xpos = target.xpos;
        unit.zpos = target.zpos;
    }
    public void SetUnit(int xpos, int zpos, UnitBattle unit) { SetUnit(grid[xpos, zpos], unit); }

    public void MoveContentTo(GameObject content, Tile start, Tile end)
    {
        start.content = null;
        end.content = content;
        content.transform.position = end.transform.position;
        end.name = "END";
        Debug.Log(end.content);
    }

    public void MoveUnitTo(UnitBattle unit, Tile tile) {
        // transfering content from start to end
        Tile start = grid[unit.xpos, unit.zpos];
        MoveContentTo(unit.gameObject, start, tile);
        // changing current unit xpos and zpos
        unit.xpos = tile.xpos;
        unit.zpos = tile.zpos;
    }
    public void MoveUnitTo(UnitBattle unit, int xpos, int zpos)
    {
        MoveUnitTo(unit, grid[xpos, zpos]);
    }

    public void ShowTiles(bool value, Zone zone)
    {
        zone.ForEach(grid, ( tile => { tile.ShowPlane(value); }));
    }

    public Tile GetTile(int xpos, int zpos)
    {
        return grid[xpos, zpos];
    }

    public List<Tile> AsList(Zone zone)
    {
        List<Tile> list = new List<Tile>();
        for(int x=zone.xStart; x<=zone.xEnd; x++)
        {
            for(int z=zone.zStart; z<=zone.zEnd; z++)
            {
                Tile t = grid[x, z];
                if(t != null) { list.Add(t); }
            }
        }
        return list;
    }

    public List<Tile> OnlyWalkables()
    {
        List<Tile> availables = new List<Tile>();
        for (int x=0; x<=xsize; x++)
        {
            for (int z=0; z <= zsize; z++)
            {
                Tile t = grid[x, z];
                if (t != null || t.IsWalkable()) { availables.Add(t); }
            }
        }
        return availables;
    }

    public void Subscribe(GridListener listener) { listeners.Add(listener); }

    public List<Path> FindPath(int xpos, int zpos, int distance, int jump)
    {
        return PathFinder.Find(grid[xpos, zpos], distance, jump, grid);
    }
}

public abstract class GridListener : MonoBehaviour
{
    public abstract void OnGridSpawn();
}
