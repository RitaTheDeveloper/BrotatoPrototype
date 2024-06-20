
using System;

public class ItemFactory : IItemFactory
{
    public Item CreateItem(Item baseItem, TierType tier)
    {
        switch (tier)
        {
            case TierType.FirstTier:
                return baseItem.Initialize(TierType.FirstTier);//new Item(TierType.FirstTier);
                 
            case TierType.SecondTier:
                return baseItem.Initialize(TierType.SecondTier);//new Item(TierType.SecondTier);

            case TierType.ThirdTier:
                return baseItem.Initialize(TierType.ThirdTier);//new Item(TierType.ThirdTier);

            case TierType.FourthTier:
                return baseItem.Initialize(TierType.FourthTier);// new Item(TierType.FourthTier);

            default: 
                throw new NotSupportedException($"item of type {baseItem} : {tier} not supported");
        }

    }

    public void SynchronizeAllCharacteristics(Item item, TierType tier)
    {
        throw new System.NotImplementedException();
    }
}
