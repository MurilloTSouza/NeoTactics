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
        listeners = new List<GridListener>(inspectorListeners);

        grid = mapSpawner.GenerateAndSpawn(xsize, zsize, this.transform);
        listeners.ForEach(l => l.OnGridSpawn());
    }

    //Instantiate content at the grid[x,z] position and return it
    public GameObject SpawnContent(int xpos, int zpos, GameObject prefab, Transform parent)
    {
        Tile tile = grid[xpos, zpos];
        GameObject content = Instantiate(
            prefab, 
            tile.transform.position, 
            Quaternion.identity,
            parent);

        tile.content = content;
        return content;
    }
    public GameObject SpawnContent(Tile tile, GameObject prefab, Transform parent)
    {
        return SpawnContent(tile.xpos, tile.zpos, prefab, parent);
    }

    public void SetContent(Tile target, GameObject content) { target.content = content; }

    public void MoveContentTo(Tile start, Tile end)
    {
        end.content = start.content;
        start.content = null;
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

    public void Subscribe(GridListener listener) { listeners.Add(listener); }
}

public abstract class GridListener : MonoBehaviour
{
    public abstract void OnGridSpawn();
}
