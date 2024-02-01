using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    private float xpForKill;

    private void Awake()
    {
        startingHealth = GetComponent<UnitParameters>().CurrentHp;
    }

    protected override void Start()
    {
        base.Start();
        xpForKill = GetComponent<UnitParameters>().AmountOfExperience;
    }

    public override void TakeHit(float damage)
    {
        base.TakeHit(damage);
        TemporaryMessageManager.Instance.AddMessageOnScreen(damage.ToString(), this.gameObject.transform.position, Color.white, 0.5f, 20);
        EnemyDamageEffect effector = GetComponent<EnemyDamageEffect>();
        if (effector)
        {
            effector.DoDamageEffect();
        }
    }

    public override void Die()
    {
        base.Die();
        var currency = PoolObject.instance.currencyPool.Get();
        currency.transform.position = new Vector3(transform.position.x, currency.transform.position.y, transform.position.z);
        currency.SetXP(xpForKill);
    }

    public override void TakeHit(float damage, bool isCrit)
    {
        base.TakeHit(damage, isCrit);
        if (isCrit)
        {
            TemporaryMessageManager.Instance.AddMessageOnScreen(damage.ToString() + "!", this.gameObject.transform.position, Color.yellow, 0.5f, 23);
        }
        else
        {
            TemporaryMessageManager.Instance.AddMessageOnScreen(damage.ToString(), this.gameObject.transform.position, Color.white, 0.5f, 20);
        }
        
    }
}
