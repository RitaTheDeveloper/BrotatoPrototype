
using System.Collections.Generic;
using UnityEngine;

public interface IShopController
{
    void PickItemsForSale();

    void CalculateDropChance();

    Dictionary<int, string> GetItemsForSale();

    void LockItem(int slot);

    bool SellItem(string itemID);

    bool BuyItem(string itemID);

    void UpgrateShop();

    bool IsItem(string id);

    bool IsWeapon(string id);

    Weapon GetWeapon(string id);

    StandartItem GetItem(string id);

}
