using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUI : MonoBehaviour
{
    Tile tile;
    BuildManager buildManager;

    public void SetTarget(Tile _tile)
    {
        buildManager = BuildManager.instance;
        // Display UI and get pressed tile position
        gameObject.SetActive(true);

        tile = _tile;
        transform.position = tile.GetTilePostition() + buildManager.PostitionOffset;
    }

    public void PurchaseBallista1()
    {
        if (buildManager != null)
        {
            buildManager.TowerToBuild = buildManager.Tower1;
            buildManager.CreateTower(tile);
            HideUI();
        }
    }

    public void PurchaseBallista2()
    {
        if (buildManager != null)
        {
            buildManager.TowerToBuild = buildManager.Tower2;
            buildManager.CreateTower(tile);
            HideUI();
        }
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }
}
