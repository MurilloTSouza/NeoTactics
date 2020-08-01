using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public override IEnumerator OnStartPhase() {
        manager.uiActions.ShowAll(true);
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

    public void OnMoveClicked() { StartCoroutine(MovingPhase()); }
}
