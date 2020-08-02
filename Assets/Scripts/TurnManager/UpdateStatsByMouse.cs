using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStatsByMouse : MonoBehaviour
{
    public UIStats uiStats;

    private void Update()
    {
        UnitStats stats = MouseRaycast.GetPointerCollider<UnitStats>();
        if (stats != null)
        {
            uiStats.SetStats(stats);
        }
    }
}
