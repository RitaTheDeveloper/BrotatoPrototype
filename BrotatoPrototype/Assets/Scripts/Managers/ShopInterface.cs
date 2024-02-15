
using System.Collections.Generic;
using UnityEngine;

public interface IShopController
{
    void PickItemsForSale();

    void CalculateDropChance();

    Dictionary<string, StandartItem> GetItemsForSale();

    void LockItem(string itemID);

    void SellItem(string itemID);

    void BuyItem(string itemID);

    void UpgrateShop();
}
