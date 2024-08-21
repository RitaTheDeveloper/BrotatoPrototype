using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLevelStruct : MonoBehaviour
{
    [Tooltip("Цена поднятия уровня:")]
    [SerializeField] public int levelPrice = 0;

    [Tooltip("Номер уровня магазина:")]
    [SerializeField] public int levelNumber  = 0;

    [Tooltip("Список редкости слотов:")]
    [SerializeField] public List<RareItemsDataStruct> slotsData = new List<RareItemsDataStruct>();

}
