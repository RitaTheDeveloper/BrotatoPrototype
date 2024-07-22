using HighlightPlus;
using System;
using UnityEngine;

public class Weapon : BaseItem
{
    [Space]
    [Header("Weapon Template")]
    [SerializeField] private WeaponTemplate weaponTemplate;

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

    [ContextMenu("CalculateAndSynchronize")]
    public void CalculateAndSynchronize()
    {
        tier = TierType.FirstTier;
        CalculateAllCharacteristics();
        SynchronizeComponents();
    }
}
