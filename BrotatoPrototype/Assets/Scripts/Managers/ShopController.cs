using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEditor.Progress;

public class ShopController : MonoBehaviour, IShopController
{
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

    private void Awake()
    {
        InitRareStorage();
        ListStorageToDict();
    }

    void Start()
    {
        uiShop.SetWaveNumberText(dataForShop.waveNumber);
        uiShop.SetTotalAmountOfGoldText(dataForShop.totalAmountOfGold);
        uiShop.SetPriceForUpgradeShopText(dataForShop.priceForUpgradeShop);
        uiShop.SetPriceForRerollText(dataForShop.priceForReroll);
        uiShop.SetNumberOfPossibleWeapons(dataForShop.maxNumberOfWeapons);
        uiShop.CreateSlotsForWeapons(dataForShop.maxNumberOfWeapons);
        uiShop.OnCreateShopInterface();
    }


    public bool BuyItem(string itemID)
    {
        if (SaleItemsDict.ContainsKey(itemID))
        {
            PlayerInventory inventary = dataForShop.playerInventory;
            if (inventary.HaveNeedCost(SaleItemsDict[itemID].Price))
            {
                inventary.ChangeMoney(SaleItemsDict[itemID].GetPrice(GetComponent<GameManager>().WaveCounter) * -1);
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
            WeaponController weaponController = dataForShop.weaponController;
            PlayerInventory inventary = dataForShop.playerInventory;
            if (inventary.HaveNeedCost(WeaponsDict[itemID].Price))
            {
                inventary.ChangeMoney(WeaponsDict[itemID].GetPrice(GetComponent<GameManager>().WaveCounter) * -1);
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
            if (dataForShop.waveNumber + 1 < RareData[i].firstWave)
            {
                LevelsChance[i] = 0;
                continue;
            }
            float value = ((RareData[i].waveChance * (dataForShop.waveNumber + 1 - RareData[i].minimalWave)) + RareData[i].baseChance) * ((100 + ShopChance[ShopLevel]) / 100);
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
        int wawe = dataForShop.waveNumber;
        //1 и 2 уровень
        if (wawe == 1 || wawe == 2)
        {
            int weapon = 0;
            int item = 0;
            for (int i = 0; i < ShopSizeList; i++)
            {
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
                    SlotItems.Add(i, LevelToItems[level][Random.Range(0, LevelToItems[level].Count - 1)]);
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
                    SlotItems.Add(i, LevelToWeapons[level][Random.Range(0, LevelToWeapons[level].Count - 1)]);
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
                        SlotItems.Add(i, LevelToWeapons[level][Random.Range(0, LevelToWeapons[level].Count - 1)]);
                        weapon++;
                    }
                    else
                    {
                        SlotItems.Add(i, LevelToItems[level][Random.Range(0, LevelToWeapons[level].Count - 1)]);
                        item++;
                    }
                }

            }
        }
        //3 - 5 волна
        else if (wawe > 2 && wawe <= 5)
        {
            int weapon = 0;
            int item = 0;
            for (int i = 0; i < ShopSizeList; i++)
            {
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
                    SlotItems.Add(i, LevelToItems[level][Random.Range(0, LevelToWeapons[level].Count - 1)]);
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
                    SlotItems.Add(i, LevelToWeapons[level][Random.Range(0, LevelToWeapons[level].Count - 1)]);
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
                        SlotItems.Add(i, LevelToWeapons[level][Random.Range(0, LevelToWeapons[level].Count - 1)]);
                        weapon++;
                    }
                    else
                    {
                        SlotItems.Add(i, LevelToItems[level][Random.Range(0, LevelToWeapons[level].Count - 1)]);
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
                    SlotItems.Add(i, LevelToWeapons[level][Random.Range(0, LevelToWeapons[level].Count - 1)]);
                }
                else
                {
                    SlotItems.Add(i, LevelToItems[level][Random.Range(0, LevelToWeapons[level].Count - 1)]);
                }
            }
        }
    }

    public bool SellItem(string itemID)
    {
        if (WeaponsDict.ContainsKey(itemID))
        {
            PlayerInventory inventary = dataForShop.playerInventory;
            WeaponController weaponController = dataForShop.weaponController;
            inventary.MoneyPlayer += WeaponsDict[itemID].GetPrice(GetComponent<GameManager>().WaveCounter) * (WeaponsDict[itemID].DiscountProcent / 100);
            weaponController.UnequipGun(WeaponsDict[itemID]);
            return true;
        }
        else if (SaleItemsDict.ContainsKey(itemID))
        {
            PlayerInventory inventary = dataForShop.playerInventory;
            inventary.MoneyPlayer += SaleItemsDict[itemID].GetPrice(GetComponent<GameManager>().WaveCounter) * (SaleItemsDict[itemID].DiscountPercentageItem / 100);
            inventary.DeleteItem(SaleItemsDict[itemID]);
            return true;
        }
        return false;
    }

    public void UpgrateShop()
    {
        PlayerInventory inventary = dataForShop.playerInventory;
        if (inventary.HaveNeedWood(GetShopLevelUpCost()))
        {
            inventary.ChangeWood(GetShopLevelUpCost() * -1);
            ShopLevel += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitRareStorage()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (!LevelToItems.ContainsKey(ItemList[i].LevelItem))
            {
                LevelToItems[ItemList[i].LevelItem] = new List<string>();
            }
            LevelToItems[ItemList[i].LevelItem].Add(ItemList[i].IdItem);
        }
        for (int i = 0; i < WeaponList.Count; i++)
        {
            if (!LevelToWeapons.ContainsKey(WeaponList[i].LevelItem))
            {
                LevelToWeapons[WeaponList[i].LevelItem] = new List<string>();
            }
            LevelToWeapons[WeaponList[i].LevelItem].Add(WeaponList[i].IdWeapon);
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
            WeaponsDict[WeaponList[i].IdWeapon] = WeaponList[i];
        }
    }

    public int GetShopLevelUpCost()
    {
        return LevelUpCost[ShopLevel];
    }

    public int GetRerollCost()
    {
        return BaseRerollCost;
    }

    public int GetCurrentWawe()
    {
        return dataForShop.waveNumber; 
    }
}
