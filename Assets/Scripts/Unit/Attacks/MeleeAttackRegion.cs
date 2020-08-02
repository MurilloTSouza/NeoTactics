using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackRegion : AttackRegion
{
    public override List<Plane> ShowRegion(int currentx, int currentz, Tile[,] grid)
    {
        List<Plane> region = new List<Plane>();
        GetTile.GetAdjactents(grid, currentx, currentz)
            .ForEach(t => { region.Add(t.plane); });
        region.ForEach(plane => plane.Show(true));
        return region;
    }

    public override int CalculateDamage(UnitBattle attacker, UnitBattle target)
    {
        int damage = attacker.stats.atk - target.stats.def;
        if(damage < 0) { damage = 0; }
        return damage;
    }
}
