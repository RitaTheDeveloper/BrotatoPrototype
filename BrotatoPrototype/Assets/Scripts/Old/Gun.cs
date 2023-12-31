using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private float _msBetweenShots = 100f;
    [SerializeField] private float muzzleVelocity = 35f;

    private float _nextShotTime;

    private void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
        if (Time.time > _nextShotTime)
        {
            _nextShotTime = Time.time + _msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(_projectile, _muzzle.position, _muzzle.rotation);
            newProjectile.SetSpeed(muzzleVelocity);
        }
        
    }
}
