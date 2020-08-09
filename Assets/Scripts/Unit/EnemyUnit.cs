using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : UnitBattle
{
    public override IEnumerator OnStartPhase()
    {
        StartCoroutine(MovingPhase());
        yield return null;
    }

    private IEnumerator MovingPhase()
    {
        List<Path> paths = ShowPath();

        Tile target = ChoosePath(paths);
        yield return new WaitForSeconds(2f); // Thinking simulation
        manager.grid.MoveUnitTo(this, target);

        HidePath(paths);
        StartCoroutine(AttackPhase());
    }

    private IEnumerator AttackPhase()
    {
        List<Plane> attackRegion = ShowAttackRegion(meleeAttack);
        UnitBattle target = null;

        // Check if there is a unit in attack region
        foreach (Plane p in attackRegion)
        {
            if(p.tile.content != null)
            {
                UnitBattle test = p.tile.content.GetComponent<UnitBattle>();
                if(test != null) { target = test; break; }
            }
        }

        yield return new WaitForSeconds(2f); // Thinking simulation

        if (target != null) { StartCoroutine(Attack(target, meleeAttack)); }
        HideRegion(attackRegion);

        SendEndTurn(this);
    }

    private Tile ChoosePath(List<Path> paths)
    {
        Tile target = null;

        // For each available path...
        foreach(Path path in paths)
        {
            // ... get all adjcents tile...
            List<Tile> adjacents = GetTile.GetAdjactents(manager.grid.grid, path.tile);
            foreach(Tile tile in adjacents)
            {
                // ... and check if has a content
                if(tile.content != null)
                {
                    // ... if it has, check if is a player unit...
                    PlayerUnit pu = tile.content.GetComponent<PlayerUnit>();
                    if (pu != null) {target = path.tile; break; } // ...if yes, set it to target
                }
            }
        }

        // if no target found yet, set it to the last target;
        if(target == null) { target = paths[paths.Count - 1].tile; }

        return target;
    }
}
