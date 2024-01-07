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
    private float timer;
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

        timer += Time.deltaTime;

        if (timer >= timeOfAttack)
        {
            ReturnWeponHolderRotationToStarting();
        }
    }

    public override void Attack()
    {
        timer = 0f;
        animator.SetTrigger("Hit");      
    }

    public override void SetDamage()
    {
        base.SetDamage();
        currentDamage = startDamage + playerCharacteristics.CurrentMelleeDamage * percantageOfMelleDamage / 100f;
    }

    private void OnTriggerEnter(Collider other)
    {
        //IDamageable damageableObject = other.gameObject.GetComponent<IDamageable>();
        //if (damageableObject != null)
        //{
        //    damageableObject.TakeHit(0.2f);
        //    Debug.Log("ударить " +  other);
        //    Destroy(other.gameObject);
        //}
        if (other.tag == "Enemy")
        {
            if (isCritDamage)
            {
                // наносим крит
                other.GetComponent<LivingEntity>().TakeHit(currentDamage * 2);
                Debug.Log("крит! " + currentDamage * 2);
            }
            else
            {
                // обычный урон
                other.GetComponent<LivingEntity>().TakeHit(currentDamage);
                Debug.Log("обычный урон " + currentDamage);
            }
            
        }
    }

    
}
