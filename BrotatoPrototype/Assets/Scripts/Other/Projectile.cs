using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    private float _range;
    [SerializeField] LayerMask collisionMask;

    private float _damage;

    private void Start()
    {
        Debug.Log(" t = " + _range / _speed);
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

    private void Update()
    {
        Move();
        CheckCollsion(_speed * Time.deltaTime);        
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
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
            damageableObject.TakeHit(_damage, hit);
        }
        GameObject.Destroy(gameObject);
    }
}
