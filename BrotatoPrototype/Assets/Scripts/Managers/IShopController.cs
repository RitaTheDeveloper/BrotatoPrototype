
using System.Collections.Generic;
using UnityEngine;

public interface IShopController
{
    void PickItemsForSale();

    void CalculateDropChance();

    Dictionary<int, string> GetItemsForSale();

    void LockItem(int slot);

    void SoldSlot(int slot);

    bool StotIsLocked(int slot);

    bool AllSlotSold();

    bool IsSlotSold(int slot);

    bool SellItem(string itemID);

    bool BuyItem(string itemID);

    void UpgrateShop();

    void RerollShop();

    bool IsItem(string id);

    bool IsWeapon(string id);

    Weapon GetWeapon(string id);

    ItemShopInfo GetUiInfo(string id);

    StandartItem GetItem(string id);

    int GetShopLevelUpCost();

    int GetRerollCost();

    public int GetCurrentWawe();

    public WeaponController GetWeaponController();

    public PlayerInventory GetPlayerInventory();

    public List<RareItemsDataStruct> GetRareItemsDataStruct();

    public int maxWeaponCount();

    public void ResetsSlots();

    public int GetShopLevel();

    public Dictionary<string, StandartItem> GetInventory();
}
