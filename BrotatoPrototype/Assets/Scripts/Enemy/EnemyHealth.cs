using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    private float xpForKill;
    [SerializeField] private GameObject dieEffecrt;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        startingHealth = GetComponent<UnitParameters>().CurrentHp;
    }

    protected override void Start()
    {
        base.Start();
        xpForKill = GetComponent<UnitParameters>().AmountOfExperience;
    }

    //public override void TakeHit(float damage)
    //{
    //    base.TakeHit(damage);
    //    TemporaryMessageManager.Instance.AddMessageOnScreen(damage.ToString(), this.gameObject.transform.position, Color.white, 0.5f, 20);
    //    EnemyDamageEffect effector = GetComponent<EnemyDamageEffect>();
    //    if (effector)
    //    {
    //        effector.DoDamageEffect();
    //    }
    //}

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

    public override void TakeHit(float damage, bool isCrit)
    {
        
        if (isCrit)
        {
            TemporaryMessageManager.Instance.AddMessageOnScreen(damage.ToString() + "!", this.gameObject.transform.position, Color.yellow, 0.5f, 20);
            PlaySoundOfTakeHit();

        }
        else
        {
            TemporaryMessageManager.Instance.AddMessageOnScreen(damage.ToString(), this.gameObject.transform.position, Color.white, 0.5f, 20);
            PlaySoundOfCrit();
        }
        base.TakeHit(damage, isCrit);
    }

    private void SpawnCurrency()
    {
        var currency = PoolObject.instance.currencyPool.Get();
        currency.transform.position = new Vector3(transform.position.x, currency.transform.position.y, transform.position.z);
        currency.SetXP(xpForKill);
    }

    protected override void PlaySoundOfTakeHit()
    {
        base.PlaySoundOfTakeHit();
    }

    protected override void PlaySoundOfCrit()
    {
        base.PlaySoundOfCrit();
    }

    protected override void PlaySoundOfDeath()
    {
        base.PlaySoundOfDeath();
    }
}
