using HighlightPlus;
using System;
using UnityEngine;

public class Weapon : BaseItem
{
    [Space]
    [Header("Weapon Template")]
    [SerializeField] private WeaponTemplate weaponTemplate;

    [Space]
    [Header("Weapon Additional Characteristic")]
    [SerializeField] protected WeaponBaff[] baffs;
    [SerializeField] protected WeaponBaff[] debaffs;

    [Space]
    [Header("Weapon Characteristics")]
    [Min(1f)]
    [SerializeField] private float startDamage = 30f;
    [Min(0.001f)]
    [SerializeField] private float startCritChance = 0.02f;
    [SerializeField] private float reductionCoeff = 1f;
    
    [Space]
    [ReadOnlyInspector]
    [SerializeField] private float damage;
    [ReadOnlyInspector]
    [SerializeField] private float critChance;
    [ReadOnlyInspector]
    [SerializeField] private float attackSpeed;

    public override void SynchronizeComponents()
    {
        SynchronizeWeaponLogicComponent();
        SynchronizeItemShopInfo();
        SynchronizeHighlightEffect();
    }

    private void SynchronizeHighlightEffect()
    {
        HighlightEffect highlightEffectComponent = GetComponent<HighlightEffect>();

        if (highlightEffectComponent == null)
        {
            throw new NullReferenceException($"HighlightEffect.cs component must be attached to {gameObject.name}");
        }

        HighlightProfile profileToLoad = weaponTemplate.GetHighlightProfileForSprecificTier(tier);
        highlightEffectComponent.ProfileLoad(profileToLoad);
        highlightEffectComponent.SetHighlighted(true);
    }

    private void SynchronizeWeaponLogicComponent()
    {
        BaseWeapon weaponLogicComponent = GetComponent<BaseWeapon>();

        if (weaponLogicComponent == null) 
        {
            throw new NullReferenceException($"To {gameObject.name} must be attached MeleeWeapon/GunWeapon/MagicWeapon component ");
        }

        weaponLogicComponent.SetWeaponBuff(characteristicMap);
        weaponLogicComponent.SetStartDamage(damage);
        weaponLogicComponent.SetStartAttackSpeedPercentage(attackSpeed);
        weaponLogicComponent.SetStartCritChance(critChance);
    }

    protected override void SynchronizeItemShopInfo()
    {
        ItemShopInfo itemShopInfo = GetComponent<ItemShopInfo>();

        // IdWeapon property
        itemShopInfo.IdWeapon = editorName;

        // LevelItem property
        itemShopInfo.LevelItem = weaponTemplate.GetPrefabDataForSpecificTier(tier);

        // Price property
        float price = weaponTemplate.GetTemplateDataForSpecificTier(tier).price;
        itemShopInfo.Price = (int)price;

        // NameWeapon property
        itemShopInfo.NameWeapon = gameName;

        // IconWeapon property
        itemShopInfo.IconWeapon = icon;
    }

    protected override void CalculateAllCharacteristics()
    {
        CalculateWeaponCharacteristics();
        CalculateAdditionalCharacteristics();
    }

    private void CalculateWeaponCharacteristics()
    {
        WeaponTemplateData data = weaponTemplate.GetTemplateDataForSpecificTier(tier) as WeaponTemplateData;

        CalculateDamage(data);
        CalculateCritChance(data);
        CalulateAttackSpeed(data);
    }

    private void CalculateDamage(WeaponTemplateData data)
    {
        damage = startDamage * data.damageStrength;
    }

    private void CalculateCritChance(WeaponTemplateData data)
    {
        critChance = startCritChance * data.critChanceStrength;
    }

    private void CalulateAttackSpeed(WeaponTemplateData data)
    {
        float damagePerSecond = data.damagePerSecond;
        float critStrength = weaponTemplate.GetCritStrength();

        attackSpeed = damagePerSecond / (damage * (1 - critChance) + damage * critChance * critStrength) / reductionCoeff;
    }

    private void CalculateAdditionalCharacteristics()
    {
        foreach (WeaponBaff baff in baffs)
        {
            CalculateAdditionalCharacteristic(baff, false);
        }

        foreach (WeaponBaff debaff in debaffs)
        {
            CalculateAdditionalCharacteristic(debaff, true);
        }
    }

    private void CalculateAdditionalCharacteristic(WeaponBaff weaponBaff, bool isDebaff)
    {
        switch (weaponBaff.characteristic)
        {
            case CharacteristicType.Satiety:
                characteristicValues.satiety = Calculate(weaponBaff, isDebaff);
                characteristicMap[weaponBaff.characteristic] = characteristicValues.satiety;
                break;

            case CharacteristicType.MaxHealth:
                characteristicValues.maxHealth = Calculate(weaponBaff, isDebaff);
                characteristicMap[weaponBaff.characteristic] = characteristicValues.maxHealth;
                break;

            case CharacteristicType.RegenerationHP:
                characteristicValues.regenerationHP = Calculate(weaponBaff, isDebaff);
                characteristicMap[weaponBaff.characteristic] = characteristicValues.regenerationHP;
                break;

            case CharacteristicType.Dodge:
                characteristicValues.dodge = Calculate(weaponBaff, isDebaff);
                characteristicMap[weaponBaff.characteristic] = characteristicValues.dodge;
                break;

            case CharacteristicType.Armor:
                characteristicValues.armor = Calculate(weaponBaff, isDebaff);
                characteristicMap[weaponBaff.characteristic] = characteristicValues.armor;
                break;

            case CharacteristicType.MoveSpeed:
                characteristicValues.moveSpeed = Calculate(weaponBaff, isDebaff);
                characteristicMap[weaponBaff.characteristic] = characteristicValues.moveSpeed;
                break;

            case CharacteristicType.AttackSpeed:
                characteristicValues.attackSpeed = Calculate(weaponBaff, isDebaff);
                characteristicMap[weaponBaff.characteristic] = characteristicValues.attackSpeed;
                break;

            case CharacteristicType.Damage:
                characteristicValues.damage = Calculate(weaponBaff, isDebaff);
                characteristicMap[weaponBaff.characteristic] = characteristicValues.damage;
                break;

            case CharacteristicType.MeleeDamage:
                characteristicValues.meleeDamage = Calculate(weaponBaff, isDebaff);
                characteristicMap[weaponBaff.characteristic] = characteristicValues.meleeDamage;
                break;

            case CharacteristicType.RangeDamage:
                characteristicValues.rangeDamage = Calculate(weaponBaff, isDebaff);
                characteristicMap[weaponBaff.characteristic] = characteristicValues.rangeDamage;
                break;

            case CharacteristicType.ChanceOfCrit:
                characteristicValues.critChance = Calculate(weaponBaff, isDebaff);
                characteristicMap[weaponBaff.characteristic] = characteristicValues.critChance;
                break;

            case CharacteristicType.MagneticRadius:
                characteristicValues.magneticRadius = Calculate(weaponBaff, isDebaff);
                characteristicMap[weaponBaff.characteristic] = characteristicValues.magneticRadius;
                break;

            default:
                throw new NotSupportedException($"Item {gameObject.name} has not supported characteristic {weaponBaff}");
        }
    }

    private float Calculate(WeaponBaff weaponBaff, bool isDebaff)
    {
        WeaponTemplateData data = weaponTemplate.GetTemplateDataForSpecificTier(tier) as WeaponTemplateData;
        float additionalCharacteristicStrength = data.additionalCharacteristicStrength;

        if (isDebaff)
        {
            float result = weaponBaff.value * additionalCharacteristicStrength * (-1);
            return Mathf.Round(result * 100.0f) * 0.01f;
        }
        else
        {
            float result = weaponBaff.value * additionalCharacteristicStrength;
            return Mathf.Round(result * 100.0f) * 0.01f;
        }
    }

    [ContextMenu("CalculateAndSynchronize")]
    private void CalculateAndSynchronize()
    {
        tier = TierType.FirstTier;
        CalculateAllCharacteristics();
        SynchronizeComponents();
    }
}
