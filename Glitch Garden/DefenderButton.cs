using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderButton : MonoBehaviour {

    [SerializeField] Defender defenderPrefab;

    private void OnMouseDown() {
        var buttons = FindObjectsOfType<DefenderButton>();

        foreach(DefenderButton button in buttons) { // Causes the white color to toggle between the two
            button.GetComponent<SpriteRenderer>().color = new Color32(41, 41, 41, 255);
        }

        GetComponent<SpriteRenderer>().color = Color.white;
        FindObjectOfType<DefenderSpawner>().SetSelectedDefender(defenderPrefab);
    }
}
