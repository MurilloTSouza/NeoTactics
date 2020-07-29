using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : GridListener
{
    public UnitTeam enemyTeam;
    public Zone spawnZone;
    public Grid grid;

    public override void OnGridSpawn()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        List<Tile> list = grid.AsList(spawnZone);
        foreach(Unit unit in enemyTeam.units)
        {
            int index = Random.Range(0, list.Count);
            grid.SpawnContent(list[index], unit.gameObject, enemyTeam.transform);
            list.RemoveAt(index);
        }
    }

}
