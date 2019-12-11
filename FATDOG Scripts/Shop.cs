using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    // set up shop attributes
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;
    public GameObject turretPanel;
    public GameObject misslePanel;
    public GameObject laserPanel;

    BuildManager buildManager;

    // upon game start
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // standard turret is selected
    public void SelectStandardTurret()
    {

        turretPanel.SetActive(true);
        misslePanel.SetActive(false);
        laserPanel.SetActive(false);
        buildManager.SelectTurretToBuild(standardTurret);

    }

    // missile launcher is selected
    public void SelectMissileLauncher()
    {

        turretPanel.SetActive(false);
        misslePanel.SetActive(true);
        laserPanel.SetActive(false);
        buildManager.SelectTurretToBuild(missileLauncher);

    }

    // laser beamer is selected
    public void SelectLaserBeamer()
    {

        turretPanel.SetActive(false);
        misslePanel.SetActive(false);
        laserPanel.SetActive(true);
        buildManager.SelectTurretToBuild(laserBeamer);

    }

}
