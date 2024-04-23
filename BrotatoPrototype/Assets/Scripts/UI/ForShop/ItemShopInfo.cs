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
    [Tooltip("Минимальная волна:")]
    [SerializeField] public int MinWave;

    [Header("Параметры отображения: ")]
    [Tooltip("Название оружия:")]
    [SerializeField] public string NameWeapon;
    [Tooltip("Тип оружия:")]
    [SerializeField] public string TypeWeapon;
    [Tooltip("Характеристики оружия:")]
    [SerializeField] public string Description;
    [Tooltip("Иконка оружия:")]
    [SerializeField] public Sprite IconWeapon;

    private int currentPrice;

    public int GetPrice(int wave)
    {
        currentPrice = Price + wave + (int)(Price * wave * 0.2f);
        return currentPrice; // расчет цены за определенную волну (wave)
    }

    public int GetSalePrice()
    {
        return currentPrice - (int)((float)currentPrice * ((float)DiscountProcent / 100.0f));
    }

    private void Awake()
    {
        IdWeapon = gameObject.name;
    }

    public void DisplayCharacteristicsOfWeapon()
    {
        Debug.Log("узнать характеристики оружия");
        Weapon weapon = GetComponent<Weapon>();
        if (weapon)
        {
            Description = "урон: " + weapon.StartDamage;
            //Debug.Log("урон " + weapon.StartDamage);
            //damage = weapon.StartDamage.ToString();
        }
    }
}
