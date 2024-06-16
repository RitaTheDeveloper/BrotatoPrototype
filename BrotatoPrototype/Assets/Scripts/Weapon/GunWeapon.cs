using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{
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

    protected float _timer;

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
        if (_timer > _timeLoop && nearestEnemy && Vector3.Distance(transform.position, nearestEnemy.transform.position) < attackRange)
            Attack();

        _timer += Time.fixedDeltaTime;
    }

    protected override void Attack()
    {
        if (!GameManager.instance.GameIsOver)
        {
            _timer = 0;

            SetAttackSpeed();
            SetAnimationSpeed();
            SetDamage();
            SetCritChance();

            if (animator)
                animator.SetTrigger("Hit");

            for (int i = 0; i < _numberOfBulletsPershot; i++)
                CreateBulletAndPassParametersToIt();

            base.Attack();
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
        newProjectile.SetIsKnockable(knockBack);
        newProjectile.SetRepulsiveForce(repulsiveForce);

        //крит или не крит
        if (Random.value < currentCritChance)
        {
            newProjectile.SetDamage(currentDamage * 5);
            newProjectile.IsCrit(true);
        }
        else
        {
            newProjectile.SetDamage(currentDamage);
            newProjectile.IsCrit(false);
        }
    }
}
