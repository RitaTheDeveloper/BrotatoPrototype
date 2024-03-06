using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    public static UIShop instance; 

    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private TextMeshProUGUI totalAmountOfGoldText;
    [SerializeField] private TextMeshProUGUI totalAmountOfWoodText;
    [SerializeField] private TextMeshProUGUI priceForUpgradeShopTxt;
    [SerializeField] private TextMeshProUGUI priceForRerollTxt;
    [SerializeField] private TextMeshProUGUI numberOfWeapons;
    [Space(20)]
    [SerializeField] private Transform panelOfWeapons;
    [SerializeField] private GameObject slotForWeaponPrefab;
    [SerializeField] private GameObject weaponElementPrefab;
    [SerializeField] private GameObject itemInfoPrefab;
    [SerializeField] private Transform canvas;
    [SerializeField] private float XmovePosOfInfoPanel;
    [SerializeField] private float YmovePosOfInfoPanel;

    List<SlotItemForSaleData> items = new List<SlotItemForSaleData>();

    List<Transform> listSlotsOfWeapons = new List<Transform>();


    private IShopController shopController;

    private GameObject _currentInfoItem = null;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeUIParametersOfShop()
    {
        SetNumberOfPossibleWeapons(5);
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
        numberOfWeapons.text = "(0/" + _number.ToString() + ")";
    }

    public void CreateSlotsForWeapons(int _maxNumberOfWeapons)
    {
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
            weaponElement.GetComponent<ItemSlot>().AddItem(_currentWeapons[i].GetComponent<ItemShopInfo>());
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
        shopController.UpgrateShop();
        totalAmountOfWoodText.text = shopController.GetPlayerInventory().WoodPlayer.ToString();
        priceForUpgradeShopTxt.text = shopController.GetShopLevelUpCost().ToString();
    }

    public void RerollClick()
    {
        shopController.RerollShop();
        ShowItemsForSale();
        totalAmountOfWoodText.text = shopController.GetPlayerInventory().WoodPlayer.ToString();
        priceForRerollTxt.text = shopController.GetRerollCost().ToString();
    }

    void Start()
    {
        shopController = GetComponent<ShopController>();
        GetComponentsInChildren<SlotItemForSaleData>(items);
        //GetComponentsInChildren<RareItemsDataStruct>(rares);
    }

    void ShowItemsForSale()
    {
        Dictionary<int, string> items_to_slot = shopController.GetItemsForSale();
        List<RareItemsDataStruct> rares = shopController.GetRareItemsDataStruct();

        for (int i = 0; i < items.Count; i++)
        {
            items[i].SlotEntytiID = items_to_slot[items[i].SlotNumber];
            if (shopController.IsWeapon(items[i].SlotEntytiID))
            {
                Weapon w = shopController.GetWeapon(items[i].SlotEntytiID);
                items[i].textName.text = w.NameWeapon;
                items[i].textType.text = w.TypeWeapon;
                items[i].textCost.text = w.GetPrice(shopController.GetCurrentWawe()).ToString();
                items[i].image.sprite = w.IconWeapon;
                for (int j = 0; j < rares.Count; j++)
                {
                    if (rares[j].level == w.LevelItem)
                    {
                        items[i].backgroud.color = rares[j].BackgroundColor;
                        break;
                    }
                }
            }
            else if (shopController.IsItem(items[i].SlotEntytiID))
            {
                StandartItem it = shopController.GetItem(items[i].SlotEntytiID);
                items[i].textName.text = it.NameItem;
                items[i].textType.text = it.TypeItem;
                items[i].textCost.text = it.GetPrice(shopController.GetCurrentWawe()).ToString();
                items[i].image.sprite = it.IconItem;
                for (int j = 0; j < rares.Count; j++)
                {
                    if (rares[j].level == it.LevelItem)
                    {
                        items[i].backgroud.color = rares[j].BackgroundColor;
                        break;
                    }
                }
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
                shopController.SoldSlot(slotNumber);
                shopController.BuyItem(items[i].SlotEntytiID);
                break;
            }
        }
        totalAmountOfGoldText.text = shopController.GetPlayerInventory().MoneyPlayer.ToString();
        priceForRerollTxt.text = shopController.GetRerollCost().ToString();
    }

    public void ButtonSoldSlot(string name)
    {
        shopController.SellItem(name);
        totalAmountOfGoldText.text = shopController.GetPlayerInventory().MoneyPlayer.ToString();
    }

    public void DisplayItemInfo(string nameItem, Sprite icon, Color tierColor, string description, Vector2 btnPosition)
    {
        DestroyItemInfo();
        btnPosition.x += XmovePosOfInfoPanel;
        btnPosition.y += YmovePosOfInfoPanel;
        _currentInfoItem = Instantiate(itemInfoPrefab, btnPosition, Quaternion.identity, canvas);
        _currentInfoItem.GetComponent<ItemInfoPanelWithSellBtn>().SetUp(nameItem, icon, tierColor, description);
    }

    public void DestroyItemInfo()
    {
        if(_currentInfoItem != null)
        {
            Destroy(_currentInfoItem.gameObject);
        }
    }
}
