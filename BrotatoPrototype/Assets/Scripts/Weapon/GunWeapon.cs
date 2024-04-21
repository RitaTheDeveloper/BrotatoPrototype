using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{
    [SerializeField] private Transform _muzzle;
    [Tooltip("������ ����")]
    [SerializeField] private Projectile _projectile;
    [Tooltip("�������� ����")]
    [SerializeField] private float _muzzleVelocity = 45f;
    [Tooltip("���������� ���� �� �������")]
    [SerializeField] private int _numberOfBulletsPershot;
    [Tooltip("������� ���� ��������")]
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
        if (!GameManager.instance.GameIsOver)
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

            for (int i = 0; i < _numberOfBulletsPershot; i++)
            {
                CreateBulletAndPassParametersToIt();
            }
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

        //���� ��� �� ����
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
