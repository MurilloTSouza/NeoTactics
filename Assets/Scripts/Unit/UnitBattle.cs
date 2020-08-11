using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBattle : MonoBehaviour
{
    public static AttackRegion meleeAttack = new MeleeAttackRegion();

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
            stats.move, stats.jump,
            manager.grid.grid);

        paths.ForEach(path => { path.tile.ShowPlane(true); });
        return paths;
    }

    protected void HidePath(List<Path> paths)
    {
        paths.ForEach(path => { path.tile.ShowPlane(false); });
    }

    protected List<Plane> ShowAttackRegion(AttackRegion attack) {
        return attack.ShowRegion(xpos, zpos, manager.grid.grid);
    }
    protected void HideRegion(List<Plane> plane) { plane.ForEach(p => p.Show(false)); }

    public IEnumerator Attack(UnitBattle target, AttackRegion attack)
    {
        StartCoroutine(target.ReceiveDamage(attack.CalculateDamage(this, target)));
        yield return null;
    }

    public IEnumerator ReceiveDamage(int damage)
    {
        Debug.Log(this + " received " + damage + " damage");
        stats.hp -= damage;

        // if is dead
        if (stats.hp <= 0) {
            stats.hp = 0; // round to 0
            Debug.Log("UnitBattle: " + this + " died");

            StartCoroutine(Die());
        }
        yield return null;
    }

    public void SendEndTurn(UnitBattle toEnqueue)
    {
        Debug.Log("UnitBattle: EndingTurn("+toEnqueue+")");
        if (toEnqueue.stats.hp > 0) { manager.EndTurn(toEnqueue); } // is alive
        else { manager.EndTurn(null); } // is dead
    }

    private IEnumerator Die()
    {
        manager.UnitDied(this);

        yield return new WaitForSeconds(1f); //Death Animation

        Destroy(this.gameObject);
    }
}
