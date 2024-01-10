using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterController : EnemyController
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float damage;
    [SerializeField] private float range;

    private Projectile _projectile;

    public override void Attacking()
    {
        _projectile = Instantiate(_projectilePrefab, _shootPoint.position, _shootPoint.rotation);
        _projectile.SetRange(range);
        _projectile.SetDamage(damage);        
    }
}
