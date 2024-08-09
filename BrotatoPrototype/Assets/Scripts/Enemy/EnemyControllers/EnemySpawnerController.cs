using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
public class EnemySpawnerController : EnemyController
{
    private EnemySpawner _enemySpawner;

    public override void LoadPar(EnemyTierSettingStandart enemyTierSetting)
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _enemySpawner.InitSpawner(enemyTierSetting.ManagerEnemyTier, 0);//Very baaaaaad
        _enemySpawner.LoadPar(enemyTierSetting.TypeSpawnEnemy, enemyTierSetting.TierSpawnUnit, enemyTierSetting.TimeBetweenSpawn, enemyTierSetting.CountSpawnUnit);
        base.LoadPar(enemyTierSetting);
    }
}
