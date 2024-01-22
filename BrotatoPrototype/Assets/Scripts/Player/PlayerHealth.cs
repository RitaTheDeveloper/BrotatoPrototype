using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    public bool invulnerability;
    public bool canTakeDmg;
    [SerializeField] private float timeOfInvulnerability = 0.5f;
    private float _timer;

    protected override void Start()
    {
        _timer = 0f;
        invulnerability = false;
        canTakeDmg = false;
        SetMaxHP();
        SetHpRegenPerSecond();
        base.Start();
        DisplayHealth();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // для того, чтобы все враги которые рядом могли нанести урон в кадр
        if (canTakeDmg)
        {
            invulnerability = true;
            canTakeDmg = false;
        }
        
        if (invulnerability)
        {
            _timer += Time.deltaTime;
            if (_timer > timeOfInvulnerability)
            {
                _timer = 0f;
                invulnerability = false;
            }
        }               
    }

    public override void Die()
    {
        base.Die();
        DisplayHealth();
        GameManager.instance.Lose();
    }

    public override void HpRegen()
    {
        base.HpRegen();
        DisplayHealth();
    }

    public void DisplayHealth()
    {
        UIManager.instance.DisplayHealth(health, startingHealth);
    }

    public override void TakeHit(float damage)
    {
        if (!invulnerability)
        {
            base.TakeHit(damage);
            canTakeDmg = true;
        }
        
        DisplayHealth();
    }

    public void SetHpRegenPerSecond()
    {
        hpRegenPerSecond = GetComponent<PlayerCharacteristics>().CurrentHpRegen;
    }

    public void SetMaxHP()
    {
        startingHealth = GetComponent<PlayerCharacteristics>().CurrentMaxHp;
    }

}
