using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [ReadOnly] public int xpos;
    [ReadOnly] public int zpos;

    public UnitStats stats;

    protected static TurnManager manager;

    public static void SetTurnManager(TurnManager tm) { manager = tm; }

    public abstract IEnumerator OnStartPhase();

    protected void ShowPath()
    {
        
    }

}
