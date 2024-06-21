
using System;
using UnityEngine;

public class ItemFactory : IItemFactory
{
    public Item CreateItem(Item baseItem, TierType tier)
    {
        switch (tier)
        {
            case TierType.FirstTier:
                Item itemT1 = baseItem.Initialize(TierType.FirstTier);
                itemT1.SynchronizeComponents();
                return itemT1;
                 
            case TierType.SecondTier:
                Item itemT2 = baseItem.Initialize(TierType.SecondTier);
                itemT2.SynchronizeComponents();
                return itemT2;

            case TierType.ThirdTier:
                Item itemT3 = baseItem.Initialize(TierType.ThirdTier);
                itemT3.SynchronizeComponents();
                return itemT3;

            case TierType.FourthTier:
                Item itemT4 = baseItem.Initialize(TierType.FourthTier);
                itemT4.SynchronizeComponents();
                return itemT4;

            default: 
                throw new NotSupportedException($"item of type {baseItem} : {tier} not supported");
        }

    }

    public void SetParentItem(Item item, Transform parent)
    {
        item.transform.SetParent(parent);
    }

}
