using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] float startingHealth;
    [SerializeField] float health;
    public bool dead;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        health = startingHealth;
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;
        if (health <= 0f && !dead)
        {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        Destroy(gameObject);
    }
}
