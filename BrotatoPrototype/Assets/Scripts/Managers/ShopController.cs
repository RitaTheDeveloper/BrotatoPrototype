using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour, IShopController
{
    public int ShopSizeList = 4;
    public int ShopLevel = 1;
    public List<StandartItem> ItemList = new List<StandartItem>();
    public Dictionary<int, StandartItem> SaleItemsDict = new Dictionary<int, StandartItem>();
    public Dictionary<int, bool> LockItemsDict = new Dictionary<int, bool>();

    public void BuyItem(string itemID)
    {
        throw new System.NotImplementedException();
    }

    public void CalculateDropChance()
    {
        throw new System.NotImplementedException();
    }

    public Dictionary<string, StandartItem> GetItemsForSale()
    {
        throw new System.NotImplementedException();
    }

    public void LockItem(string itemID)
    {
        throw new System.NotImplementedException();
    }

    public void PickItemsForSale()
    {
        throw new System.NotImplementedException();
    }

    public void SellItem(string itemID)
    {
        throw new System.NotImplementedException();
    }

    public void UpgrateShop()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
