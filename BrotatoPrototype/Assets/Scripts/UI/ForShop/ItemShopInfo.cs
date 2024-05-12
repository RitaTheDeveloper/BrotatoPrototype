using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopInfo : MonoBehaviour
{
    [Tooltip("ID оружия:")]
    [SerializeField] public string IdWeapon;
    [Tooltip("Стоимость оружия:")]
    [SerializeField] public int Price;
    [Tooltip("Скидка при продаже %:")]
    [SerializeField] public int DiscountProcent;
    [Tooltip("Уровень предмета:")]
    [SerializeField] public RareItemsDataStruct LevelItem;

    [Header("Параметры отображения: ")]
    [Tooltip("Название оружия:")]
    [SerializeField] public string NameWeapon;
    [Tooltip("Иконка оружия:")]
    [SerializeField] public Sprite IconWeapon;

    private int currentPrice;

    public int GetPrice(int wave)
    {
        currentPrice = Price + wave + (int)(Price * wave * 0.05f);
        return currentPrice; // расчет цены за определенную волну (wave)
    }

    public int GetSalePrice()
    {
        return Price - (int)((float)Price * ((float)DiscountProcent / 100.0f));
    }

    private void Awake()
    {
        IdWeapon = gameObject.name;
    }
}
