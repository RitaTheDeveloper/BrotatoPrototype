using System.Collections.Generic;
using UnityEngine;

public class ManagerEnemyTier : MonoBehaviour
{
    [SerializeField] private Container _containerEnemy;

    private EnemySetting _lastEnemy;

    public EnemyController GetSpawnEnemy(TypeEnemy typeEnemy, TierType tierType, Vector3 position, Quaternion rotation)
    {
        EnemySetting currentEnemy = null;

        if (_lastEnemy != null && _lastEnemy.TypeEnemy == typeEnemy)
        {
            currentEnemy = _lastEnemy;
            goto Skip;
        }

        foreach (EnemySetting enemySetting in _containerEnemy.EnemiesSetting)
            if(enemySetting.TypeEnemy == typeEnemy)
                currentEnemy = enemySetting;

        if(currentEnemy == null)
            return null;

            Skip:
        EnemyTierSettingStandart enemyTierSetting = currentEnemy.GetTierEnemy(tierType);

        if(enemyTierSetting == null)
            throw new System.NotImplementedException("No tier!");

        EnemyController enemy = Instantiate(currentEnemy.PrefabTier, position, rotation);
        _lastEnemy = currentEnemy;
        enemy.LoadPar(enemyTierSetting);

        return enemy;
    }
}