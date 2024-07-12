using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class EnemyHealth : LivingEntity
{
    private float xpForKill;
    [SerializeField] private GameObject dieEffecrt;
    private void Awake()
    {
        startingHealth = GetComponent<UnitParameters>().CurrentHp;
    }

    protected override void Start()
    {
        base.Start();
        xpForKill = GetComponent<UnitParameters>().AmountOfExperience;
    }

    public override void Die()
    {
        base.Die();
        //audioSource.PlayOneShot(AudioManager.instance.GetAudioClip("EnemyDeath"));
        if (dieEffecrt != null)
        {
            Instantiate(dieEffecrt, transform.position, Quaternion.identity);
        }
        
        SpawnCurrency();
        LootSpawner lootSpawner = GetComponent<LootSpawner>();
        if (lootSpawner)
        {
            lootSpawner.SpawnLoot();
        }
    }

    public override void TakeHit(float damage, bool isCrit, bool isProjectile)
    {

        if (isCrit)
        {
            TemporaryMessageManager.Instance.AddMessageOnScreen(damage.ToString() + "!", this.gameObject.transform.position, Color.yellow, 0.5f, 20);
            PlaySoundOfCrit();
        }
        else
        {
            TemporaryMessageManager.Instance.AddMessageOnScreen(damage.ToString(), this.gameObject.transform.position, Color.white, 0.5f, 20);
            PlaySoundOfTakeHit();
        }
        base.TakeHit(damage, isCrit, false);
    }

    private void SpawnCurrency()
    {
        var currency = GameManager.instance.GetCurrencyPoolObject.currencyPool.Get();
        currency.transform.position = new Vector3(transform.position.x, currency.transform.position.y, transform.position.z);
        currency.SetXP(xpForKill);
        currency.Gold = GetComponent<UnitParameters>().AmountOfGoldForKill;
    }

    protected override void PlaySoundOfTakeHit()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("Hit", this.gameObject.transform.position);
        }
    }

    protected override void PlaySoundOfCrit()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("CriticalHit", this.gameObject.transform.position);
        }
    }

    protected override void PlaySoundOfDeath()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("EnemyDeath", this.gameObject.transform.position);
        }
    }
}
