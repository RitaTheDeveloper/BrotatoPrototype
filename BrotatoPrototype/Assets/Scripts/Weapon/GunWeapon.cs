using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private float muzzleVelocity = 35f;

    private float _nextShotTime;
    private Transform _container;

    private void Start()
    {
        _container = GameObject.Find("Bullets").transform;
        Init();
    }

    private void Update()
    {
        FindTheNearestEnemy();
        Attack();
    }

    public override void Attack()
    {
        
        if (Time.time > _nextShotTime && nearestEnemy)
        {
            RotateWeaponHolder();
            SetAttackSpeed();
            _nextShotTime = Time.time + 1 / currentAttackSpeed;
            Projectile newProjectile = Instantiate(_projectile, _muzzle.position, _muzzle.rotation);
            newProjectile.transform.parent = _container;
            newProjectile.SetSpeed(muzzleVelocity);
        }

    }
}
