using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private Animator animator;
    Type type;
    private float _nextShotTime;
    private Quaternion startRotation;
    private float timeOfAttack = 0.27f; // for bur это время нужно не хардкордить и анимацию ускорять, когда ускоряем
    private float timer;

    private void Start()
    {
        Init();
        type = Type.mellee;
        startRotation = weaponHolder.transform.rotation;
        animator.speed = animator.speed * currentAttackSpeed;
        timeOfAttack = timeOfAttack / currentAttackSpeed;
        Debug.Log(timeOfAttack);
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

        timer += Time.deltaTime;

        if (timer >= timeOfAttack)
        {
            weaponHolder.rotation = startRotation;
        }
    }

    public override void Attack()
    {
        timer = 0f;
        animator.SetTrigger("Hit");      
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

            other.GetComponent<LivingEntity>().TakeHit(0.5f);
            Debug.Log("ударить " + other + " " + this);
        }
    }
}
