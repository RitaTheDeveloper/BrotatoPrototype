using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Range(0, 100)]
    [SerializeField] private float percantageOfMelleDamage = 100;
    

    private float _nextShotTime;
    private bool isCritDamage = false;
    private float _timer;
    private BoxCollider _collider;

    private void Start()
    {
        Init();
        _timer = 0;
        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;      
    }

    private void Update()
    {
        FindTheNearestEnemy();

        if (_timer >= _currentTimeOfAttack)
        {
            RotateWeaponHolder();
        }
    }

    private void FixedUpdate()
    {
        if (Time.time > _nextShotTime && nearestEnemy && Vector3.Distance(transform.position, nearestEnemy.transform.position) < attackRange)
        {            
            SetAttackSpeed();
            SetAnimationSpeed(currentAttackSpeed);
            SetTimeOfAnimation(currentAttackSpeed);
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

            _nextShotTime = Time.time + 1 / currentAttackSpeed;
        }

        _timer += Time.deltaTime;

        // прекратить наносить урон

        if (_timer >= _currentTimeOfAttack)
        {
            _collider.enabled = false;
        }
    }

    protected override void Attack()
    {
        _collider.enabled = true;
        _timer = 0;
        animator.SetTrigger("Hit");      
    }

    //private void SetAnimationSpeed(float currentAttackSpeed)
    //{
    //    // нам не нужно уменьшать скорость анимации, только увеличивать
    //    if (currentAttackSpeed > 1)
    //    {
    //        animator.speed = _startAnimationSpeed * currentAttackSpeed;
    //    }
    //}

    //private void SetTimeOfAnimation(float currentAttackSpeed)
    //{
    //    // нам не нужно уменьшать время анимации, только увеличивать
    //    if (currentAttackSpeed > 1)
    //    {
    //        _currentTimeOfAttack = timeOfAttack / currentAttackSpeed;
    //    }
    //    else
    //    {
    //        _currentTimeOfAttack = timeOfAttack;
    //    }
    //}

    protected override void SetDamage()
    {
        base.SetDamage();
        var dmg = startDamage + playerCharacteristics.CurrentMelleeDamage * percantageOfMelleDamage / 100f;
        currentDamage = dmg + dmg * playerCharacteristics.CurrentDamagePercentage / 100f;
        currentDamage = Mathf.Round(currentDamage);
        if (currentDamage < 1)
        {
            currentDamage = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            if (isCritDamage)
            {
                // наносим крит
                other.GetComponent<LivingEntity>().TakeHit(currentDamage * 2, true);
            }
            else
            {
                // обычный урон
                other.GetComponent<LivingEntity>().TakeHit(currentDamage, false);
            }            
        }
    }
    
}
