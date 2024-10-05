using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopInfo : MonoBehaviour
{
    [Tooltip("ID оружия:")]
    [SerializeField] public string IdWeapon;
    [Tooltip("Стоимость оружия:")]
    [SerializeField] public int Price;
    [Tooltip("Уровень предмета:")]
    [SerializeField] public RareItemsDataStruct LevelItem;

    [Header("Параметры отображения: ")]
    [Tooltip("Название оружия:")]
    [SerializeField] public string NameWeapon;
    [Tooltip("Иконка оружия:")]
    [SerializeField] public Sprite IconWeapon;

    private int currentPrice;
    public float DiscountProcent = 30.0f;

    public int GetPrice(int wave)
    {
        currentPrice = Price;
        return currentPrice; // расчет цены за определенную волну (wave)
    }

    public int GetSalePrice()
    {
        return Price - (int)((float)Price * (30.0f / 100.0f));
    }

    public RareItemsDataStruct GetLevelItem()
    {
        return LevelItem;
    }

    private void Awake()
    {
        IdWeapon = gameObject.name;
    }
}
