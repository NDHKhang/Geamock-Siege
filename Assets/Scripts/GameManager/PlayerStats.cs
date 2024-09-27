using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int startBalance = 100;
    int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    [SerializeField] float playerHealth = 10;
    public float PlayerHealth { get { return playerHealth; } }
    float health;
    [SerializeField] Image healthBar;

    public static PlayerStats instance;

    void Awake()
    {
        currentBalance = startBalance;
        health = playerHealth;
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
    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / PlayerHealth;

        if (health <= 0)
        {
            //Game Over;
            Debug.Log("Game Over");
        }
    }
}
