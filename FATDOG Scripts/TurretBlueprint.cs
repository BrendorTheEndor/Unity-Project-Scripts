using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class provides a blueprint for turrets
// this allows upgrading and selling to occur

[System.Serializable]
public class TurretBlueprint
{

    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }

}
