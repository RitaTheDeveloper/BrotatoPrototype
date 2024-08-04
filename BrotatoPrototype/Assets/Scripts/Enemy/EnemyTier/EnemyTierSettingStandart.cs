using UnityEngine;

[System.Serializable]
public class EnemyTierSettingStandart
{
    public string Name;
    [Space(5)]
    [Header("Visual Setting")]
    public TierType Tier;
    [Space(5)]
    [Header("Standart Setting")]
    public float HealPoint;
    public float Damage;
    public float TimeBetweenAttacks;
    public float Speed;
    [Range(0, 10000)]
    public float Exp;
    [Range(0, 10000)]
    public int LootCount;
    [Range(0, 10000)]
    public int GoldCount;
    [Space(5)]
    [Header("Escape setting")]
    public float EscapeSpeed;
    public float DistanceFromPlayer;
    [Space(5)]
    [Header("Direction?")]
    public float Distance;
    public float SpeedDirection;
    public float TimeStopDirection;
    public float CDDirection;
    [Space(5)]
    [Header("Spawn Unit")]
    public TypeEnemy TypeSpawnEnemy;
    public TierType TierSpawnUnit;
    public float TimeBetweenSpawn;
    public int CountSpawnUnit;

    //Very very very very very very very very very very very very very very very very very very very BAAAAAAADDDDDDDDDDDDDDDDDDDDDDDDDDDD
    public ManagerEnemyTier ManagerEnemyTier { get; private set; }
    public Loot Loot { get; private set; }

    public void SetOther(ManagerEnemyTier managerEnemyTier, Loot loot)
    {
        ManagerEnemyTier = managerEnemyTier;
        Loot = loot;
    }
}
