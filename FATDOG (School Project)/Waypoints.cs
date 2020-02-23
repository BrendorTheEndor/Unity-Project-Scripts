//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

// waypoints are like markers that the enemies look for to set their path of travel

public class Waypoints : MonoBehaviour
{

    public static Transform[] waypoints;

    private void Awake()
    {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }

}
