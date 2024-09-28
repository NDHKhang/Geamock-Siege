using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;

    [SerializeField] float startHealth = 10f;
    float health;

    Tower tower;
    PlayerStats bank;

    [Header("Unity stuff")]
    [SerializeField] Image healthBar;

    void OnEnable()
    {
        health = startHealth;
    }

    void Start()
    {
        bank = PlayerStats.instance;
    }

    public void RewardGold()
    {
        if (bank == null) return;
        PlayerStats.instance.Deposit(goldReward);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        WaveSpawner.EnemiesAlive--;
        gameObject.SetActive(false);
        RewardGold();
    }
}
