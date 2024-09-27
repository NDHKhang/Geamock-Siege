using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    // For creating tower
    [SerializeField] bool isPlaceable;
    [SerializeField] bool canPlaceTower;

    GameObject tower;
    Tower towerComponent;

    public bool CanPlaceTower { get { return canPlaceTower; } }

    BuildManager buildManager;
    PlayerStats bank;
    GridManager gridManager;
    Vector2Int coordinates;

    public bool IsPlaceable { get { return isPlaceable; } set { isPlaceable = value; } }

    void Start()
    {
        gridManager = GridManager.instance;
        HandleTile();

        if(!isPlaceable) canPlaceTower = false;

        buildManager = BuildManager.instance;
        bank = PlayerStats.instance;
    }

    void HandleTile()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (gameObject.transform.parent.tag != "Path")
                gridManager.BlockNode(coordinates);
        }
    }

    public Vector3 GetTilePostition()
    {
        return transform.position;
    }

    void OnMouseDown()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            if (canPlaceTower)
            {
                buildManager.SelectTile(this);
            }
        }
    }

    public void CreateTower(Tower towerToBuild, ParticleSystem buildEffect)
    {
        if (bank == null) return;
        if (bank.CurrentBalance >= towerToBuild.Cost && isPlaceable)
        {
            GameObject _tower = Instantiate(towerToBuild.gameObject, transform.position + buildManager.PostitionOffset, Quaternion.identity);
            // Spawn build effect
            Instantiate(buildEffect, transform.position + buildManager.PostitionOffset, Quaternion.identity);

            bank.Withdraw(towerToBuild.Cost);
            isPlaceable = false;
            tower = _tower;
            towerComponent = tower.GetComponent<Tower>();
            towerComponent.SellPriceUpgrade();
        }
    }
    public bool UpgradeTower()
    {
        int upgradePrice = towerComponent.GetUpgradePrice();
        if (bank.CurrentBalance >= upgradePrice)
        {
            towerComponent.Upgrade();
            bank.Withdraw(upgradePrice);
            towerComponent.SellPriceUpgrade();

            return true;
        }

        return false;
    }

    public void SellTower()
    {
        bank.Deposit(towerComponent.SellPriceUpgrade());
        Destroy(tower);
        isPlaceable = true;
    }

    public int getUpgradePrice()
    {
        return towerComponent.UpgradeCostBase * towerComponent.NumUpgrade;
    }

    public int getSellPrice()
    {
        return towerComponent.sellPrice;
    }
}
