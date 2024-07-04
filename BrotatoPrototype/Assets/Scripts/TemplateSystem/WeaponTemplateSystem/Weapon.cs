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
    [SerializeField] private float damage = 30f;
    [Min(0.001f)]
    [SerializeField] private float critChance = 0.02f;
    [Min(0.01f)]
    [SerializeField] private float reductionCoeff = 1f;

    [ReadOnlyInspector] [SerializeField] private float attackSpeed;

    public override void SynchronizeComponents()
    {
        SynchronizeItemShopInfo();
    }

    [ContextMenu("CalculateAttackSpeed")]
    private void CalulateAttackSpeed()
    {
        WeaponTemplateData weaponTemplateData = weaponTemplate.GetTemplateDataForSpecificTier(tier) as WeaponTemplateData;
        float damagePerSecond = weaponTemplateData.damagePerSecond;
        float critStrength = weaponTemplate.GetCritStrength();

        attackSpeed = damagePerSecond / (damage * (1 - critChance) + damage * critChance * critStrength) / reductionCoeff;
    }

    [ContextMenu("SynchronizeItemShopInfo")]
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
        throw new System.NotImplementedException();
    }
}
