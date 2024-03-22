using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class PlayerHealth : LivingEntity
{
    public bool invulnerability;
    public bool canTakeDmg;
    [SerializeField] private float timeOfInvulnerability = 0.5f;
    private float _timer;
    private float _oneHp = 0;
    private float _probabilityOfDodge;
    private float _armor;
    private float _lifeStealPercentage;

    private PlayerCharacteristics playerCharacteristics;

    protected override void Start()
    {
        playerCharacteristics = GetComponent<PlayerCharacteristics>();
        _timer = 0f;
        invulnerability = false;
        canTakeDmg = false;
        SetMaxHP();
        SetHpRegenPerSecond();
        SetArmor();
        SetProbabilityOfDodge();
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

        _oneHp += hpRegenPerSecond * Time.deltaTime;
        if (_oneHp >= 1)
        {
            TemporaryMessageManager.Instance.AddMessageOnScreen("+" + ((int)_oneHp).ToString(), this.gameObject.transform.position, Color.green);
            _oneHp = 0f;
        }
        DisplayHealth();
    }

    public void DisplayHealth()
    {        
        UIManager.instance.DisplayHealth(health, startingHealth);
    }

    public override void TakeHit(float damage, bool isCrit)
    {
        if (!invulnerability)
        {
            if (IsDodge())
            {
                TemporaryMessageManager.Instance.AddMessageOnScreen("уклонение", this.gameObject.transform.position, Color.blue);
            }
            else
            {
                var resultDamage = GetDamageAfterArmor(damage, _armor);
                base.TakeHit(resultDamage, isCrit);
                TemporaryMessageManager.Instance.AddMessageOnScreen("-" + resultDamage.ToString(), this.gameObject.transform.position, Color.red);
                Camera.main.GetComponent<PostEffectController>().PlayDammageEffect();
            }
            
            canTakeDmg = true;
        }
        
        DisplayHealth();
    }

    public void SetHpRegenPerSecond()
    {
        hpRegenPerSecond = playerCharacteristics.CurrentHpRegen;
    }

    public void SetMaxHP()
    {
        startingHealth = playerCharacteristics.CurrentMaxHp;
    }

    public void SetProbabilityOfDodge()
    {
        float probDodge = playerCharacteristics.CurrentProbabilityOfDodge;

        if (probDodge < 0f)
        {
            _probabilityOfDodge = 0f;
            Debug.Log("уклонение не может отрицательным");
        }
        else if (probDodge > 60f)
        {
            _probabilityOfDodge = 60f;
            Debug.Log("уклонение не может быть больше 60 %");
        }
        else
        {
            _probabilityOfDodge = probDodge;
        }
    }
        
    private float GetDamageAfterArmor(float damage, float armor)
    {
        float percentageOfDamageTaken = 1 - (1 / (1 + (armor / 15)));
        float resultDamage = damage - percentageOfDamageTaken * damage;
        return Mathf.Round(resultDamage);
    }

    public void SetArmor()
    {
        _armor = playerCharacteristics.CurrentArmor;
    }

    private bool IsDodge()
    {
        float random = Random.Range(0, 100);

        if(random <= _probabilityOfDodge)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void AddHealth(float hp)
    {
        base.AddHealth(hp);
        TemporaryMessageManager.Instance.AddMessageOnScreen("+" + ((int)hp).ToString(), this.gameObject.transform.position, Color.green);
        DisplayHealth();
    }
}
