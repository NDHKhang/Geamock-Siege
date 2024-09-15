using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;

    Bank bank;

    void Start()
    {
        bank = Bank.instance;
    }

    public void RewardGold()
    {
        if (bank == null) return;
        Bank.instance.Deposit(goldReward);
    }

    public void LostGold()
    {
        if (bank == null) return;
        
        Bank.instance.Withdraw(goldPenalty);
    }
}
