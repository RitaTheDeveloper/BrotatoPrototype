using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopInfo : MonoBehaviour
{
    public string IdWeapon;
    [Tooltip("Стоимость оружия:")]
    [SerializeField] public int Price;
    [Tooltip("Скидка при продаже %:")]
    [SerializeField] public int DiscountProcent;
    [Tooltip("Уровень предмета:")]
    [SerializeField] public int LevelItem;
    [Tooltip("Минимальная волна:")]
    [SerializeField] public int MinWave;

    [Header("Параметры отображения: ")]
    [Tooltip("Название оружия:")]
    [SerializeField] public string NameWeapon;
    [Tooltip("Тип оружия:")]
    [SerializeField] public string TypeWeapon;
    [Tooltip("Иконка оружия:")]
    [SerializeField] public Sprite IconWeapon;
}
