using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Range(0, 100)]
    [SerializeField] private float percantageOfMelleDamage = 100;

    [SerializeField] private Animator animator;
    [SerializeField] private Type typeOfWeapon;
    [SerializeField] private float timeOfAttack = 0.5f; // for sword это время нужно не хардкордить и анимацию ускорять, когда ускоряем

    private float _startAnimationSpeed;
    private float _nextShotTime;
    private float _currentTimeOfAttack; 
    private bool isCritDamage = false;
    private float _timer;
    private BoxCollider _collider;

    private void Start()
    {
        Init();
        _timer = 0;
        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
        _startAnimationSpeed = animator.speed;
        SetAnimationSpeed(currentAttackSpeed);
        SetTimeOfAnimation(currentAttackSpeed);
    }

    private void Update()
    {
        FindTheNearestEnemy2();
    }
    private void LateUpdate()
    {
        if (_timer >= _currentTimeOfAttack)
        {
            RotateWeaponHolder();
        }
    }

    private void FixedUpdate()
    {
        if (Time.time > _nextShotTime && nearestEnemy && Vector3.Distance(transform.position, nearestEnemy.transform.position) < attackRange)
        {
            SetAnimationSpeed(currentAttackSpeed);
            SetTimeOfAnimation(currentAttackSpeed);
            SetAttackSpeed();
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

    public override void Attack()
    {
        _collider.enabled = true;
        _timer = 0;
        animator.SetTrigger("Hit");      
    }

    private void SetAnimationSpeed(float currentAttackSpeed)
    {
        // нам не нужно уменьшать скорость анимации, только увеличивать
        if (currentAttackSpeed > 1)
        {
            animator.speed = _startAnimationSpeed * currentAttackSpeed;
        }
    }

    private void SetTimeOfAnimation(float currentAttackSpeed)
    {
        // нам не нужно уменьшать время анимации, только увеличивать
        if (currentAttackSpeed > 1)
        {
            _currentTimeOfAttack = timeOfAttack / currentAttackSpeed;
        }
        else
        {
            _currentTimeOfAttack = timeOfAttack;
        }
    }

    public override void SetDamage()
    {
        base.SetDamage();
        currentDamage = startDamage + playerCharacteristics.CurrentMelleeDamage * percantageOfMelleDamage / 100f;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            if (isCritDamage)
            {
                // наносим крит
                other.GetComponent<LivingEntity>().TakeHit(currentDamage * 2);
            }
            else
            {
                // обычный урон
                other.GetComponent<LivingEntity>().TakeHit(currentDamage);
            }            
        }
    }
    
}
