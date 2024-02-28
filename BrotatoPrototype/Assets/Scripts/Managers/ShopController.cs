using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] UIShop uiShop;
    [SerializeField] DataForShop dataForShop;
    void Start()
    {
        uiShop.SetWaveNumberText(dataForShop.waveNumber);
        uiShop.SetTotalAmountOfGoldText(dataForShop.totalAmountOfGold);
        uiShop.SetPriceForUpgradeShopText(dataForShop.priceForUpgradeShop);
        uiShop.SetPriceForRerollText(dataForShop.priceForReroll);
        uiShop.SetNumberOfPossibleWeapons(dataForShop.maxNumberOfWeapons);
        uiShop.CreateSlotsForWeapons(dataForShop.maxNumberOfWeapons);
        uiShop.CreateWeaponElements(dataForShop.numberOfCurrentWeapons);
    }
}
