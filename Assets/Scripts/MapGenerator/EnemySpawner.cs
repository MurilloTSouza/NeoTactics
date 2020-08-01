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
        List<Tile> availablePositions = grid.AsList(spawnZone);
        foreach(Unit prefab in enemyTeam.prefabs)
        {
            // Instantiating enemy
            int index = Random.Range(0, availablePositions.Count);
            Unit enemy = grid.SpawnUnit(
                availablePositions[index],
                prefab,
                enemyTeam.transform);

            // Adding enemy to EnemyTeam.Units
            if(enemy != null) { enemyTeam.units.Add(enemy); }

            availablePositions.RemoveAt(index);
        }
    }

}
