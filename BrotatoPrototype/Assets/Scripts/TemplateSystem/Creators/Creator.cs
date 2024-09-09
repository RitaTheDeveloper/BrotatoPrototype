using System;
using UnityEngine;

public class Creator : ICreator
{
    private BaseItem Create(BaseItem baseItem, TierType tier)
    {
        switch (tier)
        {
            case TierType.FirstTier:
                BaseItem itemT1 = baseItem.Initialize(TierType.FirstTier);
                return itemT1;

            case TierType.SecondTier:
                BaseItem itemT2 = baseItem.Initialize(TierType.SecondTier);
                return itemT2;

            case TierType.ThirdTier:
                BaseItem itemT3 = baseItem.Initialize(TierType.ThirdTier);
                return itemT3;

            case TierType.FourthTier:
                BaseItem itemT4 = baseItem.Initialize(TierType.FourthTier);
                return itemT4;

            default:
                throw new NotSupportedException($"Item of type {baseItem} : {tier} not supported");
        }

    }

    public Item CreateItem(Item baseItem, TierType tier)
    {
        Item itemToReturn = Create(baseItem, tier) as Item;

        if (itemToReturn == null)
        {
            throw new NullReferenceException($"Dynamic cast failed {baseItem} : {tier} must be Item type");
        }

        return itemToReturn;
    }

    public Weapon CreateWeapon(Weapon baseWeapon, TierType tier)
    {
        Weapon weaponToReturn = Create(baseWeapon, tier) as Weapon;

        if (weaponToReturn == null)
        {
            throw new NullReferenceException($"Dynamic cast failed {baseWeapon} : {tier} must be Weapon type");
        }

        return weaponToReturn;
    }

    public BuffPerLevel CreateBuffPerLevel(BuffPerLevel buffPerLvl, TierType tier)
    {
        BuffPerLevel itemToReturn = Create(buffPerLvl, tier) as BuffPerLevel;

        if (itemToReturn == null)
        {
            throw new NullReferenceException($"Dynamic cast failed {buffPerLvl} : {tier} must be Item type");
        }

        return itemToReturn;
    }

}
