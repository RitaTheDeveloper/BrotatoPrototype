using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using NTC.MonoCache;

public class LivingEntity : MonoCache, IDamageable
{
    [SerializeField] protected float startingHealth;
    [SerializeField] protected float health;
    [SerializeField] protected bool regenOn = false;
    [SerializeField] protected float hpRegenPerSecond = 0f;
    
    public bool dead;

    protected virtual void Start()
    {
        //Init();
    }

    protected override void FixedRun()
    {
        if (regenOn && health < startingHealth && !dead && health > 0f)
        {
            HpRegen();            
        }
    }

    public void SetStartHealpPoint(float healpPoint)
    {
        startingHealth = healpPoint;
        Init();
    }

    public virtual void Init()
    {
        health = startingHealth;
    }

    public virtual void HpRegen()
    {
        if(hpRegenPerSecond > 0)
        {
            health += hpRegenPerSecond * Time.deltaTime;
        }

    }

    public virtual void TakeHit(float damage, bool isCrit, bool isProjectile)
    {
        health -= damage;
        if (health <= 0f && !dead)
        {
            Die();
        }
    }

    public virtual void TakeHitDelayed(float damage, bool isCrit, bool isProjectile, float delay)
    {
        StartCoroutine(HitDelayedCoroutine(damage, isCrit, isProjectile, delay));
    }

    public virtual void Die()
    {
        dead = true;
        PlaySoundOfDeath();
        Destroy(gameObject);       
    }

    public virtual void AddHealth(float hp)
    {
        if(!dead && health < startingHealth)
        {
            health += hp;
            if (health > startingHealth)
            {
                health = startingHealth;
            }
        }
    }

    protected virtual void PlaySoundOfDeath()
    {

    }
    
    protected virtual void PlaySoundOfTakeHit()
    {

    }

    protected virtual void PlaySoundOfCrit()
    {

    }

    IEnumerator HitDelayedCoroutine(float damage, bool isCrit, bool isProjectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (gameObject != null)
        {
            TakeHit(damage, isCrit, isProjectile);
        }
    }
}
