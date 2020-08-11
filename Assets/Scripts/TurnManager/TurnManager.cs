using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Grid grid;
    public Zone playerSpawn;
    public UnitTeam playerTeam;
    public UnitTeam enemyTeam;

    private List<UnitBattle> order = new List<UnitBattle>();

    public UIStats uiStats;
    public UIActions uiActions;

    public Text orderLog;

    void Start()
    {
        UnitBattle.SetTurnManager(this);
        StartCoroutine(PositionTeam());
    }

    private IEnumerator PositionTeam()
    {
        Debug.Log("TurnManager: PositionTeam");
        grid.ShowTiles(true, playerSpawn);

        // Temporary list to set in unit team after positioning completed
        List<UnitBattle> selections = new List<UnitBattle>();
        // Units Remaining toPosition
        Queue<UnitBattle> toPosition = new Queue<UnitBattle>(playerTeam.prefabs);

        Vector3 firstTilePosition = grid.GetTile(
            playerSpawn.xStart,
            playerSpawn.zStart).transform.position;

        // For each remaining units to position
        while (toPosition.Count > 0)
        {
            // Creating temporary instance of the unit to be later set in PlayerTeam.Units
            UnitBattle prefab = toPosition.Dequeue();
            UnitBattle current = Instantiate(
                prefab,
                firstTilePosition,
                Quaternion.identity,
                playerTeam.transform);

            // set this preview to position and wait to confirm with mouse click
            while (!Input.GetMouseButtonUp(0))
            {
                // Getting mouse position and cliping unit to tile
                Plane plane = MouseRaycast.GetPointerCollider<Plane>();
                if (plane != null)
                {
                    current.transform.position = plane.tile.transform.position;
                }
                yield return null;
            }

            // after mouse click, confirm position
            selections.Add(current);
            yield return null; // used to wait for click in another frame
        }

        // after all units positioned, set to PlayerTeam.Units
        // and use setunit to set unit xpos and zpos and target content
        playerTeam.units = selections;
        selections.ForEach(unit => {
            grid.SetUnit(
                (int) unit.transform.position.x,
                (int) unit.transform.position.z, unit); });

        grid.ShowTiles(false, playerSpawn);
        StartCoroutine(StartBattle());
    }

    private IEnumerator StartBattle()
    {
        Debug.Log("TurnManager: StartBattle");

        order.AddRange(playerTeam.units);
        order.AddRange(enemyTeam.units);
        UpdateOrderLog();

        yield return new WaitForSeconds(1f);
        
        StartCoroutine(StartPhase(NextFromOrder()));
    }

    // until now there is no need to be a coroutine
    private IEnumerator StartPhase(UnitBattle next)
    {
        Debug.Log("TurnManager: StartPhase ("+next+")");
        uiStats.SetStats(next.stats);

        StartCoroutine(next.OnStartPhase());

        yield return null;
    }

    private IEnumerator EndPhase(UnitBattle toEnqueue) {
        Debug.Log("TurnManager: EndPhase("+toEnqueue+")");
        yield return new WaitForSeconds(1f);
        StopAllCoroutines(); // just for precaution

        // enqueue next unit, if is not dead(null)
        if(toEnqueue != null){ Enqueue(toEnqueue); }
        StartCoroutine(StartPhase(NextFromOrder()));
    }

    public void EndTurn(UnitBattle toEnqueue) { StartCoroutine(EndPhase(toEnqueue)); }

    private void Enqueue(UnitBattle unit)
    {
        order.Add(unit);
        UpdateOrderLog();
    }
    private UnitBattle NextFromOrder() { return PopFromOrder(0); }
    public void UnitDied(UnitBattle dead)
    {
        RemoveFromOrder(dead);
        grid.RemoveUnit(dead);
    }

    private void RemoveFromOrder(UnitBattle toRemove)
    {
        order.Remove(toRemove);
        UpdateOrderLog();
    }
    private UnitBattle PopFromOrder(int index)
    {
        UnitBattle unit = order[0];
        order.RemoveAt(0);
        UpdateOrderLog();
        return unit;
    }

    private void UpdateOrderLog()
    {
        string text = "";
        order.ToList().ForEach( unit => {
            text += unit.stats.nick+" - ";
        });
        orderLog.text = text;
    }

}
