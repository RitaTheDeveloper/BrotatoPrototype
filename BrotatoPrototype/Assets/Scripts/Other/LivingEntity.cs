using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] protected float startingHealth;
    [SerializeField] protected float health;
    
    public bool dead;

    protected virtual void Start()
    {
        Init();
    }

    public void Init()
    {
        health = startingHealth;
    }

    public virtual void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;
        if (health <= 0f && !dead)
        {
            Die();
        }

    }
    public virtual void TakeHit(float damage)
    {
        health -= damage;
        if (health <= 0f && !dead)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        dead = true;
        Destroy(gameObject);       
    }

    
}
