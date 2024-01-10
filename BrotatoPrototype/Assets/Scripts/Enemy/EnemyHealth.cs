using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    private float xpForKill;

    private void Awake()
    {
        Debug.Log("spawn! ");
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
        var currency = PoolObject.instance.currencyPool.Get();
        currency.transform.position = new Vector3(transform.position.x, currency.transform.position.y, transform.position.z);
        currency.SetXP(xpForKill);
    }
}
