using System;
using System.Collections.Generic;
using UnityEngine;
using static ItemTemplate;

public class Item : MonoBehaviour
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

    // Item characteristics in %
    private float satiety;
    private float maxHealth;
    private float regenerationHP;
    private float dodge;
    private float armor;
    private float moveSpeed;
    private float attackSpeed;
    private float damage;
    private float meleeDamage;
    private float rangeDamage;
    private float critChance;
    private float magneticRadius;


    /* Needs get rid PlayerCharacteristics component at Item
     * To do this needs refactor UI-item's view*/
    private PlayerCharacteristics playerCharacteristics;

    [Space(40)]
    [Header("Current properties")]
    public TierType tier = TierType.FirstTier;
    public string gameName;
    public string editorName;
    private Sprite icon;
    private Dictionary<CharacteristicType, float> characteristicMap = new Dictionary<CharacteristicType, float>();

    public Item Initialize(TierType tier, Transform parent)
    {
        this.tier = tier;

        switch (tier)
        {
            case TierType.FirstTier:
                gameName = inGameNameT1_3;
                icon = iconT1_3;
                break;

            case TierType.SecondTier:
                gameName = inGameNameT1_3;
                icon = iconT1_3;
                break;

            case TierType.ThirdTier:
                gameName = inGameNameT1_3;
                icon = iconT1_3;
                break;

            case TierType.FourthTier:
                gameName = inGameNameT4;
                icon = iconT4;
                break;

        }

        AddSuffixToEditorName(tier);

        Item instancedItem = Instantiate(this, parent);
        RenameInstance(instancedItem, editorName);
        return instancedItem;
    }

    public void SynchronizeComponents()
    {
        SynchronizePlayerCharacteristics();
        SynchronizeItemShopInfo();
        SynchonizeStandartItem();
    }

    private void AddSuffixToEditorName(TierType tier)
    {
        editorName = gameObject.name;

        editorName = editorName.Remove(editorName.Length - 3);

        switch (tier)
        {
            case TierType.FirstTier:
                editorName = editorName + "_T1";
                break;
            case TierType.SecondTier:
                editorName = editorName + "_T2";
                break;
            case TierType.ThirdTier:
                editorName = editorName + "_T3";
                break;
            case TierType.FourthTier:
                editorName = editorName + "_T4";
                break;
        }
    }

    private void RenameInstance(Item item, string newEditorName)
    {
        item.gameObject.name = newEditorName;
    }

    [ContextMenu("CalculateAllCharacteristics")]
    private void CalculateAllChartacteristics()
    {
        if (baffs.Length == 0 || debaffs.Length == 0)
        {
            throw new NotSupportedException($"{gameObject.name} baff or debaff list sould not be empty!");
        }

        TemplateData data = itemTemplate.GetTemplateDataForSpecificTier(tier);
        BaseCharacteristicsIncrement baseIncrement = itemTemplate.GetBaseIncrement();

        foreach (Baff baff in baffs) 
        {
            CalculateCharacteristic(baff.characteristic, baff.multiplier, baseIncrement, data.baffStrength);
        }

        foreach (Debaff debaff in debaffs)
        {
            CalculateCharacteristic(debaff.characteristic, debaff.multiplier, baseIncrement, data.debaffStrength);
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

    private void CalculateCharacteristic(CharacteristicType characteristic, float multiplier, BaseCharacteristicsIncrement baseIncrement, float baffStrength)
    {
        switch (characteristic)
        {
            case CharacteristicType.Satiety:
                satiety = Calculate(baseIncrement.satiety, multiplier, baffStrength);
                characteristicMap[characteristic] = satiety;
                break;

            case CharacteristicType.MaxHealth:
                maxHealth = Calculate(baseIncrement.maxHealth, multiplier, baffStrength);
                characteristicMap[characteristic] = maxHealth;
                break;

            case CharacteristicType.RegenerationHP:
                regenerationHP = Calculate(baseIncrement.regenerationHP, multiplier, baffStrength);
                characteristicMap[characteristic] = regenerationHP;
                break;

            case CharacteristicType.Dodge:
                dodge = Calculate(baseIncrement.dodge, multiplier, baffStrength);
                characteristicMap[characteristic] = dodge;
                break;

            case CharacteristicType.Armor:
                armor = Calculate(baseIncrement.armor, multiplier, baffStrength);
                characteristicMap[characteristic] = armor;
                break;
                
            case CharacteristicType.MoveSpeed:
                moveSpeed = Calculate(baseIncrement.moveSpeed, multiplier, baffStrength);
                characteristicMap[characteristic] = moveSpeed;
                break;

            case CharacteristicType.AttackSpeed:
                attackSpeed = Calculate(baseIncrement.attackSpeed, multiplier, baffStrength);
                characteristicMap[characteristic] = attackSpeed;
                break;

            case CharacteristicType.Damage:
                damage = Calculate(baseIncrement.damage, multiplier, baffStrength);
                characteristicMap[characteristic] = damage;
                break;

            case CharacteristicType.MeleeDamage:
                meleeDamage = Calculate(baseIncrement.meleeDamage, multiplier, baffStrength);
                characteristicMap[characteristic] = meleeDamage;
                break;

            case CharacteristicType.RangeDamage:
                rangeDamage = Calculate(baseIncrement.rangeDamage, multiplier, baffStrength);
                characteristicMap[characteristic] = rangeDamage;
                break;

            case CharacteristicType.ChanceOfCrit:
                critChance = Calculate(baseIncrement.chanceOfCrit, multiplier, baffStrength);
                characteristicMap[characteristic] = critChance;
                break;

            case CharacteristicType.MagneticRadius:
                magneticRadius = Calculate(baseIncrement.magneticRadius, multiplier, baffStrength);
                characteristicMap[characteristic] = magneticRadius;
                break;
        }
    }

    private float Calculate(float baseCharacteristicIncrement ,float multiplier, float baffStrength)
    {
        float totalMultiplier = CalculateTotalMultiplierForItem();
        return baseCharacteristicIncrement * multiplier / totalMultiplier * baffStrength;
    }

    [ContextMenu("SynchonizeAllCharacteristics")]
    private void SynchronizePlayerCharacteristics()
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

    private void SynchronizeItemShopInfo()
    {
        ItemShopInfo itemShopInfo = GetComponent<ItemShopInfo>();

        // IdWeapon property
        itemShopInfo.IdWeapon = editorName;

        // LevelItem property
        itemShopInfo.LevelItem = itemTemplate.GetPrefabDataForSpecificTier(tier);

        // Price property
        float price = itemTemplate.GetTemplateDataForSpecificTier(tier).price;
        itemShopInfo.Price = (int)price;

        // NameWeapon property
        itemShopInfo.NameWeapon = gameName;

        // IconWeapon property
        itemShopInfo.IconWeapon = icon;
    }

    private void SynchonizeStandartItem()
    {
        StandartItem standartItem = GetComponent<StandartItem>();

        standartItem.IdItem = editorName;
    }

}

[System.Serializable]
public class Baff
{
    public CharacteristicType characteristic;
    [Range(0.001f, 10f)]
    public float multiplier;
}

[System.Serializable]
public class Debaff
{
    public CharacteristicType characteristic;
    [Range(0.001f, 10f)]
    public float multiplier;
}