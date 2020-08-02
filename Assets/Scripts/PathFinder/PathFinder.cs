using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public static List<Path> Find(Tile start, int distance, int jump, Tile[,] grid)
    {
        List<Path> paths = new List<Path>();

        int xsize = grid.GetLength(0);
        int zsize = grid.GetLength(1);
        Tile[,] available = FilterWalkables(grid);

        //Initializing queue with adjacents tiles
        List<Tile> adjacents = PopTile.PopAdjacents(available, start, jump);
        List<Path> firstPaths = new List<Path>();
        foreach (Tile t in adjacents)
        {
            firstPaths.Add(new Path(1, t, null));
        }
        Queue<Path> queue = new Queue<Path>(firstPaths);

        // while still has paths to iterate at the queue
        while (queue.Count > 0)
        {
            // getting and adding path to the result list
            Path path = queue.Dequeue();
            paths.Add(path);

            // if is not the end path
            if (path.distance < distance){
                
                // getting adjacents paths to be added to the queue
                List<Tile> newAdjacents = PopTile.PopAdjacents(available, path.tile, jump);
                foreach (Tile t in newAdjacents)
                {
                    Path adj = new Path(path.distance + 1, t, path);
                    queue.Enqueue(adj);
                }
            }
        }
        return paths;
    }

    private static Tile[,] FilterWalkables(Tile[,] grid)
    {
        int xsize = grid.GetLength(0);
        int zsize = grid.GetLength(1);
        Tile[,] available = new Tile[xsize, zsize];

        for (int x = 0; x < xsize; x++)
        {
            for (int z = 0; z < zsize; z++)
            {
                if (grid[x, z] != null && grid[x, z].IsWalkable())
                    available[x, z] = grid[x, z];
            }
        }

        return available;
    }
}
