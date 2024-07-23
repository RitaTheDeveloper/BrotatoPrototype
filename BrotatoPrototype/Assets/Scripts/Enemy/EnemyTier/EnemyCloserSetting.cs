using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyCloserSetting")]
public class EnemyCloserSetting : EnemySetting
{
    public List<EnemyTierSettingStandart> _enemiesTier = new();

    public override EnemyTierSettingStandart GetTierEnemy(TierType tierType)
    {
        foreach (EnemyTierSettingStandart enemyTierSetting in _enemiesTier)
        {
            if (enemyTierSetting.Tier == tierType)
                return enemyTierSetting;
        }

        return null;
    }
}
