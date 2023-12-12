using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float _msBetweenAttacks = 1;
    [SerializeField] private Animator animator;

    private BoxCollider _collider;
    private float _nextShotTime;
    private void Start()
    {
        // Attack();
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
            _nextShotTime = Time.time + _msBetweenAttacks;
        }
    }


    public override void Attack()
    {
        //animator.SetBool("Attack", true);
        animator.SetTrigger("Hit");
    }
}
