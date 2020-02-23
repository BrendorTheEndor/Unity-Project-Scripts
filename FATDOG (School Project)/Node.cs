using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    // set up node attributes
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    private Renderer rend;
    private Color startColor;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    public Vector3 positionOffset;
    BuildManager buildManager;

    // upon game start
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    //helper function to get position of where you want to build turret
    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }

    // if mouse clicks on node
    private void OnMouseDown() {
        ClickBehavior();
    }

    // this is called from clicks or hovering over node if hover is turned on
    public void ClickBehavior() {

        // node contains a turret, so don't place a turret
        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        // if no tower is selected, don't do anything
        if(!buildManager.CanBuild) {
            return;
        }
        BuildTurret(buildManager.GetTurretToBuild());

        // node contains a turret, so don't place a turret
        if(turret != null) {
            return;
        }

        // If no turret selected, do nothing
        if(!buildManager.CanBuild) {
            return;
        }

        BuildTurret(buildManager.turretToBuild);

    }

    // place a turret on a tile
    void BuildTurret (TurretBlueprint blueprint)
    {

        // don't let player place turret if they don't have enough money
        if (PlayerStats.Money < blueprint.cost)
        {
            return;
        }

        // subtract turret cost from player's money
        PlayerStats.Money -= blueprint.cost;

        // place the turret
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

    }

    // upgrade the turret when the upgrade button is activated
    public void UpgradeTurret()
    {

        // don't let the player upgrade turret if they don't have enough money
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            return;
        }

        // subtract turret upgrade cost from player's money
        PlayerStats.Money -= turretBlueprint.upgradeCost;

        //Get rid of the old turret
        Destroy(turret);

        //Build a new turret
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

    }

    // sell the turret when the sell button is activated
    public void SellTurret()
    {

        // add half of the turret's cost to the player's money
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        //spawn an effect and destroy turret
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;

    }

    // mouse is hovering over a node
    private void OnMouseEnter()
    {

        GetComponent<Renderer>().enabled = true;
        GetComponent<Renderer>().material.color = hoverColor;

        // do nothing if there is a button on top of the node (like the turret button)
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // if no tower is selected, don't perform hover animation
        if (!buildManager.CanBuild)
        {
            return;
        }

        // if player has money, change node color from starting color to hoverColor
        // otherwise, change it to notEnoughMoneyColor
        if (buildManager.HasMoney)
        {
            GetComponent<Renderer>().material.color = hoverColor;
        }
        else
        {
            GetComponent<Renderer>().material.color = notEnoughMoneyColor;
        }
        

    }

    // mouse stops hovering over a node
    private void OnMouseExit()
    {

        // reset node color to whatever it was originally
        rend.material.color = startColor;
        GetComponent<Renderer>().enabled = false;

    }

}
