using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : UnitBattle
{
    public override IEnumerator OnStartPhase() {
        manager.uiActions.SetUnit(this);
        manager.uiActions.ShowAll(true);
        manager.uiActions.EnableAll(true);
        manager.uiActions.SetUnit(this);
        yield return null;
    }

    public IEnumerator MovingPhase()
    {
        Debug.Log(stats.nick + " moving turn");
        manager.uiActions.SetMoveEnabled(false);
        List<Path> paths = ShowPath();
        
        // wait for player select plane
        Plane selected = null;
        while(selected == null)
        {
            while (!Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if( Physics.Raycast(ray, out hit))
                {
                    Plane plane = hit.collider.GetComponent<Plane>();
                    if(plane != null) { selected = plane; }
                }
                yield return null;
            }
            yield return null;
        }

        // move unit to selected and clear path
        manager.grid.MoveUnitTo(this, selected.tile);
        HidePath(paths);

        yield return null;
    }

    public IEnumerator AttackingPhase(AttackRegion attack)
    {
        Debug.Log(stats.nick + " attacking turn");
        manager.uiActions.SetAttackEnabled(false);
        List<Plane> attackRegion = ShowAttackRegion(attack);

        // wait for player select plane
        UnitBattle target = null;
        while (target == null)
        {
            while (!Input.GetMouseButtonUp(0))
            {
                // if clicked in a unit
                UnitBattle unit = MouseRaycast.GetPointerCollider<UnitBattle>();
                if(unit != null) { target = unit; }
                yield return null;
            }
            yield return null;
        }

        StartCoroutine(Attack(target, attack));
        HideRegion(attackRegion);

        yield return null;
    }

    public void OnMoveClicked() { StartCoroutine(MovingPhase()); }
    public void OnAttackClicked() { 
        StartCoroutine(AttackingPhase(meleeAttack));
    }
    public void OnEndTurnClicked() {
        manager.uiActions.ClearListeners();
        manager.uiActions.ShowAll(false);
        SendEndTurn(this);
    }
}
