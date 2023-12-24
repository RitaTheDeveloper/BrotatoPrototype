using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] protected float startingHealth;
    [SerializeField] protected float health;
    [SerializeField] protected bool regenOn = false;
    [SerializeField] protected float hpRegenPerSecond = 0f;
    
    public bool dead;

    protected virtual void Start()
    {
        Init();
    }

    protected virtual void FixedUpdate()
    {
        if (regenOn && health < startingHealth && !dead)
        {
            HpRegen();
        }
    }

    public void Init()
    {
        health = startingHealth;
    }

    public virtual void HpRegen()
    {
        health += hpRegenPerSecond * Time.deltaTime;
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
