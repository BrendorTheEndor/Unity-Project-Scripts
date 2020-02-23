using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{

    // set up the NodeUI attributes
    private Node target;

    public GameObject UI;
    public Button upgradeButton;
    public Text upgradeCost;
    public Text sellAmount;


    // display the NodeUI
    public void SetTarget(Node _target)
    {

        this.target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost.ToString();
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
            upgradeCost.text = "UPGRADED";
        }

        sellAmount.text = "+$" + target.turretBlueprint.GetSellAmount();

        UI.SetActive(true);

    }

    // deactivate the NodeUI
    public void Hide()
    {
        UI.SetActive(false);
    }

    // when upgrade button is selected
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    // when sell button is selected
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
