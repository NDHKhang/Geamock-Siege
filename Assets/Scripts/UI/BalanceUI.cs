using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalanceUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayBalance;

    void Update()
    {
        updateBalance();
    }

    void updateBalance()
    {
        displayBalance.text = $"${PlayerStats.instance.CurrentBalance}";
    }
}
