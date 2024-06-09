using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MeleeWeapon : Weapon
{    
    protected float _nextShotTime;
    private bool isCritDamage = false;
    protected float _timer;
    private BoxCollider _collider;

    private void Start()
    {
        Init();
        _timer = 0;
        _collider = GetComponent<BoxCollider>();
        if (_collider)
        {
            _collider.enabled = false;
        }
     
    }

    private void Update()
    {
        FindTheNearestEnemy();

        if (_timer >= _currentAnimationTime)
        {
            RotateWeaponHolder();
        }
    }

    private void FixedUpdate()
    {
        if (Time.time > _timeLoop && nearestEnemy && Vector3.Distance(transform.position, nearestEnemy.transform.position) - nearestEnemy.GetComponent<NavMeshAgent>().radius < attackRange)
        {            
            SetAttackSpeed();
            SetAnimationSpeed();
            SetDamage();
            SetCritChance();

            Attack();

            // крит или не крит
            if (Random.value < currentCritChance)
            {
                isCritDamage = true;
            }
            else
            {
                isCritDamage = false;
            }
        }

        _timer += Time.deltaTime;

        // прекратить наносить урон

        if (_timer >= _currentAnimationTime)
            _collider.enabled = false;
    }

    protected override void Attack()
    {
        if (!GameManager.instance.GameIsOver)
        {
            _collider.enabled = true;
            _timer = 0;
            animator.SetTrigger("Hit");
        }            

        base.Attack();
    }

    protected override void SetDamage()
    {
        base.SetDamage();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            if (isCritDamage)
            {
                // наносим крит
                other.GetComponent<LivingEntity>().TakeHit(currentDamage * 5, true, false);
            }
            else
            {
                // обычный урон
                other.GetComponent<LivingEntity>().TakeHit(currentDamage, false,false);
            }

            if (knockBack)
            {
                IKnockbackable knockbackableObject = other.GetComponentInParent<IKnockbackable>();
                if (knockbackableObject != null)
                {
                    knockbackableObject.GetKnockedBack(transform.forward.normalized * repulsiveForce);                   
                }
            }           
        }
    }
    
}
