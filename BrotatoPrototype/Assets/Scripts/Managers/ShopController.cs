using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class ShopController : MonoBehaviour, IShopController
{
    public List<StandartItem> ItemList = new List<StandartItem>();
    public List<BaseWeapon> WeaponList = new List<BaseWeapon>();
    public Dictionary<int, List<string>> LevelToItems = new Dictionary<int, List<string>>();
    public Dictionary<int, List<string>> LevelToWeapons = new Dictionary<int, List<string>>();

    public Dictionary<string, StandartItem> SaleItemsDict = new Dictionary<string, StandartItem>();
    public Dictionary<string, BaseWeapon> WeaponsDict = new Dictionary<string, BaseWeapon>();

    public Dictionary<int, string> SlotItems = new Dictionary<int, string>();
    public Dictionary<int, bool> LockItemsDict = new Dictionary<int, bool>();
    public Dictionary<int, bool> SoldItemsDict = new Dictionary<int, bool>();

    [Tooltip("Структура уровней мгазина")]
    [SerializeField] public List<ShopLevelStruct> ShopLevelStructsStorage = new List<ShopLevelStruct>();

    [Tooltip("Ткущее колличество слотов")]
    [SerializeField] int ShopSizeList = 0;

    [Tooltip("Текущий уровень магазина")]
    [SerializeField] int CurrentShopLevel = 1;

    [Tooltip("Возможная редкость предметов")]
    [SerializeField] public List<RareItemsDataStruct> RareData = new List<RareItemsDataStruct>();

    [Tooltip("Деолтная цена рерола")]
    [SerializeField] public int DefaultRerollPrice = 20;

    [Tooltip("Шаг полвышения цены рерола")]
    [SerializeField] public int StepRerollPrice = 5;

    [Tooltip("Текущая цена рерола")]
    [SerializeField] private int CurrentRerollPrice = 20;

    [Tooltip("Шанс оружия %")]
    [SerializeField] public int WeaponChance = 30;

    [SerializeField] UIShop uiShop;
    [SerializeField] DataForShop dataForShop;

    WeaponController weaponController;
    private GameObject player;
    private List<BaseWeapon> weaponsList;
    private PlayerInventory playerInventory;
    private int currentWave;
    private BaseWeapon kostylWeapon;

    void Start()
    {
        //Init();
        //UpdateShop();
    }   

    public void Init()
    {
        InitRareStorage();
        ListStorageToDict();
        player = GameManager.instance.player;
        weaponController = player.GetComponent<WeaponController>();
        weaponsList = weaponController.GetAllWeapons();
        playerInventory = player.GetComponent<PlayerInventory>();
        currentWave = GameManager.instance.WaveCounter;
        CurrentRerollPrice = DefaultRerollPrice;

        if (ShopLevelStructsStorage[0].slotsData.Count > 0)
            ShopSizeList = ShopLevelStructsStorage[0].slotsData.Count;

        for (int i = 0; i < ShopSizeList; i++)
        {
            LockItemsDict[i] = false;
            SoldItemsDict[i] = false;
        }
    }

    public void ResetShop()
    {
        CurrentShopLevel = 1;
        uiShop.DisplayLevelShop(CurrentShopLevel);
        uiShop.ResetUIShop();
    }

    public void UpdateShop()
    {
        ShopSizeList = ShopLevelStructsStorage[CurrentShopLevel - 1].slotsData.Count;
        uiShop.SetWaveNumberText(currentWave);
        uiShop.UpdateNumberOfCurrentWeapons(GetWeaponController().GetAllWeapons().Count, GetWeaponController().GetMaxNumberOfweapons());
        uiShop.CreateSlotsForWeapons(weaponController.GetMaxNumberOfweapons());
        uiShop.CreateSlotsForItems();
        uiShop.CreateWeaponElements(weaponController.GetAllWeapons());
        uiShop.CreateItemsElements(playerInventory.inventory);
        uiShop.OnCreateShopInterface();
    }

    public bool BuyItem(string itemID)
    {
        if (SaleItemsDict.ContainsKey(itemID))
        {
            if (playerInventory.HaveNeedCost(SaleItemsDict[itemID].ShopInfoItem.GetPrice(currentWave)))
            {
                playerInventory.ChangeMoney(SaleItemsDict[itemID].GetComponent<ItemShopInfo>().GetPrice(currentWave) * -1);
                playerInventory.AddItem(SaleItemsDict[itemID]);
                uiShop.CreateItemsElements(playerInventory.inventory);
                PlaySoundBuySold();
                return true;
            }
            else
            {
                PlayNeedMoreGold();
                StartCoroutine(uiShop.ShowMessage("Недостаточно денег", uiShop.pointsForAttentionWindows[0]));
                return false;
            }
        }
        else if (WeaponsDict.ContainsKey(itemID))
        {
            if (playerInventory.HaveNeedCost(WeaponsDict[itemID].GetComponent<ItemShopInfo>().GetPrice(currentWave)))
            {
                if (weaponsList.Count < weaponController.GetMaxNumberOfweapons())
                {
                    playerInventory.ChangeMoney(WeaponsDict[itemID].GetComponent<ItemShopInfo>().GetPrice(currentWave) * -1);
                    weaponsList.Add(WeaponsDict[itemID]);
                    uiShop.CreateWeaponElements(weaponController.GetAllWeapons());
                    PlaySoundBuySold();
                    return true;
                }
                else
                {
                    PlayMaxSlots();
                    StartCoroutine(uiShop.ShowMessage("Все слоты оружия заполнены", uiShop.pointsForAttentionWindows[1]));
                    return false;
                }
            }
            else
            {
                PlayNeedMoreGold();
                StartCoroutine(uiShop.ShowMessage("Недостаточно денег", uiShop.pointsForAttentionWindows[0]));
                return false;
            }
        }
        return false;
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
        ShopSizeList = ShopLevelStructsStorage[CurrentShopLevel - 1].slotsData.Count;
        ResetsSlots();
        int countWeapon = 0;
        int countItem = 0;
        for (int i = 0; i < ShopLevelStructsStorage[CurrentShopLevel - 1].slotsData.Count; i++)
        {
            bool needWeapon = false;
            bool needItem = false;

            //1 - 5 óðîâåíü
            if (CurrentShopLevel >= 1 && CurrentShopLevel <= 5)
            {

                if (ShopLevelStructsStorage[CurrentShopLevel - 1].slotsData.Count - countWeapon - countItem > 1 || (countWeapon > 0 && countItem > 0))
                {
                    int chance = Random.Range(0, 100);
                    if (chance >= 0 && chance < WeaponChance)
                    {
                        needWeapon = true;
                    }
                    else
                    {
                        needItem = true;
                    }
                }
                else if (countItem > 0)
                {
                    needWeapon = true;
                }
                else if (countWeapon > 0)
                {
                    needItem = true;
                }
            }
            //Âñå ïðî÷èå ðîâíè ìàãàçèíà
            else
            {
                int chance = Random.Range(0, 100);
                if (chance >= 0 && chance < WeaponChance)
                {
                    needWeapon = true;
                }
                else
                {
                    needItem = true;
                }
            }

            if (needWeapon)
            {
                int countLevelRareWeapon = LevelToWeapons[ShopLevelStructsStorage[CurrentShopLevel - 1].slotsData[i].level].Count;
                int weaponNum = Random.Range(0, countLevelRareWeapon);
                SlotItems[i] = LevelToWeapons[ShopLevelStructsStorage[CurrentShopLevel - 1].slotsData[i].level][weaponNum];
                countWeapon++;
            }
            else if (needItem)
            {
                int countLevelRareItem = LevelToItems[ShopLevelStructsStorage[CurrentShopLevel - 1].slotsData[i].level].Count;
                int itenNum = Random.Range(0, countLevelRareItem);
                SlotItems[i] = LevelToItems[ShopLevelStructsStorage[CurrentShopLevel - 1].slotsData[i].level][itenNum];
                countItem++;
            }
        }
    }

    public bool SellItem(string itemID)
    {
        if (WeaponsDict.ContainsKey(itemID))
        {
            ItemShopInfo shopComponent = WeaponsDict[itemID].GetComponent<ItemShopInfo>();
            int priceForSale = shopComponent.GetSalePrice();
            playerInventory.ChangeMoney(priceForSale);

            // дальнейший код нужно уничтожить и сделать по-нормальному

            foreach (BaseWeapon kostyl in weaponsList)
            {
                if (kostyl.gameObject.name == itemID)
                {
                    kostylWeapon = kostyl;
                    break;
                }
            }

            weaponsList.Remove(kostylWeapon);
            PlaySoundBuySold();
            return true;
        }
        else if (SaleItemsDict.ContainsKey(itemID))
        {
            ItemShopInfo shopComponent = SaleItemsDict[itemID].GetComponent<ItemShopInfo>();
            int priceForSale = shopComponent.GetSalePrice();
            playerInventory.ChangeMoney(priceForSale); //SaleItemsDict[itemID].ShopInfoItem.Price - SaleItemsDict[itemID].ShopInfoItem.Price * (SaleItemsDict[itemID].ShopInfoItem.DiscountProcent / 100)
            playerInventory.DeleteItem(SaleItemsDict[itemID]);
            PlaySoundBuySold();
            return true;
        }
        return false;
    }

    public bool UpgrateShop()
    {
        if (CurrentShopLevel < ShopLevelStructsStorage.Count)
        {
            ShopLevelStruct newLevel = ShopLevelStructsStorage[CurrentShopLevel];
            if (playerInventory.HaveNeedWood(newLevel.levelPrice))
            {
                uiShop.FireAnim();
                playerInventory.ChangeWood(newLevel.levelPrice * -1);
                CurrentShopLevel = newLevel.levelNumber;
                CurrentRerollPrice = DefaultRerollPrice;
                ShopSizeList = ShopLevelStructsStorage[CurrentShopLevel - 1].slotsData.Count;

                for (int i = 0; i < ShopSizeList; i++)
                {
                    LockItemsDict[i] = false;
                    SoldItemsDict[i] = false;
                }

                PlaySoundShopLevelUp();

                return true;
            }
            else
            {
                PlayNeedMoreWood();
                StartCoroutine(uiShop.ShowMessage("Недостаточно дерева", uiShop.pointsForAttentionWindows[0]));
            }
        }
        else
        {
            uiShop.ShopIsMax();
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

    public BaseWeapon GetWeapon(string id)
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
        if (CurrentShopLevel < ShopLevelStructsStorage.Count)
        {
            return ShopLevelStructsStorage[CurrentShopLevel].levelPrice;
        }
        else
        {
            uiShop.ShopIsMax();
            return 0;
        }
    }

    public int GetRerollCost()
    {
        return CurrentRerollPrice;
    }

    public int GetCurrentWawe()
    {
        return currentWave; 
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

    public bool RerollShop()
    {
        if (playerInventory.HaveNeedWood(GetRerollCost()))
        {
            playerInventory.ChangeWood(GetRerollCost() * -1);
            CurrentRerollPrice += StepRerollPrice;
            PickItemsForSale();
            return true;
        }
        else
        {
            PlayNeedMoreWood();
            StartCoroutine(uiShop.ShowMessage("Недостаточно дерева", uiShop.pointsForAttentionWindows[0]));
        }
        return false;
    }

    public List<RareItemsDataStruct> GetRareItemsDataStruct()
    {
        return RareData;
    }

    public void SoldSlot(int slot)
    {
        SoldItemsDict[slot] = true;
    }

    //Возвращает true если все слоты куплены
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
        return CurrentShopLevel;
    }

    public List<StandartItem> GetInventory()
    {
        return playerInventory.inventory;
    }

    public void OnShowUI()
    {
        PickItemsForSale();
        CurrentRerollPrice = DefaultRerollPrice;
    }

    public int GetSlotCount()
    {
        return ShopLevelStructsStorage[CurrentShopLevel - 1].slotsData.Count;
    }

    private void PlaySoundShopLevelUp()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("ShopLevelUp");
        }
    }

    private void PlayNeedMoreGold()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("NeedMoreGold");
        }
    }

    private void PlayNeedMoreWood()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("NeedMoreWood");
        }
    }

    private void PlayMaxSlots()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("MaxSlots");
        }
    }

    private void PlaySoundBuySold()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("BuySoldSlot");
        }
    }
}
