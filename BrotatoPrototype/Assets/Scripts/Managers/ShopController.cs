using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEditor.Progress;

public class ShopController : MonoBehaviour, IShopController
{
    public static ShopController instance;
    public int ShopSizeList = 4;
    public int ShopLevel = 0;
    [Tooltip("Максимальный уровень магазина:")]
    public int ShopMaxLevel = 5;
    [Tooltip("Стоимость прокачки магазина:")]
    public List<int> LevelUpCost = new List<int>();

    public List<StandartItem> ItemList = new List<StandartItem>();
    public List<Weapon> WeaponList = new List<Weapon>();
    public Dictionary<int, List<string>> LevelToItems = new Dictionary<int, List<string>>();
    public Dictionary<int, List<string>> LevelToWeapons = new Dictionary<int, List<string>>();

    public Dictionary<string, StandartItem> SaleItemsDict = new Dictionary<string, StandartItem>();
    public Dictionary<string, Weapon> WeaponsDict = new Dictionary<string, Weapon>();

    public Dictionary<int, string> SlotItems = new Dictionary<int, string>();
    public Dictionary<int, bool> LockItemsDict = new Dictionary<int, bool>();
    public Dictionary<int, bool> SoldItemsDict = new Dictionary<int, bool>();

    [Tooltip("Структура шаса:")]
    [SerializeField] public List<RareItemsDataStruct> RareData;
    [Tooltip("Удача магазина от уровня:")]
    [SerializeField] public List<float> ShopChance;
    [Tooltip("Шанс оружия на слот после 5 уровня:")]
    [SerializeField] public int LateWeaponChance = 35;

    private List<float> LevelsChance = new List<float>();
    private List<float> accumulateChance = new List<float>();

    [Tooltip("Базовая стоимость реролла:")]
    public int BaseRerollCost = 15;

    [SerializeField] UIShop uiShop;
    [SerializeField] DataForShop dataForShop;

    WeaponController weaponController;
    private List<Weapon> weaponsList;
    private PlayerInventory playerInventory;
    private int currentWave;

    private void Awake()
    {
        instance = this;

        InitRareStorage();
        ListStorageToDict();
        weaponController = dataForShop.weaponController;
        weaponsList = weaponController.GetAllWeapons();
        playerInventory = dataForShop.playerInventory;
        currentWave = dataForShop.waveNumber;

        for (int i = 0; i < ShopSizeList; i++)
        {
            LockItemsDict[i] = false;
            SoldItemsDict[i] = false;
        }
    }

    void Start()
    {
        uiShop.SetWaveNumberText(currentWave);
        // uiShop.CreateItemsSlotsForSale(4);
        uiShop.UpdateNumberOfCurrentWeapons(GetWeaponController().GetAllWeapons().Count, GetWeaponController().GetMaxNumberOfweapons());
        uiShop.CreateSlotsForWeapons(dataForShop.weaponController.GetMaxNumberOfweapons());
        uiShop.CreateSlotsForItems();
        uiShop.CreateWeaponElements(dataForShop.weaponController.GetAllWeapons());
        uiShop.CreateItemsElements(dataForShop.playerInventory.inventory);
        uiShop.OnCreateShopInterface();

        for (int i = 0; i < weaponsList.Count; i++)
        {
            weaponsList[i].GetComponent<ItemShopInfo>().GetPrice(dataForShop.waveNumber);
        }
    }

    public bool BuyItem(string itemID)
    {
        if (SaleItemsDict.ContainsKey(itemID))
        {
            if (playerInventory.HaveNeedCost(SaleItemsDict[itemID].ShopInfoItem.Price))
            {
                playerInventory.ChangeMoney(SaleItemsDict[itemID].GetPrice(currentWave) * -1);
                playerInventory.AddItem(SaleItemsDict[itemID]);
                uiShop.CreateItemsElements(playerInventory.inventory);
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (WeaponsDict.ContainsKey(itemID))
        {
            if (playerInventory.HaveNeedCost(WeaponsDict[itemID].GetComponent<ItemShopInfo>().Price) && weaponsList.Count < weaponController.GetMaxNumberOfweapons())
            {
                playerInventory.ChangeMoney(WeaponsDict[itemID].GetComponent<ItemShopInfo>().GetPrice(currentWave) * -1);
                weaponsList.Add(WeaponsDict[itemID]);
                //uiShop.CreateSlotsForWeapons(dataForShop.maxNumberOfWeapons);
                uiShop.CreateWeaponElements(weaponController.GetAllWeapons());
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
        int maxRareLevel = 0;
        maxRareLevel = LevelToItems.Keys.Max();
        if (maxRareLevel < LevelToWeapons.Keys.Max()) {
            maxRareLevel = LevelToWeapons.Keys.Max();
        }
        LevelsChance = new List<float>(maxRareLevel);
        accumulateChance = new List<float>(maxRareLevel);
        for (int i = 0; i < maxRareLevel; i++)
        {
            LevelsChance.Add(0);
            accumulateChance.Add(0);    
        }

        for (int i = (maxRareLevel - 1); i >= 0; i--)
        {
            if (currentWave + 1 < RareData[i].firstWave)
            {
                LevelsChance[i] = 0;
                continue;
            }
            float value = ((RareData[i].waveChance * (currentWave + 1 - RareData[i].minimalWave)) + RareData[i].baseChance) * ((100 + ShopChance[ShopLevel]) / 100);
            for (int j = i + 1; j < maxRareLevel; j++)
            {
                value -= LevelsChance[j];
            }
            LevelsChance[i] = value;
        }
        accumulateChance[0] = LevelsChance[0] * 100.0f;
        for (int i = 1; i < LevelsChance.Count; i++)
        {
            accumulateChance[i] = accumulateChance[i - 1] + LevelsChance[i] * 100;
        }
    }

    public Dictionary<int, string> GetItemsForSale()
    {
        return SlotItems;
    }

    public void LockItem(int slot)
    {
        LockItemsDict[slot] = !LockItemsDict[slot];
    }

    public void PickItemsForSale()
    {
        //вывбор оружия в зависимости от уровня волны
        int wave = currentWave;
        //1 и 2 уровень
        if (wave == 1 || wave == 2)
        {
            int weapon = 0;
            int item = 0;
            for (int i = 0; i < ShopSizeList; i++)
            {
                if (LockItemsDict[i])
                    continue;
                if (weapon == 2)
                {
                    int random = Random.Range(0, 100 * 100);
                    int level = 0;
                    for (int j = 0; j < accumulateChance.Count; j++)
                    {
                        if (random < accumulateChance[j])
                        {
                            level = j;
                            break;
                        }
                    }
                    level += 1;
                    SlotItems[i] = LevelToItems[level][Random.Range(0, LevelToItems[level].Count - 1)];
                    item++;
                }
                else if (item == 2)
                {
                    int random = Random.Range(0, 100 * 100);
                    int level = 0;
                    for (int j = 0; j < accumulateChance.Count; j++)
                    {
                        if (random < accumulateChance[j])
                        {
                            level = j;
                            break;
                        }
                    }
                    level += 1;
                    SlotItems[i] = LevelToWeapons[level][Random.Range(0, LevelToWeapons[level].Count - 1)];
                    weapon++;
                }
                else
                {
                    int random = Random.Range(0, 100 * 100);
                    int level = 0;
                    for (int j = 0; j < accumulateChance.Count; j++)
                    {
                        if (random < accumulateChance[j])
                        {
                            level = j;
                            break;
                        }
                    }
                    level += 1;
                    int isweapon = Random.Range(0, 1);
                    if (isweapon == 0)
                    {
                        SlotItems[i] = LevelToWeapons[level][Random.Range(0, LevelToWeapons[level].Count - 1)];
                        weapon++;
                    }
                    else
                    {
                        SlotItems[i] = LevelToItems[level][Random.Range(0, LevelToItems[level].Count - 1)];
                        item++;
                    }
                }

            }
        }
        //3 - 5 волна
        else if (wave > 2 && wave <= 5)
        {
            int weapon = 0;
            int item = 0;
            for (int i = 0; i < ShopSizeList; i++)
            {
                if (LockItemsDict[i])
                    continue;
                if (weapon == 1)
                {
                    int random = Random.Range(0, 100 * 100);
                    int level = 0;
                    for (int j = 0; j < accumulateChance.Count; j++)
                    {
                        if (random < accumulateChance[j])
                        {
                            level = j;
                            break;
                        }
                    }
                    level += 1;
                    SlotItems[i] = LevelToItems[level][Random.Range(0, LevelToItems[level].Count - 1)];
                    item++;
                }
                else if (item == 3)
                {
                    int random = Random.Range(0, 100 * 100);
                    int level = 0;
                    for (int j = 0; j < accumulateChance.Count; j++)
                    {
                        if (random < accumulateChance[j])
                        {
                            level = j;
                            break;
                        }
                    }
                    level += 1;
                    SlotItems[i] = LevelToWeapons[level][Random.Range(0, LevelToWeapons[level].Count - 1)];
                    weapon++;
                }
                else
                {
                    int random = Random.Range(0, 100 * 100);
                    int level = 0;
                    for (int j = 0; j < accumulateChance.Count; j++)
                    {
                        if (random < accumulateChance[j])
                        {
                            level = j;
                            break;
                        }
                    }
                    level += 1;
                    int isweapon = Random.Range(0, 1);
                    if (isweapon == 0)
                    {
                        SlotItems[i] = LevelToWeapons[level][Random.Range(0, LevelToWeapons[level].Count - 1)];
                        weapon++;
                    }
                    else
                    {
                        SlotItems[i] = LevelToItems[level][Random.Range(0, LevelToItems[level].Count - 1)];
                        item++;
                    }
                }
            }
        }
        //Все проочие волны
        else
        {
            for (int i = 0; i < ShopSizeList; i++)
            {
                if (LockItemsDict[i])
                    continue;
                int random = Random.Range(0, 100 * 100);
                int level = 0;
                for (int j = 0; j < accumulateChance.Count; j++)
                {
                    if (random < accumulateChance[j])
                    {
                        level = j;
                        break;
                    }
                }
                level += 1;
                int isweapon = Random.Range(0, 100);
                if (isweapon <= LateWeaponChance)
                {
                    SlotItems[i] = LevelToWeapons[level][Random.Range(0, LevelToWeapons[level].Count - 1)];
                }
                else
                {
                    SlotItems[i] = LevelToItems[level][Random.Range(0, LevelToItems[level].Count - 1)];
                }
            }
        }
    }

    public bool SellItem(string itemID)
    {
        if (WeaponsDict.ContainsKey(itemID))
        {    
            int priceForSale = WeaponsDict[itemID].GetComponent<ItemShopInfo>().GetSalePrice();
            playerInventory.MoneyPlayer += priceForSale;
            weaponsList.Remove(WeaponsDict[itemID]);
            return true;
        }
        else if (SaleItemsDict.ContainsKey(itemID))
        {
            playerInventory.MoneyPlayer += SaleItemsDict[itemID].GetPrice(currentWave) * (SaleItemsDict[itemID].ShopInfoItem.DiscountProcent / 100);
            playerInventory.DeleteItem(SaleItemsDict[itemID]);
            return true;
        }
        return false;
    }

    public bool UpgrateShop()
    {
        if (ShopLevel < ShopMaxLevel)
        {

            if (playerInventory.HaveNeedWood(GetShopLevelUpCost()))
            {
                playerInventory.ChangeWood(GetShopLevelUpCost() * -1);
                ShopLevel += 1;
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitRareStorage()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (!LevelToItems.ContainsKey(ItemList[i].ShopInfoItem.LevelItem.level))
            {
                LevelToItems[ItemList[i].ShopInfoItem.LevelItem.level] = new List<string>();
            }
            LevelToItems[ItemList[i].ShopInfoItem.LevelItem.level].Add(ItemList[i].IdItem);
        }
        for (int i = 0; i < WeaponList.Count; i++)
        {
            if (!LevelToWeapons.ContainsKey(WeaponList[i].GetComponent<ItemShopInfo>().LevelItem.level))
            {
                LevelToWeapons[WeaponList[i].GetComponent<ItemShopInfo>().LevelItem.level] = new List<string>();
            }
            LevelToWeapons[WeaponList[i].GetComponent<ItemShopInfo>().LevelItem.level].Add(WeaponList[i].GetComponent<ItemShopInfo>().IdWeapon);
        }
    }

    public bool IsItem(string id)
    {
        if (SaleItemsDict.ContainsKey(id))
        {
            return true;
        }
        return false;
    }

    public bool IsWeapon(string id)
    {
        if (WeaponsDict.ContainsKey(id))
        {
            return true;
        }
        return false;
    }

    public Weapon GetWeapon(string id)
    {
        if (WeaponsDict.ContainsKey(id))
        {
            return WeaponsDict[id];
        } 
        return null;
    }

    public StandartItem GetItem(string id)
    {
        if (SaleItemsDict.ContainsKey(id))
        {
            return SaleItemsDict[id];
        }
        return null;
    }

    private void ListStorageToDict()
    {
        for (int i = 0; i < ItemList.Count;i++)
        {
            SaleItemsDict[ItemList[i].IdItem] = ItemList[i];
        }
        for (int i = 0; i < WeaponList.Count; i++)
        {
            WeaponsDict[WeaponList[i].GetComponent<ItemShopInfo>().IdWeapon] = WeaponList[i];
        }
    }

    public int GetShopLevelUpCost()
    {
        return LevelUpCost[ShopLevel];
    }

    public int GetRerollCost()
    {
        if (AllSlotSold())
        {
            return 0;
        }
        return BaseRerollCost;
    }

    public int GetCurrentWawe()
    {
        return dataForShop.waveNumber; 
    }

    public bool StotIsLocked(int slot)
    {
        return LockItemsDict[slot];
    }

    public WeaponController GetWeaponController()
    {
        return weaponController;
    }

    public PlayerInventory GetPlayerInventory()
    {
        return playerInventory;
    }

    public void RerollShop()
    {
        PlayerInventory inventary = dataForShop.playerInventory;
        if (inventary.HaveNeedWood(GetRerollCost()))
        {
            inventary.ChangeWood(GetRerollCost() * -1);
            PickItemsForSale();
        }
    }

    public List<RareItemsDataStruct> GetRareItemsDataStruct()
    {
        return RareData;
    }

    public void SoldSlot(int slot)
    {
        SoldItemsDict[slot] = true;
    }

    public bool AllSlotSold()
    {
        bool r = true;
        for (int i = 0; i < SoldItemsDict.Count; i++)
        {
            r = r && SoldItemsDict[i];
        }
        return r;
    }

    public bool IsSlotSold(int slot)
    {
        return SoldItemsDict[slot];
    }

    public ItemShopInfo GetUiInfo(string id)
    {
        return WeaponsDict[id].GetComponent<ItemShopInfo>();
    }

    public int maxWeaponCount()
    {
        return weaponController.GetMaxNumberOfweapons();
    }

    public void ResetsSlots()
    {
        for (int i = 0; i < ShopSizeList; i++)
        {
            SoldItemsDict[i] = false;
        }
    }

    public int GetShopLevel()
    {
        return ShopLevel;
    }

    public List<StandartItem> GetInventory()
    {
        return playerInventory.inventory;
    }
}
