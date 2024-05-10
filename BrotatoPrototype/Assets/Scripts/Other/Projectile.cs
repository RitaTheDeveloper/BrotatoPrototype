using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private int penetration = 1;
    [SerializeField] LayerMask collisionMask;
    [SerializeField] private bool knockBack = false;

    private bool _isCrit;
    private float _range;

    private float _damage;

    Vector3 direction;

    private void Start()
    {
        // _isCrit = false;
        direction = Vector3.forward;
        Destroy(gameObject, _range / _speed);
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void SetRange(float attackRange)
    {
        _range = attackRange;
    }

    public void IsCrit(bool isCrit)
    {
        _isCrit = isCrit;
    }

    private void Update()
    {
        Move();
        CheckCollsion(_speed * Time.deltaTime);        
    }

    private void Move()
    {
        transform.Translate(direction * Time.deltaTime * _speed);
    }

    private void CheckCollsion(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OutHitObject(hit);
        }
    }

    private void OutHitObject(RaycastHit hit)
    {
        IDamageable damageableObject = hit.collider.GetComponentInParent<IDamageable>();              
        if (damageableObject != null)
        {
            damageableObject.TakeHit(_damage, _isCrit, true);
        }

        if (knockBack)
        {
            IKnockbackable knockbackableObject = hit.collider.GetComponentInParent<IKnockbackable>();
            if (knockbackableObject != null)
            {
                knockbackableObject.GetKnockedBack(transform.forward.normalized * 500f);
            }
        }
        
        penetration -= 1;

        if (penetration <= 0)
        {            
            GameObject.Destroy(gameObject);
        }
        
    }
}
