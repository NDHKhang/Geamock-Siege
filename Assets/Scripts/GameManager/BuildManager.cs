using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    [SerializeField] Vector3 positionOffset;
    [SerializeField] ParticleSystem buildEffect;

    Bank bank;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        bank = Bank.instance;
    }

    public bool CreateTower(Tower tower, Vector3 tile)
    {
        if(bank == null) return false;

        if(bank.CurrentBalance >= tower.Cost)
        {
            Instantiate(tower.gameObject, tile + positionOffset, Quaternion.identity);
            // Spawn build effect then destroy it
            Instantiate(buildEffect, tile + positionOffset, Quaternion.identity);

            bank.Withdraw(tower.Cost);
            return true;
        }

        return false;
    }
}
