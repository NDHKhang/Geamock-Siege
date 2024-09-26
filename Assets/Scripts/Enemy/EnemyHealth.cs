using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float startHealth = 10f;
    float health;

    Enemy enemy;
    Tower tower;

    void OnEnable()
    {
        health = startHealth;
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
        }
    }
}
