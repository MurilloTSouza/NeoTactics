using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIActions : MonoBehaviour
{
    public Button moveButton;
    public Button attackButton;
    public Button abilityButton;
    public Button endTurnButton;

    private UnitBattle current;

    private void Awake() { ShowAll(false); }

    public void SetUnit(PlayerUnit unit)
    {
        ClearListeners();
        moveButton.onClick.AddListener(unit.OnMoveClicked);
        attackButton.onClick.AddListener(unit.OnAttackClicked);
        endTurnButton.onClick.AddListener(unit.OnEndTurnClicked);
    }

    public void ClearListeners()
    {
        moveButton.onClick.RemoveAllListeners();
        attackButton.onClick.RemoveAllListeners();
        endTurnButton.onClick.RemoveAllListeners();
    }

    private void SetListener(Button button, UnityAction call)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(call);
    }

    public void ShowAll(bool value)
    {
        ShowMove(value);
        ShowAttack(value);
        ShowAbility(value);
        ShowEndTurn(value);
    }

    public void EnableAll(bool value)
    {
        SetMoveEnabled(value);
        SetAttackEnabled(value);
        SetAbilityEnabled(value);
        SetEndTurnEnabled(value);
    }

    public void ShowMove(bool value) { moveButton.gameObject.SetActive(value); }
    public void ShowAttack(bool value) { attackButton.gameObject.SetActive(value); }
    public void ShowAbility(bool value) { abilityButton.gameObject.SetActive(value); }
    public void ShowEndTurn(bool value) { endTurnButton.gameObject.SetActive(value); }

    public void SetMoveEnabled(bool value) { moveButton.interactable = value; }
    public void SetAttackEnabled(bool value) { attackButton.interactable = value; }
    public void SetAbilityEnabled(bool value) { abilityButton.interactable = value; }
    public void SetEndTurnEnabled(bool value) { endTurnButton.interactable = value; }
}