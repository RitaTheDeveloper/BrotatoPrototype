using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float _msBetweenAttacks = 1;
    [SerializeField] private float _speedAttack = 5f;
    [SerializeField] private Animator animator;

    private BoxCollider _collider;
    private float _nextShotTime;
    private void Start()
    {
        // Attack();
        _collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Time.time > _nextShotTime)
        {
            Attack();
            _nextShotTime = Time.time + _msBetweenAttacks;
        }
    }


    public override void Attack()
    {
        //animator.SetBool("Attack", true);
        animator.SetTrigger("Hit");
    }


    //private void OnCollisionEnter(Collision collision)
    //{

    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }

    }
}
