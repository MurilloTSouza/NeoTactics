using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    public override IEnumerator OnStartPhase()
    {
        yield return new WaitForSeconds(1f);
    }
}
