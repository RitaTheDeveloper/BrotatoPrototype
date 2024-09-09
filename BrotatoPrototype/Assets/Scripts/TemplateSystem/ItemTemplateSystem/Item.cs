using System;
using System.Collections.Generic;
using UnityEngine;

public class Item : BaseItem
{
    [Space]
    [Header("Item Template")]
    [SerializeField] protected ItemTemplate itemTemplate;

    [Space]
    [Header("Item Characteristics")]
    [Header("Baff multipliers")]
    [SerializeField] protected ItemBaff[] baffs;
    [Header("Debaff multipliers")]
    [SerializeField] protected ItemBaff[] debaffs;

    [Space]
    [Header("Characteristics")]
    [SerializeField] protected CharacteristicValues characteristicValues;

    protected Dictionary<CharacteristicType, float> characteristicMap = new Dictionary<CharacteristicType, float>();


    /* Needs get rid PlayerCharacteristics component at Item
     * To do this needs refactor UI-item's view*/
    protected PlayerCharacteristics playerCharacteristics;

    public override void SynchronizeComponents()
    {
        //CalculateAllCharacteristics();
        SynchronizePlayerCharacteristics();
        SynchronizeItemShopInfo();
        SynchonizeStandartItem();
    }

    [ContextMenu("CalculateAllCharacteristics")]
    protected override void CalculateAllCharacteristics()
    {
        if (baffs.Length == 0 || debaffs.Length == 0)
        {
            throw new NotSupportedException($"{gameObject.name} baff or debaff list sould not be empty!");
        }

        ItemTemplateData data = itemTemplate.GetTemplateDataForSpecificTier(tier) as ItemTemplateData;
        ItemCharacteristicIncrement baseIncrement = itemTemplate.GetBaseIncrement();

        foreach (ItemBaff baff in baffs) 
        {
            CalculateCharacteristic(baff.characteristic, baff.multiplier, baseIncrement, data.baffStrength, false);
        }

        foreach (ItemBaff debaff in debaffs)
        {
            CalculateCharacteristic(debaff.characteristic, debaff.multiplier, baseIncrement, data.debaffStrength, true);
        }
    }

    protected float CalculateTotalMultiplierForItem()
    {
        float totalMultiplier = 0;
        foreach (ItemBaff baff in baffs)
        {
            totalMultiplier += baff.multiplier;
        }
        foreach (ItemBaff debaff in debaffs)
        {
            totalMultiplier += debaff.multiplier;
        }
        return totalMultiplier;
    }

    protected void CalculateCharacteristic(CharacteristicType characteristic, float multiplier, ItemCharacteristicIncrement baseIncrement, float baffStrength, bool isDebaff)
    {
        switch (characteristic)
        {
            case CharacteristicType.Satiety:
                characteristicValues.satiety = Calculate(baseIncrement.satiety, multiplier, baffStrength, isDebaff);
                characteristicMap[characteristic] = characteristicValues.satiety;
                break;

            case CharacteristicType.MaxHealth:
                characteristicValues.maxHealth = Calculate(baseIncrement.maxHealth, multiplier, baffStrength, isDebaff);
                characteristicMap[characteristic] = characteristicValues.maxHealth;
                break;

            case CharacteristicType.RegenerationHP:
                characteristicValues.regenerationHP = Calculate(baseIncrement.regenerationHP, multiplier, baffStrength, isDebaff);
                characteristicMap[characteristic] = characteristicValues.regenerationHP;
                break;

            case CharacteristicType.Dodge:
                characteristicValues.dodge = Calculate(baseIncrement.dodge, multiplier, baffStrength, isDebaff);
                characteristicMap[characteristic] = characteristicValues.dodge;
                break;

            case CharacteristicType.Armor:
                characteristicValues.armor = Calculate(baseIncrement.armor, multiplier, baffStrength, isDebaff);
                characteristicMap[characteristic] = characteristicValues.armor;
                break;
                
            case CharacteristicType.MoveSpeed:
                characteristicValues.moveSpeed = Calculate(baseIncrement.moveSpeed, multiplier, baffStrength, isDebaff);
                characteristicMap[characteristic] = characteristicValues.moveSpeed;
                break;

            case CharacteristicType.AttackSpeed:
                characteristicValues.attackSpeed = Calculate(baseIncrement.attackSpeed, multiplier, baffStrength, isDebaff);
                characteristicMap[characteristic] = characteristicValues.attackSpeed;
                break;

            case CharacteristicType.Damage:
                float totalMultiplier = CalculateTotalMultiplierForItem();
                characteristicValues.damage = Calculate(baseIncrement.damage, multiplier, baffStrength, isDebaff);
                characteristicMap[characteristic] = characteristicValues.damage;
                break;

            case CharacteristicType.MeleeDamage:
                characteristicValues.meleeDamage = Calculate(baseIncrement.meleeDamage, multiplier, baffStrength, isDebaff);
                characteristicMap[characteristic] = characteristicValues.meleeDamage;
                break;

            case CharacteristicType.RangeDamage:
                characteristicValues.rangeDamage = Calculate(baseIncrement.rangeDamage, multiplier, baffStrength, isDebaff);
                characteristicMap[characteristic] = characteristicValues.rangeDamage;
                break;

            case CharacteristicType.ChanceOfCrit:
                characteristicValues.critChance = Calculate(baseIncrement.chanceOfCrit, multiplier, baffStrength, isDebaff);
                characteristicMap[characteristic] = characteristicValues.critChance;
                break;

            case CharacteristicType.MagneticRadius:
                characteristicValues.magneticRadius = Calculate(baseIncrement.magneticRadius, multiplier, baffStrength, isDebaff);
                characteristicMap[characteristic] = characteristicValues.magneticRadius;
                break;

            default:
                throw new NotSupportedException($"Item {gameObject.name} has not supported characteristic {characteristic}");
        }
    }

    protected float Calculate(float baseCharacteristicIncrement ,float multiplier, float baffStrength, bool isDebaff)
    {
        float totalMultiplier = CalculateTotalMultiplierForItem();
        if (isDebaff)
        {
            float result = baseCharacteristicIncrement * multiplier / totalMultiplier * baffStrength * (-1);
            return Mathf.Round(result * 10.0f) * 0.1f;
        }
        else
        {
            float result = baseCharacteristicIncrement * multiplier / totalMultiplier * baffStrength;
            return Mathf.Round(result * 10.0f) * 0.1f;
        }
    }

    [ContextMenu("SynchonizeAllCharacteristics")]
    protected void SynchronizePlayerCharacteristics()
    {
        playerCharacteristics = GetComponent<PlayerCharacteristics>();
        
        if (playerCharacteristics == null) 
            return;

        foreach (ItemBaff baff in baffs)
        {
            SynchronizeCharacteristic(baff.characteristic, characteristicMap[baff.characteristic]);
        }

        foreach (ItemBaff debaff in debaffs)
        {
            SynchronizeCharacteristic(debaff.characteristic, characteristicMap[debaff.characteristic]);
        }
    }

    protected void SynchronizeCharacteristic(CharacteristicType characteristic, float value)
    {
        playerCharacteristics.SynchronizeCharacteristic(characteristic, value);
    }

    protected void SynchonizeStandartItem()
    {
        StandartItem standartItem = GetComponent<StandartItem>();

        standartItem.IdItem = editorName;
    }

    protected override void SynchronizeItemShopInfo()
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

}
