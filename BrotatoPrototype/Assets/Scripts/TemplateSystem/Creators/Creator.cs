using System;
using UnityEngine;

public abstract class Creator
{
    protected BaseItem Create(BaseItem baseItem, TierType tier)
    {
        switch (tier)
        {
            case TierType.FirstTier:
                BaseItem itemT1 = baseItem.Initialize(TierType.FirstTier);
                itemT1.SynchronizeComponents();
                return itemT1;

            case TierType.SecondTier:
                BaseItem itemT2 = baseItem.Initialize(TierType.SecondTier);
                itemT2.SynchronizeComponents();
                return itemT2;

            case TierType.ThirdTier:
                BaseItem itemT3 = baseItem.Initialize(TierType.ThirdTier);
                itemT3.SynchronizeComponents();
                return itemT3;

            case TierType.FourthTier:
                BaseItem itemT4 = baseItem.Initialize(TierType.FourthTier);
                itemT4.SynchronizeComponents();
                return itemT4;

            default:
                throw new NotSupportedException($"Item of type {baseItem} : {tier} not supported");
        }

    }

    public void SetParentItem(BaseItem item, Transform parent)
    {
        item.transform.SetParent(parent);
    }
}
