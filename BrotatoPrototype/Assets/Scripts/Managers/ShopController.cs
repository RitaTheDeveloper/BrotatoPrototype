using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour, IShopController
{
    public int ShopSizeList = 4;
    public int ShopLevel = 1;
    public List<StandartItem> ItemList = new List<StandartItem>();
    public List<Weapon> WeaponList = new List<Weapon>();
    public Dictionary<string, StandartItem> SaleItemsDict = new Dictionary<string, StandartItem>();
    public Dictionary<int, bool> LockItemsDict = new Dictionary<int, bool>();
    public Dictionary<string, Weapon> WeaponsDict = new Dictionary<string, Weapon>();
    [SerializeField] public GameObject Player;


    public bool BuyItem(string itemID)
    {
        if (SaleItemsDict.ContainsKey(itemID))
        {
            PlayerInventory inventary = Player.GetComponent<PlayerInventory>();
            if (inventary.HaveNeedCost(SaleItemsDict[itemID].Price))
            {
                inventary.ChangeMoney(SaleItemsDict[itemID].Price);
                inventary.AddItem(SaleItemsDict[itemID]);
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (WeaponsDict.ContainsKey(itemID))
        {
            WeaponController weaponController = Player.GetComponent<WeaponController>();
            PlayerInventory inventary = Player.GetComponent<PlayerInventory>();
            if (inventary.HaveNeedCost(WeaponsDict[itemID].Price))
            {
                inventary.ChangeMoney(WeaponsDict[itemID].Price);
                weaponController.EquipGun(WeaponsDict[itemID]);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void CalculateDropChance()
    {
        throw new System.NotImplementedException();
    }

    public List<string> GetItemsForSale()
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

    public bool SellItem(string itemID)
    {
        if (WeaponsDict.ContainsKey(itemID))
        {
            PlayerInventory inventary = Player.GetComponent<PlayerInventory>();
            WeaponController weaponController = Player.GetComponent<WeaponController>();
            inventary.MoneyPlayer += WeaponsDict[itemID].Price * (WeaponsDict[itemID].DiscountProcent / 100);
            weaponController.UnequipGun(WeaponsDict[itemID]);
        }
    }

    public void UpgrateShop()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
