using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    protected override void Start()
    {
        SetMaxHP();
        SetHpRegenPerSecond();
        base.Start();
        DisplayHealth();
    }

    public override void Die()
    {
        base.Die();
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
        base.TakeHit(damage);
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
