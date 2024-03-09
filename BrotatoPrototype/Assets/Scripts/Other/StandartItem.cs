using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StandartItem : MonoBehaviour
{
    [SerializeField] public string IdItem;

    [SerializeField] public PlayerCharacteristics CharacteristicsItem;
    [SerializeField] public ItemShopInfo ShopInfoItem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private int currentPrice;

    public int GetPrice(int wave)
    {
        currentPrice = ShopInfoItem.Price + wave + (int)(ShopInfoItem.Price * wave * 0.01f);
        return currentPrice; // расчет цены за определенную волну (wave)
    }

    public int GetSalePrice()
    {
        return currentPrice - (int)((float)currentPrice * ((float)ShopInfoItem.DiscountProcent / 100.0f));
    }
}
