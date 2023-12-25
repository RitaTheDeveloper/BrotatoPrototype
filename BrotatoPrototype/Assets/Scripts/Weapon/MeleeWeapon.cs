using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private Animator animator;

    private BoxCollider _collider;
    private float _nextShotTime;
    private void Start()
    {
        Init();
        _collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        FindTheNearestEnemy();
    }

    private void FixedUpdate()
    {
        if (Time.time > _nextShotTime && nearestEnemy)
        {
            RotateWeaponHolder();
            Attack();
            SetAttackSpeed();
            _nextShotTime = Time.time + 1 / currentAttackSpeed;
        }
    }


    public override void Attack()
    {
        //animator.SetBool("Attack", true);
        animator.SetTrigger("Hit");
    }
}
