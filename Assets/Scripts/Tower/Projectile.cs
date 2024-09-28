using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
    Transform target;
    Tower tower;

    [SerializeField] float speed = 70f;

    public void Seek(Transform _target, Tower _tower)
    {
        target = _target;
        tower = _tower;
    }

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

    public void HitTarget()
    {
        Damage(target);
        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(tower.DamagePerHit);
        }
    }
}
