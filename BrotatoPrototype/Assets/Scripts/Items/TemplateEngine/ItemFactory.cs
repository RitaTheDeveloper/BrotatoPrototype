
using System;
using UnityEngine;

public class ItemFactory : IItemFactory
{
    public Transform parent;

    public Item CreateItem(Item baseItem, TierType tier)
    {
        switch (tier)
        {
            case TierType.FirstTier:
                Item itemT1 = baseItem.Initialize(TierType.FirstTier, parent);
                itemT1.SynchronizeComponents();
                return itemT1;
                 
            case TierType.SecondTier:
                Item itemT2 = baseItem.Initialize(TierType.SecondTier, parent);
                itemT2.SynchronizeComponents();
                return itemT2;

            case TierType.ThirdTier:
                Item itemT3 = baseItem.Initialize(TierType.ThirdTier, parent);
                itemT3.SynchronizeComponents();
                return itemT3;

            case TierType.FourthTier:
                Item itemT4 = baseItem.Initialize(TierType.FourthTier, parent);
                itemT4.SynchronizeComponents();
                return itemT4;

            default: 
                throw new NotSupportedException($"item of type {baseItem} : {tier} not supported");
        }

    }

}
