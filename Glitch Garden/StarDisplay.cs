using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour {

    [SerializeField] int stars = 100;
    Text starText;

    void Start() {
        starText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay() {
        starText.text = stars.ToString();
    }

    public void AddStars(int starsToAdd) {
        stars += starsToAdd;
        UpdateDisplay();
    }

    public void RemoveStars(int starsToRemove) {
        if(stars >= starsToRemove) {
            stars -= starsToRemove;
            UpdateDisplay();
        }
    }

    public bool HaveEnoughStars(int amount) {
        return stars >= amount;
    }
}
