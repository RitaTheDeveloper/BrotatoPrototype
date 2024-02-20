
using System.Collections.Generic;
using UnityEngine;

public interface IShopController
{
    void PickItemsForSale();

    void CalculateDropChance();

    List<string> GetItemsForSale();

    void LockItem(string itemID);

    bool SellItem(string itemID);

    bool BuyItem(string itemID);

    void UpgrateShop();
}
