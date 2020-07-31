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
        yield return null;
    }

    public void OnMoveClicked() { StartCoroutine(MovingPhase()); }
}
