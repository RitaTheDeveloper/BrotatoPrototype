using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    private readonly IItemFactory itemFactory = new ItemFactory();

    [SerializeField] List<Item> baseItemList = new List<Item>();
    [SerializeField] List<StandartItem> itemList = new List<StandartItem>();
    [SerializeField] ShopController shopController;

    private void Start()
    {
        if(shopController == null)
        {
            shopController = FindObjectOfType<ShopController>();
        }

        CreateItems();
        shopController.ItemList = itemList;
    }

    private void CreateItems()
    {
        Transform parent = transform;
        foreach (Item item in baseItemList)
        {
            Item itemT1 = itemFactory.CreateItem(item, TierType.FirstTier);
            itemFactory.SetParentItem(itemT1, parent);
            itemList.Add(itemT1.GetComponent<StandartItem>());

            Item itemT2 = itemFactory.CreateItem(item, TierType.SecondTier);
            itemFactory.SetParentItem(itemT2, parent);
            itemList.Add(itemT2.GetComponent<StandartItem>());

            Item itemT3 = itemFactory.CreateItem(item, TierType.ThirdTier);
            itemFactory.SetParentItem(itemT3, parent);
            itemList.Add(itemT3.GetComponent<StandartItem>());

            Item itemT4 = itemFactory.CreateItem(item, TierType.FourthTier);
            itemFactory.SetParentItem(itemT4, parent);
            itemList.Add(itemT4.GetComponent<StandartItem>());
        }
    }
}
