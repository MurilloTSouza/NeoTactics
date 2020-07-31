using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected static TurnManager manager;
    public UnitStats stats;

    public static void SetTurnManager(TurnManager tm) { manager = tm; }

    public abstract IEnumerator OnStartPhase();

}
