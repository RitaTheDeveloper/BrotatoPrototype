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
    [Space(20)]
    [SerializeField] private Transform panelOfWeapons;
    [SerializeField] private Transform panelOfItems;
    [SerializeField] private GameObject slotForWeaponPrefab;
    [SerializeField] private GameObject slotForItemPrefab;
    [SerializeField] private GameObject weaponElementPrefab;
    [SerializeField] private GameObject itemElementPrefab;
    [SerializeField] private GameObject weaponInfoPrefab;
    [SerializeField] private GameObject itemInfoPrefab;
    [SerializeField] private Transform canvas;
    [SerializeField] private float XmovePosOfInfoPanel;
    [SerializeField] private float YmovePosOfInfoPanel;

    List<SlotItemForSaleData> items = new List<SlotItemForSaleData>();

    List<Transform> listSlotsOfWeapons = new List<Transform>();
    List<Transform> listSlotsOfItems = new List<Transform>();


    private IShopController shopController;

    private GameObject _currentInfoItem = null;

    private int maxCountWeapons { set; get; }

    private void Awake()
    {
        instance = this;
        shopController = GetComponent<ShopController>();
        GetComponentsInChildren<SlotItemForSaleData>(items);
    }

    void Start()
    {
        //GetComponentsInChildren<RareItemsDataStruct>(rares);
    }

    public void ChangeUIParametersOfShop()
    {
        SetNumberOfPossibleWeapons(maxCountWeapons);
        SetTotalAmountOfGoldText(689);
        SetPriceForUpgradeShopText(125);
        SetPriceForRerollText(17);
        SetNumberOfPossibleWeapons(8);
    }

    public void SetWaveNumberText(int _waveNumber)
    {
        waveNumberText.text = _waveNumber.ToString() + ")";
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

    public void SetNumberOfPossibleWeapons(int _number)
    {
        numberOfWeapons.text = "(" + shopController.GetWeaponController().GetAllWeapons().Count.ToString()  + "/" + _number.ToString() + ")";
    }

    public void CreateSlotsForWeapons(int _maxNumberOfWeapons)
    {
        maxCountWeapons = _maxNumberOfWeapons;

        DestroyAllSlotsForWeapons();

        //WeaponController playW = shopController.GetWeaponController();
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

        for (int i = 0; i < _currentWeapons.Count; i++)
        {
            GameObject weaponElement = Instantiate(weaponElementPrefab, listSlotsOfWeapons[i]);
            weaponElement.GetComponent<WeaponSlot>().AddItem(_currentWeapons[i].GetComponent<ItemShopInfo>());
        }

    }

    public void CreateSlotsForItems()
    {
        DestroyAllSlotsForItems();

        int count = shopController.GetInventory().Count;
        if (count < 16)
            count = 16;

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
        totalAmountOfGoldText.text = shopController.GetPlayerInventory().MoneyPlayer.ToString();
        totalAmountOfWoodText.text = shopController.GetPlayerInventory().WoodPlayer.ToString();
        priceForRerollTxt.text = shopController.GetRerollCost().ToString();
        priceForUpgradeShopTxt.text = shopController.GetShopLevelUpCost().ToString();

        priceForRerollTxt.text = shopController.GetRerollCost().ToString();
        shopController.CalculateDropChance();
        shopController.PickItemsForSale();
        ShowItemsForSale();
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
            totalAmountOfWoodText.text = shopController.GetPlayerInventory().WoodPlayer.ToString();
            priceForUpgradeShopTxt.text = shopController.GetShopLevelUpCost().ToString();
            shopLevelValue.text = (shopController.GetShopLevel()).ToString();
        }
    }

    public void RerollClick()
    {
        shopController.RerollShop();
        ShowItemsForSale();
        shopController.ResetsSlots();
        totalAmountOfWoodText.text = shopController.GetPlayerInventory().WoodPlayer.ToString();
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
            }
            else if (shopController.IsItem(items[i].SlotEntytiID))
            {
                StandartItem it = shopController.GetItem(items[i].SlotEntytiID);
                items[i].textName.text = it.ShopInfoItem.NameWeapon;
                items[i].textType.text = it.ShopInfoItem.TypeWeapon;
                items[i].textCost.text = it.GetPrice(shopController.GetCurrentWawe()).ToString();
                items[i].image.sprite = it.ShopInfoItem.IconWeapon;
                items[i].backgroud.color = it.ShopInfoItem.LevelItem.BackgroundColor;
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
                    items[i].gameObject.SetActive(false);
                    shopController.SoldSlot(slotNumber);
                }
                break;
            }
        }
        totalAmountOfGoldText.text = shopController.GetPlayerInventory().MoneyPlayer.ToString();
        priceForRerollTxt.text = shopController.GetRerollCost().ToString();
        numberOfWeapons.text = "(" + shopController.GetWeaponController().GetAllWeapons().Count.ToString()  + "/" + maxCountWeapons + ")";
    }

    public void ButtonSoldSlot(string name)
    {
        shopController.SellItem(name);
        totalAmountOfGoldText.text = shopController.GetPlayerInventory().MoneyPlayer.ToString();        
    }

    public void DisplayItemInfoWithBtn(ItemShopInfo _info, string description, Vector2 btnPosition)
    {
        DestroyItemInfo();
        btnPosition.x += XmovePosOfInfoPanel;
        btnPosition.y += YmovePosOfInfoPanel;
        _currentInfoItem = Instantiate(weaponInfoPrefab, btnPosition, Quaternion.identity, canvas);
        _currentInfoItem.GetComponent<ItemInfoPanelWithSellBtn>().SetUp(_info, description);
    }
    public void DisplayItemInfoWithoutBtn(ItemShopInfo _info, string description, Vector2 btnPosition)
    {
        DestroyItemInfo();
        btnPosition.x += XmovePosOfInfoPanel;
        btnPosition.y += YmovePosOfInfoPanel;
        _currentInfoItem = Instantiate(itemInfoPrefab, btnPosition, Quaternion.identity, canvas);
        _currentInfoItem.GetComponent<ItemInfoPanelWithoutSellBtn>().SetUp(_info, description);
    }

    public void DestroyItemInfo()
    {
        if(_currentInfoItem != null)
        {
            Destroy(_currentInfoItem.gameObject);
        }
    }
}
