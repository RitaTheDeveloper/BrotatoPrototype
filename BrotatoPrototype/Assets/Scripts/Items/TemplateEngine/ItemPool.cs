using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    private readonly ItemFactory itemFactory = new ItemFactory();

    [SerializeField] List<Item> baseItemList = new List<Item>();
    [SerializeField] List<StandartItem> itemList = new List<StandartItem>();
    [SerializeField] ShopController shopController;

    private void Start()
    {
        if(shopController == null)
        {
            shopController = FindObjectOfType<ShopController>();
        }

        itemFactory.parent = gameObject.transform;

        CreateItems();
        shopController.ItemList = itemList;
    }

    private void CreateItems()
    {
        foreach (Item item in baseItemList)
        {
            Item itemT1 = itemFactory.CreateItem(item, TierType.FirstTier);
            itemList.Add(itemT1.GetComponent<StandartItem>());

            Item itemT2 = itemFactory.CreateItem(item, TierType.SecondTier);
            itemList.Add(itemT2.GetComponent<StandartItem>());

            Item itemT3 = itemFactory.CreateItem(item, TierType.ThirdTier);
            itemList.Add(itemT3.GetComponent<StandartItem>());

            Item itemT4 = itemFactory.CreateItem(item, TierType.FourthTier);
            itemList.Add(itemT4.GetComponent<StandartItem>());
        }
    }
}
