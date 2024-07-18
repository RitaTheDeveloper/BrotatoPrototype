using UnityEngine;

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
