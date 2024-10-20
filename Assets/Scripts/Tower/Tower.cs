using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{
    [Header("Tower Attributes")]
    // Control speed and cooldown between shot of the tower
    [SerializeField] float fireRate = 1f;
    [SerializeField] float fireCountdown = 0f;

    [SerializeField] float range = 15f;
    [SerializeField] int cost = 50;
    [SerializeField] int upgradeCostBase = 100;

    [HideInInspector]
    public int upgradeCost;
    [HideInInspector]
    public int sellPrice;

    [SerializeField] float damagePerHit = 1f;

    public float FireRate { get { return fireRate; } }
    public float FireCountdown { get { return fireCountdown; } set { fireCountdown = value; } }
    public float Range { get { return range; } }
    public int Cost { get { return cost; } }
    public float DamagePerHit { get { return damagePerHit; } set { damagePerHit = value; } }

    public int UpgradeCostBase { get { return upgradeCostBase; } set { upgradeCostBase = value; } }

    // number of upgrades
    [SerializeField] int numUpgrade = 1;
    public int NumUpgrade { get { return numUpgrade; } set { numUpgrade = value; } }

    int totalUpgradePrice;

    // enemy target
    Transform target;

    // projectile speed
    //[SerializeField] float speed = 10f;
    GameObject projectile;

    BuildManager buildManager;

    //public void Seek(Transform _target, GameObject _projectile)
    //{
    //    target = _target;
    //    projectile = _projectile;
    //}

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame

    public int GetUpgradePrice()
    {
        upgradeCost = upgradeCostBase * numUpgrade;

        return upgradeCost;
    }

    public void Upgrade()
    {
        DamagePerHit += 1f;
        totalUpgradePrice += upgradeCost;
        numUpgrade++;
    }

    public int SellPriceUpgrade()
    {
        if (numUpgrade <= 1) 
            sellPrice = cost / 2;
        else
            sellPrice = totalUpgradePrice / 2;

        return sellPrice;
    }
}
