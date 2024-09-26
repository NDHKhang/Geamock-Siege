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
    public Tower TowerToBuild {  get { return towerToBuild; } set { towerToBuild = value; } }

    [SerializeField] TileUI tileUI;
    //Store currently selected tile
    Tile selectedTile;

    Bank bank;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        bank = Bank.instance;
    }

    public void CreateTower()
    {
        selectedTile.CreateTower(TowerToBuild, buildEffect);
        DeselectTile();
    }

    public void SelectTile(Tile tile)
    {
        if (selectedTile == tile)
        {
            DeselectTile();
            return;
        }

        DeselectTile();
        selectedTile = tile;

        if (tileUI == null) return;
        tileUI.SetTarget(selectedTile);
    }

    public void DeselectTile()
    {
        selectedTile = null;
        tileUI.HideUI();
    }
}
