using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackRegion
{
    public abstract List<Plane> ShowRegion(int currentx, int currentz, Tile[,] grid);
    public abstract int CalculateDamage(UnitBattle attacker, UnitBattle target);
}
