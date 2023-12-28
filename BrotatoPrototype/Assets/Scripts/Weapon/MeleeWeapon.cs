using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private Animator animator;
    [SerializeField] private Type typeOfWeapon;
    [Range(0,100)]
    [SerializeField] private float percantageOfMelleDamage = 100;
    private float _nextShotTime;
    private float timeOfAttack = 0.27f; // for bur ��� ����� ����� �� ����������� � �������� ��������, ����� ��������
    private float timer;

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
            Attack();
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
        //    Debug.Log("������� " +  other);
        //    Destroy(other.gameObject);
        //}
        if (other.tag == "Enemy")
        {
            other.GetComponent<LivingEntity>().TakeHit(currentDamage);
        }
    }

    
}
