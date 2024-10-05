using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopInfo : MonoBehaviour
{
    [Tooltip("ID ������:")]
    [SerializeField] public string IdWeapon;
    [Tooltip("��������� ������:")]
    [SerializeField] public int Price;
    [Tooltip("������� ��������:")]
    [SerializeField] public RareItemsDataStruct LevelItem;

    [Header("��������� �����������: ")]
    [Tooltip("�������� ������:")]
    [SerializeField] public string NameWeapon;
    [Tooltip("������ ������:")]
    [SerializeField] public Sprite IconWeapon;

    private int currentPrice;
    public float DiscountProcent = 30.0f;

    public int GetPrice(int wave)
    {
        currentPrice = Price;
        return currentPrice; // ������ ���� �� ������������ ����� (wave)
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
