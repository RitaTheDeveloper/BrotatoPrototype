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
    [SerializeField] private float maxArmor = 60f;
    private float _timer;
    private float _oneHp = 0;
    private float _probabilityOfDodge;
    private float _armor;
    private float _lifeStealPercentage;
    private PlayerCharacteristics playerCharacteristics;
    private float maxStartHealth;

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

    private void Update()
    {
        if (health < startingHealth * 0.45f)
        {
            UIManager.instance.LowHPImageOn(true);
        }
        else
        {
            UIManager.instance.LowHPImageOn(false);
        }
    }

    public override void Die()
    {
        DisplayHealth();
        GameManager.instance.Lose();
        base.Die();
    }

    public override void HpRegen()
    {
        base.HpRegen();
        if (hpRegenPerSecond > 0)
        {
            _oneHp += hpRegenPerSecond * Time.deltaTime;
            if (_oneHp >= 1)
            {
                TemporaryMessageManager.Instance.AddMessageOnScreen("+" + ((int)_oneHp).ToString(), this.gameObject.transform.position, Color.green);
                _oneHp = 0f;
            }
            DisplayHealth();
        }
       
    }

    public void DisplayHealth()
    {
        float satiety = 100 - playerCharacteristics.CurrentSatiety;
        UIManager.instance.DisplayHealth(health, startingHealth, maxStartHealth, satiety);
    }

    public override void TakeHit(float damage, bool isCrit, bool isProjectile)
    {
        if (isProjectile)
        {
            if (IsDodge())
            {
                TemporaryMessageManager.Instance.AddMessageOnScreen("уклонение", this.gameObject.transform.position, Color.blue);
            }
            else
            {
                var resultDamage = GetDamageAfterArmor(damage, _armor);
                base.TakeHit(resultDamage, isCrit, false);
                PlaySoundOfTakeHit();
                TemporaryMessageManager.Instance.AddMessageOnScreen("-" + resultDamage.ToString(), this.gameObject.transform.position, Color.red);
                Camera.main.GetComponent<PostEffectController>().PlayDammageEffect();
            }
        }
        else
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
                    base.TakeHit(resultDamage, isCrit, false);
                    PlaySoundOfTakeHit();
                    TemporaryMessageManager.Instance.AddMessageOnScreen("-" + resultDamage.ToString(), this.gameObject.transform.position, Color.red);
                    Camera.main.GetComponent<PostEffectController>().PlayDammageEffect();
                }

                canTakeDmg = true;
            }
        }
                
        DisplayHealth();
    }

    public void SetHpRegenPerSecond()
    {
        hpRegenPerSecond = playerCharacteristics.CurrentHpRegen;
    }

    public void SetMaxHP()
    {
        maxStartHealth = playerCharacteristics.CurrentMaxHp;
        startingHealth = SetStartHealthDependingOfSatiety();
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

    public void UpdateCharactestics()
    {
        SetMaxHP();
        SetProbabilityOfDodge();
        SetHpRegenPerSecond();
        SetArmor();
    }
        
    private float GetDamageAfterArmor(float damage, float armor)
    {
        //float percentageOfDamageTaken = 1 - (1 / (1 + (armor / 15)));
        //float resultDamage = damage - percentageOfDamageTaken * damage;
        if(armor > maxArmor)
        {
            armor = maxArmor;
        }
        float resultDamage = damage - damage * armor * 0.01f;
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

    protected override void PlaySoundOfTakeHit()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("PlayerTakeHit");
        }
    }

    protected override void PlaySoundOfDeath()
    {
        //Пока такого звука нет
    }

    public override void Init()
    {
        startingHealth = SetStartHealthDependingOfSatiety();
        base.Init();
    }

    public float SetStartHealthDependingOfSatiety()
    {
        float satiety = playerCharacteristics.CurrentSatiety / 100f;
        if (satiety > 1) satiety = 1f;
        float startHealth = Mathf.Ceil(maxStartHealth * satiety);
        if (startHealth < 1f)
        {
            startHealth = 1f;
        }
        return startHealth;
    }
}
