using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour {

    [SerializeField] int starCost = 100;
    [SerializeField] int starGain = 0;

    public void AddStars() {
        FindObjectOfType<StarDisplay>().AddStars(starGain);
    }

    public int GetStarCost() { return starCost; }

}
