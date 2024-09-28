using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Tower))]
public class HandleTowerBehavior : MonoBehaviour
{
    [Header("Target Locator")]
    [SerializeField] Transform partToRotate;
    [SerializeField] float rotateSpeed = 10f;

    Transform target;
    Tower tower;

    [Header("Game Object initialize")]
    // For initialize bullet and spawn postition
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePosition;

    private void Awake()
    {
        tower = GetComponent<Tower>();
    }

    void Start()
    {
        InvokeRepeating("FindClosestTarget", 0f, 0.5f);
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Transform closestTarget = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
                if (targetDistance < shortestDistance)
                {
                    
                    closestTarget = enemy.transform;
                    shortestDistance = targetDistance;
                }
            }
        }
        if (closestTarget != null && shortestDistance <= tower.Range)
            target = closestTarget;
        else
            target = null;
    }

    void Update()
    {
        AimWeapon();
        HandleFiring();
    }

    void AimWeapon()
    {
        if (target == null)
            return;
        //partToRotate.LookAt(target);
        // Get direction need to rotating
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;

        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void HandleFiring()
    {
        if(target ==  null) return;

        if(tower.FireCountdown <= 0f)
        {
            Shoot();
            tower.FireCountdown = 1f / tower.FireRate;
        }

        tower.FireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        if(target == null) return;

        GameObject projectileGO = Instantiate(projectilePrefab, firePosition.position, firePosition.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null)
        {
            //tower.Seek(target, projectileGO);
            projectile.Seek(target, tower);
        }
    }

    // Visual range for test
    void OnDrawGizmosSelected()
    {
        if (tower == null) tower = GetComponent<Tower>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, tower.Range);
    }
}
