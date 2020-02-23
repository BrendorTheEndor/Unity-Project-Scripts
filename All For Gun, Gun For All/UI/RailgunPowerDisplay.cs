using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RailgunPowerDisplay : MonoBehaviour {

    GameState gameState;
    Slider railgunUI;

    // Start is called before the first frame update
    void Start() {
        gameState = GameState.instanceOfGameState;
        railgunUI = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update() {
        railgunUI.value = (float)gameState.GetRailgunDamage() / gameState.GetMaxRailgunDamage();
    }
}
