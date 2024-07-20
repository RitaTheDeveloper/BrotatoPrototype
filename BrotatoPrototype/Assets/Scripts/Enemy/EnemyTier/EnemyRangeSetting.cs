using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyRangeSetting")]
public class EnemyRangeSetting : EnemySetting
{
    public List<EnemyRangeTierSetting> _enemiesTier = new();

    public override EnemyTierSettingStandart GetTierEnemy(TierType tierType)
    {
        foreach (EnemyRangeTierSetting enemyTierSetting in _enemiesTier)
            if (enemyTierSetting.Tier == tierType)
                return enemyTierSetting;

        return null;
    }
}
