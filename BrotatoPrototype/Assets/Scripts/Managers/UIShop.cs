using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIShop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private TextMeshProUGUI totalAmountOfGoldText;
    [SerializeField] private TextMeshProUGUI priceForUpgradeShopTxt;
    [SerializeField] private TextMeshProUGUI priceForRerollTxt;
    [SerializeField] private TextMeshProUGUI numberOfWeapons;
    [Space(20)]
    [SerializeField] private Transform panelOfWeapons;
    [SerializeField] private GameObject slotForWeaponPrefab;
    [SerializeField] private GameObject weaponElementPrefab;

    List<Transform> listSlotsOfWeapons = new List<Transform>();

    private IShopController shopController;

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

    public void CreateWeaponElements(int _numberOfCurrentWeapons)
    {
        for(int i = 0; i < _numberOfCurrentWeapons; i++)
        {
            var weaponElement = Instantiate(weaponElementPrefab, listSlotsOfWeapons[i]);
        }
    }

    public void OnCreateShopInterface()
    {
        shopController.GetRerollCost();
        shopController.CalculateDropChance();
        shopController.PickItemsForSale();

        Dictionary<int, string> items_to_slot = shopController.GetItemsForSale();

        SlotItemForSaleData[] items = GetComponents<SlotItemForSaleData>();

        for (int i = 0; i < items.Length; i++)
        {
            

        }
    }

    void Start()
    {
        shopController = GetComponent<ShopController>();
    }
}
