using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour {

    const string DEFENDER_PARENT_NAME = "Defenders";

    Defender defender;
    GameObject defenderParent;

    private void Start() {
        CreateDefenderParent();
    }

    private void CreateDefenderParent() {
        defenderParent = GameObject.Find(DEFENDER_PARENT_NAME);

        if(!defenderParent) {
            defenderParent = new GameObject(DEFENDER_PARENT_NAME);
        }
    }

    private void OnMouseDown() {
        AttemptToPlaceDefenderAt(GetSquareClicked());
    }

    public void SetSelectedDefender(Defender defenderToSelect) {
        defender = defenderToSelect;
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos) {
        var starDisplay = FindObjectOfType<StarDisplay>();
        int defenderCost = defender.GetStarCost();

        if(starDisplay.HaveEnoughStars(defenderCost)) {
            spawnDefender(gridPos);
            starDisplay.RemoveStars(defenderCost);
        }
    }

    private Vector2 GetSquareClicked() {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        return SnapToGrid(worldPos);
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos) {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.Ceil(rawWorldPos.y);
        return new Vector2(newX, newY);
    }

    private void spawnDefender(Vector2 whereToSpawn) {
        Defender newDefender = Instantiate(defender, whereToSpawn, Quaternion.identity) as Defender;
        newDefender.transform.parent = defenderParent.transform;
    }
}
