using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySetting : ScriptableObject
{
    [Space(5)]
    [Header("Other Setting")]
    public TypeEnemy TypeEnemy;

    public virtual EnemyTierSettingStandart GetTierEnemy(TierType tierType)
    {
        return null;
    }
}

[System.Serializable]
[CreateAssetMenu(menuName = "Enemy/Container")]
public class Container : ScriptableObject
{
    public List<EnemySetting> EnemiesSetting;
}

[System.Serializable]
[CreateAssetMenu(menuName = "Enemy/EnemyCloserSetting")]
public class EnemyCloserSetting : EnemySetting
{
    public List<EnemyTierSettingStandart> _enemiesTier = new();

    public override EnemyTierSettingStandart GetTierEnemy(TierType tierType)
    {
        foreach(EnemyTierSettingStandart enemyTierSetting in _enemiesTier)
            if(enemyTierSetting.Tier == tierType)
                return enemyTierSetting;

        return null;
    }
}

[System.Serializable]
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

[System.Serializable]
public class EnemyTierSettingStandart
{
    public string Name;
    [Space(5)]
    [Header("Visual Setting")]
    public TierType Tier;
    public EnemyController PrefabTier;
    [Space(5)]
    [Header("Standart Setting")]
    public float HealPoint;
    public float Damage;
    public float TimeBetweenAttacks;
    public float Speed;
    [Space(5)]
    [Header("Direction?")]
    public float Distance;
    public float SpeedDirection;
    public float TimeStopDirection;
    public float CDDirection;
    [Space(5)]
    [Header("Spawn Unit")]
    public GameObject PrefabSpawnUnit;
    public TierType TierSpawnUnit;
    public float TimeBetweenSpawn;
    public int CountSpawnUnit;
}

[System.Serializable]
public class EnemyRangeTierSetting : EnemyTierSettingStandart
{
    [Space(5)]
    [Header("Range")]
    public float RangeAttack;
}

[System.Serializable]
public enum TypeEnemy
{
    NULL,
    Skeleton,
    SkeletonBoss,
    Bear,
    BearBoss,
    Crow,
    CrowBoss,
    EvilTree,
    EvilTreeBoss,
    Spawner,
    SpawnerBoss,
    Wolf,
    WolfBoss,
    CrowCharger,
    CrowChargerBoss,
    ArmoredRaven,
    ArmoredRavenBoss,
}
