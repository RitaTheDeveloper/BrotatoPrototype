using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIShop : MonoBehaviour
{
    public static UIShop instance; 

    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private TextMeshProUGUI totalAmountOfGoldText;
    [SerializeField] private TextMeshProUGUI totalAmountOfWoodText;
    [SerializeField] private TextMeshProUGUI priceForUpgradeShopTxt;
    [SerializeField] private TextMeshProUGUI priceForRerollTxt;
    [SerializeField] private TextMeshProUGUI numberOfWeapons;
    [SerializeField] private TextMeshProUGUI shopLevelValue;
    [SerializeField] private CharacteristicsUI characteristicsUI;
    [Space(20)]
    [SerializeField] private Transform panelOfWeapons;
    [SerializeField] private Transform panelOfItems;
    [SerializeField] private Transform panelItemForSale;
    [SerializeField] private GameObject slotForWeaponPrefab;
    [SerializeField] private GameObject slotForItemPrefab;
    [SerializeField] private GameObject weaponElementPrefab;
    [SerializeField] private GameObject itemElementPrefab;
    [SerializeField] private GameObject weaponInfoPrefab;
    [SerializeField] private GameObject itemInfoPrefab;
    [SerializeField] private Transform canvas;
    [SerializeField] private float XmovePosOfInfoPanel;
    [SerializeField] private float YmovePosOfInfoPanel;
    [SerializeField] private Transform positionOfInfoPanel;
    [SerializeField] private SlotItemForSaleData itemSlotForSalePrefab;
    [SerializeField] private List<SlotItemForSaleData> listOfPrefabsForItemsForSale;
    [SerializeField] private int maxAmountOfItems = 16;
    private List<SlotItemForSaleData> items = new List<SlotItemForSaleData>();

    public List<Transform> listSlotsOfWeapons = new List<Transform>();
    List<Transform> listSlotsOfItems = new List<Transform>();

    public ShopController shopController;

    private GameObject _currentInfoItem = null;

    private int maxCountWeapons { set; get; }

    private void Awake()
    {
        instance = this;
       // shopController = GetComponent<ShopController>();
        GetComponentsInChildren<SlotItemForSaleData>(items);
        //CreateItemsSlotsForSale(4);
    }

    public void UpdateUIShop()
    {
        UpdateUICharacteristics();
        shopController.Init();
        CreateItemsSlotsForSale(shopController.GetSlotCount());
        shopController.OnShowUI();
        UpdateItemsForSale();
        shopController.UpdateShop();
    }

    public void SetWaveNumberText(int _waveNumber)
    {
        waveNumberText.text = "волна " + _waveNumber.ToString();
    }

    public void SetTotalAmountOfGoldText(int _totalAmountOfGold)
    {
        totalAmountOfGoldText.text = _totalAmountOfGold.ToString();
    }

    public void SetPriceForUpgradeShopText(int _price)
    {
        priceForUpgradeShopTxt.text = _price.ToString();
    }

    public void SetPriceForRerollText(int _price)
    {
        priceForRerollTxt.text = _price.ToString();
    }

    public void CreateSlotsForWeapons(int _maxNumberOfWeapons)
    {
        maxCountWeapons = _maxNumberOfWeapons;

        DestroyAllSlotsForWeapons();

        List<Weapon> wl = shopController.GetWeaponController().GetAllWeapons();

        for (int i = 0; i < _maxNumberOfWeapons; i++)
        {
            GameObject slot = Instantiate(slotForWeaponPrefab, panelOfWeapons);
            listSlotsOfWeapons.Add(slot.transform);            
        }
    }

    public void DestroyAllSlotsForWeapons()
    {
        foreach(Transform child in panelOfWeapons.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }

        listSlotsOfWeapons.Clear();
    }

    public void CreateWeaponElements(List<Weapon> _currentWeapons)
    {
        if (_currentWeapons.Count > 0)
        {
            for (int i = 0; i < _currentWeapons.Count; i++)
            {
                GameObject weaponElement = Instantiate(weaponElementPrefab, listSlotsOfWeapons[i]);
                weaponElement.GetComponent<WeaponSlot>().AddItem(_currentWeapons[i].GetComponent<ItemShopInfo>());
            }
        }        
    }

    public void DeleteAllWeaponElements()
    {
        foreach(Transform weaponSlot in panelOfWeapons.GetComponentInChildren<Transform>())
        {
            foreach(Transform weaponElement in weaponSlot)
            {
                if (weaponElement)
                {
                    Destroy(weaponElement.gameObject);
                }
            }
        }
    }

    public void CreateSlotsForItems()
    {
        DestroyAllSlotsForItems();

        int count = shopController.GetInventory().Count;
        if (count < maxAmountOfItems)
            count = maxAmountOfItems;

        for (int i = 0; i < count; i++)
        {
            GameObject slot = Instantiate(slotForItemPrefab, panelOfItems);
            listSlotsOfItems.Add(slot.transform);
        }
    }

    public void DestroyAllSlotsForItems()
    {
        foreach (Transform child in panelOfItems.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }

        listSlotsOfItems.Clear();
    }

    public void CreateItemsElements(List<StandartItem> _items)
    {
        if (items.Count > listSlotsOfItems.Count)
        {
            CreateSlotsForItems();
        }
        for (int i = 0; i < _items.Count; i++)
        {
            GameObject itemElement = Instantiate(itemElementPrefab, listSlotsOfItems[i]);
            itemElement.GetComponent<ItemSlot>().AddItem(_items[i].GetComponent<ItemShopInfo>());
        }
    }

    public void OnCreateShopInterface()
    {
        totalAmountOfGoldText.text = shopController.GetPlayerInventory().GetMoney().ToString();
        totalAmountOfWoodText.text = shopController.GetPlayerInventory().GetWood().ToString();
        priceForRerollTxt.text = shopController.GetRerollCost().ToString();
        priceForUpgradeShopTxt.text = shopController.GetShopLevelUpCost().ToString();

        priceForRerollTxt.text = shopController.GetRerollCost().ToString();
        //shopController.CalculateDropChance();
        //CreateItemsSlotsForSale(4);
        CreateItemsSlotsForSale(shopController.GetSlotCount());
        shopController.PickItemsForSale();
        ShowItemsForSale();
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMovement(false);
        }
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.PlayShopMusic();
        }
    }

    public void LockSlot(int slotNumber)
    {
        if (shopController.IsSlotSold(slotNumber))
        {
            return;
        }
        shopController.LockItem(slotNumber);
        string value = "";
        if (shopController.StotIsLocked(slotNumber))
        {
            value = "Unlock";
        }
        else
        {
            value = "Lock";
        }
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].SlotNumber == slotNumber)
            {
                items[i].lockButtonText.text = value;
            }
        }
    }

    public void LevelUpClick()
    {
        if (shopController.UpgrateShop())
        {
            totalAmountOfWoodText.text = shopController.GetPlayerInventory().GetWood().ToString();
            priceForUpgradeShopTxt.text = shopController.GetShopLevelUpCost().ToString();
            DisplayLevelShop(shopController.GetShopLevel());
            UpdateItemsForSale();
        }
    }

    public void DisplayLevelShop(int value)
    {
        shopLevelValue.text = "(уровень " + value.ToString() + ")";
    }

    public void RerollClick()
    {
        if (shopController.RerollShop())
        {
            shopController.ResetsSlots();
            CreateItemsSlotsForSale(shopController.GetSlotCount());
            totalAmountOfWoodText.text = shopController.GetPlayerInventory().GetWood().ToString();
            priceForRerollTxt.text = shopController.GetRerollCost().ToString();
            ShowItemsForSale();
            PlayRerollSound();
        }
    }

    public void UpdateItemsForSale()
    {
        CreateItemsSlotsForSale(shopController.GetSlotCount());
        shopController.PickItemsForSale();
        ShowItemsForSale();
        shopController.ResetsSlots();
        totalAmountOfWoodText.text = shopController.GetPlayerInventory().GetWood().ToString();
        priceForRerollTxt.text = shopController.GetRerollCost().ToString();
        for (int i = 0; i < items.Count; i++)
        {
            items[i].gameObject.SetActive(true);
        }
    }

    void ShowItemsForSale()
    {
        Dictionary<int, string> items_to_slot = shopController.GetItemsForSale();

        for (int i = 0; i < items.Count; i++)
        {
            items[i].SlotEntytiID = items_to_slot[items[i].SlotNumber];
            if (shopController.IsWeapon(items[i].SlotEntytiID))
            {
                ItemShopInfo w = shopController.GetUiInfo(items[i].SlotEntytiID);
                items[i].textName.text = w.NameWeapon;
                items[i].textType.text = w.TypeWeapon;
                items[i].textCost.text = w.GetPrice(shopController.GetCurrentWawe()).ToString();
                items[i].image.sprite = w.IconWeapon;
                items[i].backgroud.color = w.LevelItem.BackgroundColor;
               // w.DisplayCharacteristicsOfWeapon();
                items[i].description.text = w.Description;
                items[i].buyBtn.onClick.RemoveAllListeners();
                items[i].OnClickBuyItem();
            }
            else if (shopController.IsItem(items[i].SlotEntytiID))
            {
                StandartItem it = shopController.GetItem(items[i].SlotEntytiID);
                items[i].textName.text = it.ShopInfoItem.NameWeapon;
                items[i].textType.text = it.ShopInfoItem.TypeWeapon;
                items[i].textCost.text = it.GetPrice(shopController.GetCurrentWawe()).ToString();
                items[i].image.sprite = it.ShopInfoItem.IconWeapon;
                items[i].backgroud.color = it.ShopInfoItem.LevelItem.BackgroundColor;               
                items[i].description.text = it.ShopInfoItem.Description;
                items[i].buyBtn.onClick.RemoveAllListeners();
                items[i].OnClickBuyItem();
            }
        }
    }

    public void ButtonBuySlot(int slotNumber)
    {
        if (shopController.IsSlotSold(slotNumber))
        {
            return;
        }
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].SlotNumber == slotNumber)
            {
                if (shopController.BuyItem(items[i].SlotEntytiID))
                {
                    //Destroy(items[i].gameObject);
                    listOfPrefabsForItemsForSale[i].GetComponent<SlotItemForSaleData>().PotOff();
                    //items.RemoveAt(i);
                    shopController.SoldSlot(slotNumber);
                }
                break;
            }
        }
        totalAmountOfGoldText.text = shopController.GetPlayerInventory().GetMoney().ToString();
        priceForRerollTxt.text = shopController.GetRerollCost().ToString();
        UpdateNumberOfCurrentWeapons(shopController.GetWeaponController().GetAllWeapons().Count, maxCountWeapons);
    }

    private void DisplayItemSold(int index)
    {
        listOfPrefabsForItemsForSale[index].GetComponent<SlotItemForSaleData>().PotOff();
    }


    public void UpdateNumberOfCurrentWeapons(int numberOfCurrentWeapons, int numberOfMaxweapons)
    {
        numberOfWeapons.text = "оружия (" + numberOfCurrentWeapons.ToString() + "/" + numberOfMaxweapons + ")";
    }

    public void ButtonSoldSlot(string name)
    {
        shopController.SellItem(name);
        totalAmountOfGoldText.text = shopController.GetPlayerInventory().GetMoney().ToString();
        UpdateNumberOfCurrentWeapons(shopController.GetWeaponController().GetAllWeapons().Count, maxCountWeapons);
    }

    public void DisplayItemInfoWithBtn(ItemShopInfo _info, Vector2 btnPosition)
    {
        DestroyItemInfo();
        //btnPosition.x += XmovePosOfInfoPanel;
        //btnPosition.y += YmovePosOfInfoPanel;
        _currentInfoItem = Instantiate(weaponInfoPrefab, positionOfInfoPanel.position, Quaternion.identity, canvas);
        _currentInfoItem.GetComponent<ItemInfoPanelWithSellBtn>().SetUp(_info);
    }

    public void DisplayItemInfoWithoutBtn(ItemShopInfo _info, Vector2 btnPosition)
    {
        DestroyItemInfo();
        //btnPosition.x += XmovePosOfInfoPanel;
        //btnPosition.y += YmovePosOfInfoPanel;
        _currentInfoItem = Instantiate(itemInfoPrefab, positionOfInfoPanel.position, Quaternion.identity, canvas);
        _currentInfoItem.GetComponent<ItemInfoPanelWithoutSellBtn>().SetUp(_info);
    }

    public void DestroyItemInfo()
    {
        if(_currentInfoItem != null)
        {
            Destroy(_currentInfoItem.gameObject);
        }
    }

    // этот метод нужен, если хотим создавать кол-во предлагаемых предметов динамически 
    public void CreateItemsSlotsForSale(int _number)
    {
        //for (int i = 0; i < items.Count; i++)
        //{
        //    Destroy(items[i].gameObject);
        //}
        //items.Clear();
        //for (int i = 0; i <_number; i++)
        //{
        //    GameObject itemSlotForSale = Instantiate(itemSlotForSalePrefab.gameObject, panelItemForSale);
        //    SlotItemForSaleData slot = itemSlotForSale.GetComponent<SlotItemForSaleData>();
        //    slot.SlotNumber = i;
        //    items.Add(itemSlotForSale.GetComponent<SlotItemForSaleData>());           
        //}

        items.Clear();
        for (int i = 0; i < listOfPrefabsForItemsForSale.Count; i++)
        {
            listOfPrefabsForItemsForSale[i].PotOff();
        }
        for (int i = 0; i < _number; i++)
        {
            items.Add(listOfPrefabsForItemsForSale[i]);
            listOfPrefabsForItemsForSale[i].PotOn();
        }
    }

    public void UpdateUICharacteristics()
    {
        PlayerCharacteristics playerCharacteristics = GameManager.instance.player.GetComponent<PlayerCharacteristics>();
        characteristicsUI.UpdateCharacterisctics(playerCharacteristics);
    }

    private void PlayRerollSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("RerollShop");
        }
    }

    private void PlayBackgroundMusic()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayShopBackGround(true);
        }
    }

    private void StopBackGroudMusic()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayShopBackGround(false);
        }
    }
}
