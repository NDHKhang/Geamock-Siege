using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Attributes")]
    // Control speed and cooldown between shot of the tower
    [SerializeField] float fireRate = 1f;
    [SerializeField] float fireCountdown = 0f;

    [SerializeField] float range = 15f;
    [SerializeField] int cost = 50;

    public float FireRate { get { return fireRate; } }
    public float FireCountdown { get { return fireCountdown; } set { fireCountdown = value; } }
    public float Range { get { return range; } }
    public int Cost { get { return cost; } }
}
