using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour {

    TextMeshProUGUI healthDisplay;
    Player player;

    // Start is called before the first frame update
    void Start() {
        healthDisplay = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update() {
        healthDisplay.text = player.getHealth().ToString();
    }
}
