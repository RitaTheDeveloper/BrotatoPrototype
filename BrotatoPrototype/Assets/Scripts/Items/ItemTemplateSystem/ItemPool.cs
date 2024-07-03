using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    private readonly IItemBuilder itemBuilder = new ItemBuilder();

    [SerializeField] List<Item> baseItemList = new List<Item>();
    [SerializeField] List<StandartItem> itemList = new List<StandartItem>();
    [SerializeField] ShopController shopController;

    private void Start()
    {
        if(shopController == null)
        {
            throw new NotSupportedException($"shopController must be valid.");
        }

        CreateItems();
        shopController.ItemList = itemList;
    }

    private void CreateItems()
    {
        Transform parent = transform;
        foreach (Item item in baseItemList)
        {
            Item itemT1 = itemBuilder.CreateItem(item, TierType.FirstTier);
            itemBuilder.SetParentItem(itemT1, parent);
            itemList.Add(itemT1.GetComponent<StandartItem>());

            Item itemT2 = itemBuilder.CreateItem(item, TierType.SecondTier);
            itemBuilder.SetParentItem(itemT2, parent);
            itemList.Add(itemT2.GetComponent<StandartItem>());

            Item itemT3 = itemBuilder.CreateItem(item, TierType.ThirdTier);
            itemBuilder.SetParentItem(itemT3, parent);
            itemList.Add(itemT3.GetComponent<StandartItem>());

            Item itemT4 = itemBuilder.CreateItem(item, TierType.FourthTier);
            itemBuilder.SetParentItem(itemT4, parent);
            itemList.Add(itemT4.GetComponent<StandartItem>());
        }
    }
}
