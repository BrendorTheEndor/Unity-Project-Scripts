using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    // set up BuildManager attributes
    public static BuildManager instance;
    public TurretBlueprint turretToBuild;
    private Node selectedNode;
    public GameObject buildEffect;
    public NodeUI nodeUI;
    public GameObject sellEffect;
    public GameObject turretPanel;
    public GameObject misslePanel;
    public GameObject laserPanel;

    void Awake()
    {

        if(instance != null)
        {
            return;
        }

        instance = this;

        turretToBuild = null;

    }

    // check if we have a turret selected
    public bool CanBuild { get { return turretToBuild != null; } }

    // check if we have enough money to place the turret we have selected
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    

    // selecting a node
    public void SelectNode (Node node)
    {

        // deselect the node if it is already selected
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        // bring up turret menu (upgrade/sell)
        selectedNode = node;
        turretToBuild = null;

        turretPanel.SetActive(false);
        misslePanel.SetActive(false);
        laserPanel.SetActive(false);

        nodeUI.SetTarget(node);
    }

    // selecting a turret
    public void SelectTurretToBuild (TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

    // a selected node is now unselected
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

}
