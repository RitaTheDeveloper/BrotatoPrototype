using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Range(0, 100)]
    [SerializeField] private float percantageOfMelleDamage = 100;

    [SerializeField] private Animator animator;
    [SerializeField] private Type typeOfWeapon;
    
    private float _nextShotTime;
    private float timeOfAttack = 0.27f; // for bur это время нужно не хардкордить и анимацию ускорять, когда ускоряем
    private bool isCritDamage = false;

    private void Start()
    {
        Init();
        animator.speed = animator.speed * currentAttackSpeed;
        timeOfAttack = timeOfAttack / currentAttackSpeed;
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
    }

    public override void Attack()
    {
        animator.SetTrigger("Hit");      
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
