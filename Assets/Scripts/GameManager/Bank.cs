using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] int startBalance = 100;
    [SerializeField] int currentBalance;
    
    public int CurrentBalance { get { return currentBalance; } }

    public static Bank instance;

    void Awake()
    {
        currentBalance = startBalance;
        if(instance == null) instance = this;
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
    }

    public void Withdraw(int amount)
    {
        if(currentBalance > 0)
            currentBalance -= Mathf.Abs(amount);
    }
}
