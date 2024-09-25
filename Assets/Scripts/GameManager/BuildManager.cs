using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    [SerializeField] Vector3 positionOffset;
    public Vector3 PostitionOffset { get {  return positionOffset; } }

    [SerializeField] ParticleSystem buildEffect;

    // Store prefab to build
    [SerializeField] Tower tower1;
    [SerializeField] Tower tower2;

    public Tower Tower1 { get { return tower1; } }
    public Tower Tower2 { get { return tower2; } }

    //Tile selectedTile;
    Tower towerToBuild;

    [SerializeField] TileUI tileUI;

    public Tower TowerToBuild {  get { return towerToBuild; } set { towerToBuild = value; } }

    Bank bank;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        bank = Bank.instance;
    }

    public void CreateTower(Tile tile)
    {
        if(bank == null) return;
        if (bank.CurrentBalance >= towerToBuild.Cost)
        {
            Instantiate(towerToBuild.gameObject, tile.transform.position + positionOffset, Quaternion.identity);
            // Spawn build effect
            Instantiate(buildEffect, tile.transform.position + positionOffset, Quaternion.identity);

            bank.Withdraw(towerToBuild.Cost);
            tile.IsPlaceable = false;
        }
    }

    public void selectTile(Tile tile)
    {
        if(tileUI == null) return;

        tileUI.SetTarget(tile);
    }
}
