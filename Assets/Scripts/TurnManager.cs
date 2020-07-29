using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Grid grid;
    public Zone playerSpawn;
    public UnitTeam playerTeam;

    public Text log;

    void Start()
    {
        StartCoroutine(PositionTeam());
    }

    private IEnumerator PositionTeam()
    {
        log.text = "Position Team";
        grid.ShowTiles(true, playerSpawn);

        // Temporary list to set in unit team after positioning completed
        List<Unit> selections = new List<Unit>();
        // Units Remaining toPosition
        Queue<Unit> toPosition = new Queue<Unit>(playerTeam.prefabs);

        // For each remaining units to position
        while (toPosition.Count > 0)
        {
            // Creating temporary instance of the unit to be later set in PlayerTeam.Units
            Unit prefab = toPosition.Dequeue();
            Unit current = Instantiate(
                prefab,
                playerTeam.transform.position,
                Quaternion.identity,
                playerTeam.transform);

            // set this preview to position and wait to confirm with mouse click
            while (!Input.GetMouseButtonUp(0))
            {
                // Getting mouse position and cliping unit to tile
                Ray ray = ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
        playerTeam.units = selections;

        grid.ShowTiles(false, playerSpawn);
        StartCoroutine(StartPhase());
    }

    private IEnumerator StartPhase()
    {
        log.text = "Start Phase";
        yield return null;
    }
}
