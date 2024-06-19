using System;
using System.Collections.Generic;
using UnityEngine;
using static ItemTemplate;

public enum CharacteristicType
{
    Satiety,
    MaxHealth,
    RegenerationHP,
    Dodge,
    Armor,
    MoveSpeed,
    AttackSpeed,
    Damage,
    MeleeDamage,
    RangeDamage,
    ChanceOfCrit,
    MagneticRadius
};

public class ItemCharacteristics : MonoBehaviour
{
    [Header("Name")]
    [SerializeField] private string inGameNameT1_3;
    [SerializeField] private string inGameNameT4;
    [Header("Icon")]
    [SerializeField] private Sprite iconT1_3;
    [SerializeField] private Sprite iconT4;

    [Space]
    [Header("Item Template")]
    [SerializeField] private ItemTemplate itemTemplate;

    [Space]
    [Header("Item Characteristics")]

    [Header("Baff multipliers")]
    [SerializeField] private Baff[] baffs;

    [Header("Debaff multipliers")]
    [SerializeField] private Debaff[] debaffs;

    [System.Serializable]
    public class Baff
    {
        public CharacteristicType characteristic;
        [Range(0.0001f, 10f)]
        public float multiplier;
    }

    [System.Serializable]
    public class Debaff
    {
        public CharacteristicType characteristic;
        [Range(0.001f, 10f)]
        public float multiplier;
    }

    private Dictionary<CharacteristicType, float> characteristicMap = new Dictionary<CharacteristicType, float>();

    // Item characteristics bonus in %
    public float satiety { get; private set; }
    public float maxHealth { get; private set; }
    public float regenerationHP { get; private set; }
    public float dodge {  get; private set; }
    public float armor { get; private set; }
    public float moveSpeed { get; private set; }
    public float attackSpeed { get; private set; }
    public float damage { get; private set; }
    public float meleeDamage { get; private set; }
    public float rangeDamage { get; private set; }
    public float critChance { get; private set; }
    public float magneticRadius {  get; private set; }

    private RareItemsDataStruct tier;

    private PlayerCharacteristics playerCharacteristics;


    [ContextMenu("CalculateAllCharacteristics")]
    public void CalculateAllChartacteristics()
    {
        if (baffs.Length == 0 || debaffs.Length == 0) 
            return;

        tier = GetComponent<ItemShopInfo>().GetLevelItem();
        DataPerTier data = itemTemplate.GetTierTemplateData(tier);
        BaseIncrementCharacteristics baseIncrement = itemTemplate.GetBaseIncrement();
        float totalMultiplier = CalculateTotalMultiplierForItem();

        foreach (Baff baff in baffs) 
        {
            CalculateCharacteristic(baff.characteristic, baff.multiplier, baseIncrement, totalMultiplier, data.baffStrength);
        }

        foreach (Debaff debaff in debaffs)
        {
            CalculateCharacteristic(debaff.characteristic, debaff.multiplier, baseIncrement, totalMultiplier, data.debaffStrength);
        }
    }

    private float CalculateTotalMultiplierForItem()
    {
        float totalMultiplier = 0;
        foreach (Baff baff in baffs)
        {
            totalMultiplier += baff.multiplier;
        }
        return totalMultiplier;
    }

    private void CalculateCharacteristic(CharacteristicType characteristic, float multiplier, BaseIncrementCharacteristics baseIncrement, float totalMultiplier, float baffStrength)
    {
        switch (characteristic)
        {
            case CharacteristicType.Satiety:
                satiety = Calculate(baseIncrement.satiety, multiplier, totalMultiplier, baffStrength);
                characteristicMap[characteristic] = satiety;
                break;

            case CharacteristicType.MaxHealth:
                maxHealth = Calculate(baseIncrement.maxHealth, multiplier, totalMultiplier, baffStrength);
                characteristicMap[characteristic] = maxHealth;
                break;

            case CharacteristicType.RegenerationHP:
                regenerationHP = Calculate(baseIncrement.regenerationHP, multiplier, totalMultiplier, baffStrength);
                characteristicMap[characteristic] = regenerationHP;
                break;

            case CharacteristicType.Dodge:
                dodge = Calculate(baseIncrement.dodge, multiplier, totalMultiplier, baffStrength);
                characteristicMap[characteristic] = dodge;
                break;

            case CharacteristicType.Armor:
                armor = Calculate(baseIncrement.armor, multiplier, totalMultiplier, baffStrength);
                characteristicMap[characteristic] = armor;
                break;
                
            case CharacteristicType.MoveSpeed:
                moveSpeed = Calculate(baseIncrement.moveSpeed, multiplier, totalMultiplier, baffStrength);
                characteristicMap[characteristic] = moveSpeed;
                break;

            case CharacteristicType.AttackSpeed:
                attackSpeed = Calculate(baseIncrement.attackSpeed, multiplier, totalMultiplier, baffStrength);
                characteristicMap[characteristic] = attackSpeed;
                break;

            case CharacteristicType.Damage:
                damage = Calculate(baseIncrement.damage, multiplier, totalMultiplier, baffStrength);
                characteristicMap[characteristic] = damage;
                break;

            case CharacteristicType.MeleeDamage:
                meleeDamage = Calculate(baseIncrement.meleeDamage, multiplier, totalMultiplier, baffStrength);
                characteristicMap[characteristic] = meleeDamage;
                break;

            case CharacteristicType.RangeDamage:
                rangeDamage = Calculate(baseIncrement.rangeDamage, multiplier, totalMultiplier, baffStrength);
                characteristicMap[characteristic] = rangeDamage;
                break;

            case CharacteristicType.ChanceOfCrit:
                critChance = Calculate(baseIncrement.chanceOfCrit, multiplier, totalMultiplier, baffStrength);
                characteristicMap[characteristic] = critChance;
                break;

            case CharacteristicType.MagneticRadius:
                magneticRadius = Calculate(baseIncrement.magneticRadius, multiplier, totalMultiplier, baffStrength);
                characteristicMap[characteristic] = magneticRadius;
                break;
        }
    }

    private float Calculate(float baseCharacteristicIncrement ,float multiplier, float totalMultiplier, float baffStrength)
    {
        return baseCharacteristicIncrement * multiplier / totalMultiplier * baffStrength;
    }

    [ContextMenu("SynchonizeAllCharacteristics")]
    public void SynchronizeAllCharacteristics()
    {
        playerCharacteristics = GetComponent<PlayerCharacteristics>();
        
        if (playerCharacteristics == null) 
            return;

        CalculateAllChartacteristics();

        foreach (Baff baff in baffs)
        {
            SynchronizeCharacteristic(baff.characteristic, characteristicMap[baff.characteristic]);
        }

        foreach (Debaff debaff in debaffs)
        {
            SynchronizeCharacteristic(debaff.characteristic, characteristicMap[debaff.characteristic] * (-1));
        }
    }

    private void SynchronizeCharacteristic(CharacteristicType characteristic, float value)
    {
        playerCharacteristics.SynchronizeCharacteristic(characteristic, value);
    }
}
