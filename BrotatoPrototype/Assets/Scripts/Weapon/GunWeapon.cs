using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{
    [Range(0, 100)]
    [SerializeField] private float percantageOfRangedDamage = 100;
    [SerializeField] private Transform _muzzle;
    [Tooltip("Префаб пули")]
    [SerializeField] private Projectile _projectile;
    [Tooltip("Скорость пули")]
    [SerializeField] private float _muzzleVelocity = 45f;
    [Tooltip("Количество пуль за выстрел")]
    [SerializeField] private int _numberOfBulletsPershot;
    [Tooltip("Разброс угла выстрела")]
    [SerializeField] private float _maxAngleDispersion;


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
        RotateWeaponHolder();
    }

    private void FixedUpdate()
    {
        if (Time.time > _nextShotTime && nearestEnemy && Vector3.Distance(transform.position, nearestEnemy.transform.position) < attackRange)
        {
            Attack();
        }
    }

    protected override void Attack()
    {        
        SetAttackSpeed();
        SetAnimationSpeed(currentAttackSpeed);
        SetTimeOfAnimation(currentAttackSpeed);
        SetDamage();
        SetCritChance();
        if (animator)
        {
            animator.SetTrigger("Hit");
        }
        _nextShotTime = Time.time + 1 / currentAttackSpeed;

        for(int i = 0; i < _numberOfBulletsPershot; i++)
        {
            CreateBulletAndPassParametersToIt();
        }
        
    }

    protected override void RotateWeaponHolder()
    {
        if (nearestEnemy)
        {
            Vector3 dir = nearestEnemy.transform.position - weaponHolder.position;
            Quaternion rotation = Quaternion.Slerp(weaponHolder.rotation, Quaternion.LookRotation(dir), 30f * Time.deltaTime);
            weaponHolder.rotation = rotation;
        }
    }

    protected override void SetDamage()
    {
        base.SetDamage();
        var dmg = startDamage + playerCharacteristics.CurrentRangedDamage * percantageOfRangedDamage / 100f;
        currentDamage = dmg + dmg * playerCharacteristics.CurrentDamagePercentage / 100f;
        currentDamage = Mathf.Round(currentDamage);
        if(currentDamage < 1)
        {
            currentDamage = 1f;
        }

    }

    private void CreateBulletAndPassParametersToIt()
    {
        var tg = Mathf.Tan(Mathf.Deg2Rad * _maxAngleDispersion);
        float dispersion = tg * Random.Range(-1f, 1f);
        Vector3 direction = (_muzzle.forward + Vector3.right * dispersion).normalized;
        Projectile newProjectile = Instantiate(_projectile, _muzzle.position, Quaternion.LookRotation(direction));
        newProjectile.transform.parent = _container;
        newProjectile.SetSpeed(_muzzleVelocity);
        newProjectile.SetRange(attackRange);

        //крит или не крит
        if (Random.value < currentCritChance)
        {
            newProjectile.SetDamage(currentDamage * 2);
            newProjectile.IsCrit(true);
        }
        else
        {
            newProjectile.SetDamage(currentDamage);
            newProjectile.IsCrit(false);
        }
    }
}
