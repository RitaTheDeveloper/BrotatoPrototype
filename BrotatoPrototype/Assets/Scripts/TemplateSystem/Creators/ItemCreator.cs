using System;
using UnityEngine;

public class ItemCreator : Creator
{
    public Item CreateItem(Item item, TierType tier)
    {
        Item itemToReturn = Create(item, tier) as Item;

        if (itemToReturn == null)
        {
            throw new NullReferenceException($" {item} : {tier} must be Item type");
        }

        return itemToReturn;
    }
}
