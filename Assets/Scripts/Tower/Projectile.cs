using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Projectile : MonoBehaviour
{
    Transform target;

    [SerializeField] float speed = 10f;

    [SerializeField] int damagePerHit = 1;
    public int DamagePerHit { get { return damagePerHit; } set { damagePerHit = value; } }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileHandle();
    }

    void ProjectileHandle()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Get the direction of the target then shoot bullet
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Check if bullet reach the target or not
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Damage(target);
        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        EnemyHealth e = enemy.GetComponent<EnemyHealth>();

        if (e != null)
        {
            e.TakeDamage(DamagePerHit);
        }
    }
}
