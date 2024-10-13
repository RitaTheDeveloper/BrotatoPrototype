using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    [SerializeField] private AnalyticsSystem analyticsSystem;
    [SerializeField] public Image babaYagaImg;
    [SerializeField] private Animator ygaAnimator;
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private TextMeshProUGUI totalAmountOfGoldText;
    [SerializeField] private TextMeshProUGUI totalAmountOfWoodText;
    [SerializeField] private TextMeshProUGUI priceForUpgradeShopTxt;
    [SerializeField] private TextMeshProUGUI priceForRerollTxt;
    [SerializeField] private TextMeshProUGUI numberOfWeapons;
    [SerializeField] private TextMeshProUGUI shopLevelValue;
    [SerializeField] private CharacteristicsUI[] characteristicsUIs;
    [SerializeField] private Button upgradeShopBtn;
    [Space(20)]
    [SerializeField] private Transform panelOfWeapons;
    [SerializeField] private Transform panelOfItems;
    [SerializeField] private Transform panelItemForSale;
    [SerializeField] private GameObject slotForWeaponPrefab;
    [SerializeField] private GameObject slotForItemPrefab;
    [SerializeField] private GameObject weaponElementPrefab;
    [SerializeField] private GameObject itemElementPrefab;
    [SerializeField] private GameObject attentionWindowPrefab;
    [SerializeField] private Transform containerForPopUpWindows;
    [SerializeField] private GameObject weaponInfoPrefab;
    [SerializeField] private GameObject itemInfoPrefab;
    [SerializeField] private Transform canvas;
    [SerializeField] private float XmovePosOfInfoPanel;
    [SerializeField] private float YmovePosOfInfoPanel;
    [SerializeField] private Transform positionOfInfoPanel;
    [SerializeField] private SlotItemForSaleData itemSlotForSalePrefab;
    [SerializeField] private List<SlotItemForSaleData> listOfPrefabsForItemsForSale;
    [SerializeField] private int maxAmountOfItems = 32;
    [SerializeField] private float delayAttentionWindow = 1f;
    [SerializeField] public Vector2[] pointsForAttentionWindows = new Vector2[2];
    [Header("левелы дл€ апгрейда бабы яги")]
    [SerializeField] private int[] levelsForUpgradeBabaYaga = new int[3];
    [SerializeField] private Sprite[] babaYagaSprites;
    [SerializeField] private Animator fireAnimator;
    [SerializeField] private UpgrateShopBtn upgrateShop;

    private List<WeaponSlot> _currentWeaponSlots;
    private List<ItemSlot> _currentItemSlots;
    private int currentIndexBabaYaga = 0;
    public GameObject dimmingPanel;
    private List<SlotItemForSaleData> items = new List<SlotItemForSaleData>();

    public List<Transform> listSlotsOfWeapons = new List<Transform>();
    List<Transform> listSlotsOfItems = new List<Transform>();

    public ShopController shopController;

    private GameObject _currentInfoItem = null;

    private int maxCountWeapons { set; get; }

    private void Awake()
    {
        GetComponentsInChildren<SlotItemForSaleData>(items);
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
        waveNumberText.text = "(волна " + (_waveNumber + 1).ToString() + ")";
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

        List<BaseWeapon> wl = shopController.GetWeaponController().GetAllWeapons();

        for (int i = 0; i < _maxNumberOfWeapons; i++)
        {
            GameObject slot = Instantiate(slotForWeaponPrefab, panelOfWeapons);
            listSlotsOfWeapons.Add(slot.transform);
        }
    }

    public void DestroyAllSlotsForWeapons()
    {
        foreach (Transform child in panelOfWeapons.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }

        listSlotsOfWeapons.Clear();
    }

    public void CreateWeaponElements(List<BaseWeapon> _currentWeapons)
    {
        _currentWeaponSlots = new List<WeaponSlot>();

        if (_currentWeapons.Count > 0)
        {
            for (int i = 0; i < _currentWeapons.Count; i++)
            {
                GameObject weaponElement = Instantiate(weaponElementPrefab, listSlotsOfWeapons[i]);
                WeaponSlot ws = weaponElement.GetComponent<WeaponSlot>();
                ws.Init(this);
                ws.AddItem(_currentWeapons[i].GetComponent<ItemShopInfo>());
                _currentWeaponSlots.Add(ws);
            }
        }
    }

    public void DeleteAllWeaponElements()
    {
        foreach (Transform weaponSlot in panelOfWeapons.GetComponentInChildren<Transform>())
        {
            foreach (Transform weaponElement in weaponSlot)
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
        _currentItemSlots = new List<ItemSlot>();

        if (_items.Count > listSlotsOfItems.Count)
        {
            CreateSlotsForItems();
        }
        for (int i = 0; i < _items.Count; i++)
        {
            GameObject itemElement = Instantiate(itemElementPrefab, listSlotsOfItems[i]);
            itemElement.GetComponent<ItemSlot>().Init(this);
            itemElement.GetComponent<ItemSlot>().AddItem(_items[i].GetComponent<ItemShopInfo>());
            _currentItemSlots.Add(itemElement.GetComponent<ItemSlot>());
        }
    }

    public void DeleteAllItemElements()
    {
        foreach (Transform itemSlot in panelOfItems.GetComponentInChildren<Transform>())
        {
            foreach (Transform itemElement in itemSlot)
            {
                if (itemElement)
                {
                    Destroy(itemElement.gameObject);
                }
            }
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
            BackgroundMusicManger.instance.PlayShopMusicFromFight();
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
            analyticsSystem.OnUpgradeShop();
            ChangeSpriteOfBabaYga(shopController.GetShopLevel());
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
            analyticsSystem.OnRerollItems();
            FireAnim();
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
            items[i].Init(this);
            if (shopController.IsWeapon(items[i].SlotEntytiID))
            {
                ItemShopInfo w = shopController.GetUiInfo(items[i].SlotEntytiID);
                items[i].DisplayInfoForWeapon(w, shopController.GetCurrentWawe());
            }
            else if (shopController.IsItem(items[i].SlotEntytiID))
            {
                StandartItem it = shopController.GetItem(items[i].SlotEntytiID);
                items[i].DisplayInfoForItem(it, shopController.GetCurrentWawe());
            }
        }
    }

    public void ButtonBuySlot(int slotNumber)
    {
        if (shopController.IsSlotSold(slotNumber))
        {
            Debug.Log("—лот продан!");
            return;
        }
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].SlotNumber == slotNumber)
            {
                if (shopController.BuyItem(items[i].SlotEntytiID))
                {
                    listOfPrefabsForItemsForSale[i].GetComponent<SlotItemForSaleData>().PotOff();
                    shopController.SoldSlot(slotNumber);
                }
                break;
            }
        }
        totalAmountOfGoldText.text = shopController.GetPlayerInventory().GetMoney().ToString();
        priceForRerollTxt.text = shopController.GetRerollCost().ToString();
        UpdatePlayerScales();
        UpdateNumberOfCurrentWeapons(shopController.GetWeaponController().GetAllWeapons().Count, maxCountWeapons);
    }

    private void DisplayItemSold(int index)
    {
        listOfPrefabsForItemsForSale[index].GetComponent<SlotItemForSaleData>().PotOff();
    }


    public void UpdateNumberOfCurrentWeapons(int numberOfCurrentWeapons, int numberOfMaxweapons)
    {
        numberOfWeapons.text = "ќружие (" + numberOfCurrentWeapons.ToString() + "/" + numberOfMaxweapons + ")";
    }

    public void ButtonSoldSlot(string name)
    {
        shopController.SellItem(name);
        totalAmountOfGoldText.text = shopController.GetPlayerInventory().GetMoney().ToString();
        UpdateNumberOfCurrentWeapons(shopController.GetWeaponController().GetAllWeapons().Count, maxCountWeapons);
    }

    public void DisplayWeaponInfo(ItemShopInfo _info, bool isIconPressed, Vector2 posBtn)
    {
        DestroyItemInfo();
        dimmingPanel.SetActive(isIconPressed);
        Vector2 position = new Vector2(posBtn.x - Screen.width / 10, positionOfInfoPanel.position.y);
        _currentInfoItem = Instantiate(weaponInfoPrefab, position, Quaternion.identity, canvas);
        _currentInfoItem.GetComponent<ItemInfoPanel>().Init(this);
        _currentInfoItem.GetComponent<ItemInfoPanel>().SetUp(_info);
    }

    public void DisplayItemInfo(ItemShopInfo _info, bool isIconPressed, Vector2 posBtn)
    {
        DestroyItemInfo();
        dimmingPanel.SetActive(isIconPressed);
        Vector2 position = new Vector2(posBtn.x + Screen.width / 10, positionOfInfoPanel.position.y);
        _currentInfoItem = Instantiate(itemInfoPrefab, position, Quaternion.identity, canvas);
        _currentInfoItem.GetComponent<ItemInfoPanel>().Init(this);
        _currentInfoItem.GetComponent<ItemInfoPanel>().SetUp(_info);
    }

    public void DestroyItemInfo()
    {
        dimmingPanel.SetActive(false);
        if (_currentInfoItem != null)
        {
            Destroy(_currentInfoItem.gameObject);
        }
    }

    // этот метод нужен, если хотим создавать кол-во предлагаемых предметов динамически 
    public void CreateItemsSlotsForSale(int _number)
    {
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

    public void UpdatePlayerScales()
    {
        GameObject player = GameManager.instance.player;
        player.GetComponent<PlayerHealth>().Init();
        player.GetComponent<PlayerHealth>().DisplayHealth();
        player.GetComponent<PlayerSatiety>().ChangeSatiety(0);
    }

    public void UpdateUICharacteristics()
    {
        if (GameManager.instance.player != null)
        {
            PlayerCharacteristics playerCharacteristics = GameManager.instance.player.GetComponent<PlayerCharacteristics>();
            foreach (var characteristicsUI in characteristicsUIs)
            {
                characteristicsUI.UpdateCharacterisctics(playerCharacteristics);
            }
        }
            
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

    public void ChangeSpriteOfBabaYga(int levelShop)
    {
        if (levelsForUpgradeBabaYaga.Contains(levelShop))
        {
            currentIndexBabaYaga++;
            if (currentIndexBabaYaga > babaYagaSprites.Length - 1)
            {
                currentIndexBabaYaga = babaYagaSprites.Length - 1;
            }

            ygaAnimator.SetTrigger("change");
            StartCoroutine(ChangeSpriteYga(babaYagaSprites[currentIndexBabaYaga]));
            if (AudioManager.instance != null)
            {
                AudioManager.instance.Play("ShopBabaYagaChange");
            }
        }        
    }

    private IEnumerator ChangeSpriteYga(Sprite newSprite)
    {
        yield return new WaitForSeconds(0.2f);
        babaYagaImg.GetComponent<Image>().sprite = newSprite;
    }

    public void ResetBabaYaga()
    {
        currentIndexBabaYaga = 0;
        babaYagaImg.GetComponent<Image>().sprite = babaYagaSprites[currentIndexBabaYaga];
    }

    public void FrameOffWeaponSlot()
    {
        foreach(WeaponSlot w in _currentWeaponSlots)
        {
            w.FrameOff();
        }
    }

    public void FrameOffItemSlot()
    {
        foreach (ItemSlot item in _currentItemSlots)
        {
            item.FrameOff();
        }
    }

    public IEnumerator ShowMessage(string message, Vector2 point)
    {
        var popupWindow = Instantiate(attentionWindowPrefab, transform.position, Quaternion.identity, canvas);
        popupWindow.transform.SetParent(containerForPopUpWindows);
        popupWindow.transform.localPosition = point;
        popupWindow.GetComponentInChildren<TextMeshProUGUI>().text = message;
        yield return new WaitForSeconds(delayAttentionWindow);
        Destroy(popupWindow);
    }

    public void DestroyAllPopUpWindows()
    {
        DestroyItemInfo();
        foreach (Transform popUpWindow in containerForPopUpWindows)
        {
            Destroy(popUpWindow.gameObject);
        }
    }

    public void FireAnim()
    {
        fireAnimator.SetTrigger("Fire");
    }

    public int GetBabaYagaIndex()
    {
        return currentIndexBabaYaga;
    }

    public void ResetUIShop()
    {
        ResetBabaYaga();
        upgrateShop.ShopMax(false);
    }

    public void ShopIsMax()
    {
        upgrateShop.ShopMax(true);
    }

    public void SellItem(bool isWeapon, string id)
    {
        ButtonSoldSlot(id);
        DestroyItemInfo();
        if (isWeapon)
        {
            DeleteAllWeaponElements();
            CreateWeaponElements(shopController.GetWeaponController().GetAllWeapons());
        }
        else
        {
            DeleteAllItemElements();
            CreateItemsElements(shopController.GetPlayerInventory().GetAllItems());
        }
    }
}
