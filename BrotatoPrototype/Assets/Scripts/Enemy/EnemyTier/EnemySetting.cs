using UnityEngine;

public abstract class EnemySetting : ScriptableObject
{
    [Space(5)]
    [Header("Other Setting")]
    public TypeEnemy TypeEnemy;
    public EnemyController PrefabTier;
    public ViewSetting ViewSetting;

    public virtual EnemyTierSettingStandart GetTierEnemy(TierType tierType)
    {
        return null;
    }
}