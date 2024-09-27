using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileUI : MonoBehaviour
{
    Tile tile;
    BuildManager buildManager;

    [SerializeField] TextMeshProUGUI Tower1;
    [SerializeField] TextMeshProUGUI Tower2;

    [SerializeField] GameObject buildUI;
    [SerializeField] GameObject upgradeSellUI;

    [SerializeField] TextMeshProUGUI upgradeText;
    [SerializeField] TextMeshProUGUI sellText;

    [SerializeField] ParticleSystem upgradeEffect;
    [SerializeField] ParticleSystem sellEffect;

    bool isDisplay;

    public void SetTarget(Tile _tile)
    {
        buildManager = BuildManager.instance;

        // Display UI and get pressed tile position
        tile = _tile;
        transform.position = tile.GetTilePostition() + buildManager.PostitionOffset;

        if(tile.IsPlaceable)
        {
            buildUI.SetActive(true);
            DisplayPrice();
        }
        else
        {
            upgradeSellUI.SetActive(true);
            DisplayUpgradeSell();
        }   
    }
    void DisplayPrice()
    {
        Tower1.text = $"${buildManager.Tower1.Cost}";
        Tower2.text = $"${buildManager.Tower2.Cost}";
    }

    void DisplayUpgradeSell()
    {
        upgradeText.text = $"<b>+1 Damage</b> \n ${tile.getUpgradePrice()}";
        sellText.text = $"<b>Sell</b> \n ${tile.getSellPrice()}";
    }

    public void PurchaseBallista1()
    {
        if (buildManager != null)
        {
            buildManager.TowerToBuild = buildManager.Tower1;
            buildManager.CreateTower();
        }
    }

    public void PurchaseBallista2()
    {
        if (buildManager != null)
        {
            buildManager.TowerToBuild = buildManager.Tower2;
            buildManager.CreateTower();
        }
    }

    public void HideUI()
    {
        if(buildManager != null)
        {
            buildManager.selectedTile = null;
            buildUI.SetActive(false);
            upgradeSellUI.SetActive(false);
        }
    }

    public void UpgradeTower()
    {
        bool isUpgrade = tile.UpgradeTower();
        if (!isUpgrade) return;

        Instantiate(upgradeEffect, tile.transform.position + buildManager.PostitionOffset, Quaternion.identity);
        DisplayUpgradeSell();
    }

    public void SellTower()
    {
        tile.SellTower();
        Instantiate(sellEffect, tile.transform.position + buildManager.PostitionOffset, Quaternion.identity);
        HideUI();
    }
}
