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

    private Queue<Unit> order;

    public UIStats uiStats;
    public UIActions uiActions;

    public Text phaseLog;
    public Text orderLog;

    void Start()
    {
        Unit.SetTurnManager(this);
        StartCoroutine(PositionTeam());
    }

    private IEnumerator PositionTeam()
    {
        phaseLog.text = "Position Team";
        grid.ShowTiles(true, playerSpawn);

        // Temporary list to set in unit team after positioning completed
        List<Unit> selections = new List<Unit>();
        // Units Remaining toPosition
        Queue<Unit> toPosition = new Queue<Unit>(playerTeam.prefabs);

        Vector3 firstTilePosition = grid.GetTile(
            playerSpawn.xStart,
            playerSpawn.zStart).transform.position;

        // For each remaining units to position
        while (toPosition.Count > 0)
        {
            // Creating temporary instance of the unit to be later set in PlayerTeam.Units
            Unit prefab = toPosition.Dequeue();
            Unit current = Instantiate(
                prefab,
                firstTilePosition,
                Quaternion.identity,
                playerTeam.transform);

            // set this preview to position and wait to confirm with mouse click
            while (!Input.GetMouseButtonUp(0))
            {
                // Getting mouse position and cliping unit to tile
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) //Probably need to change to RayCastAll
                {
                    Plane plane = hit.collider.gameObject.GetComponent<Plane>();
                    if (plane != null)
                    {
                        current.transform.position = plane.tile.transform.position;
                    }
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
        phaseLog.text = "Start Battle";

        // setting turn manager on each unit
        List<Unit> allUnits = new List<Unit>();
        allUnits.AddRange(playerTeam.units);
        allUnits.AddRange(enemyTeam.units);

        // setting order queue
        order = new Queue<Unit>(
            allUnits.OrderBy( u=> u.stats.speed ));

        UpdateOrderLog();

        yield return new WaitForSeconds(1f);
        StartCoroutine(StartPhase(order.Dequeue()));
    }

    // until now there is no need to be a coroutine
    private IEnumerator StartPhase(Unit unit)
    {
        phaseLog.text = "Start Phase";
        uiStats.SetStats(unit.stats);
        UpdateOrderLog();

        StartCoroutine(unit.OnStartPhase());

        yield return null;
    }

    private IEnumerator EndPhase() { yield return new WaitForSeconds(1f); }

    public void EndTurn() { StartCoroutine(EndPhase()); }

    private void UpdateOrderLog()
    {
        string text = "";
        order.ToList().ForEach( unit => {
            text += unit.stats.nick+" - ";
        });
        orderLog.text = text;
    }

}
