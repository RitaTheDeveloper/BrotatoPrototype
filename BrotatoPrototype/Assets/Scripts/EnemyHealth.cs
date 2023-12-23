using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    private float xpForKill;
    private GameObject player;
    private LevelSystem playerLevelSystem;
   

    protected override void Start()
    {
        base.Start();
        xpForKill = GetComponent<UnitParameters>().AmountOfExperience;
        player = GetComponent<EnemyController>().target.gameObject;
        playerLevelSystem = player.GetComponent<LevelSystem>();
    }
    public override void Die()
    {
        base.Die();
        var currency = PoolObject.instance.currencyPool.Get();
        currency.transform.position = new Vector3(transform.position.x, currency.transform.position.y, transform.position.z);
        currency.SetXP(xpForKill);
        //playerLevelSystem.IncreaseCurrentExperience(xpForKill);
    }
}
