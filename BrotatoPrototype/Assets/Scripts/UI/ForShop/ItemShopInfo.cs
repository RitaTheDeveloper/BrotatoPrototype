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
    [Tooltip("������ ��� ������� %:")]
    [SerializeField] public int DiscountProcent;
    [Tooltip("������� ��������:")]
    [SerializeField] public RareItemsDataStruct LevelItem;

    [Header("��������� �����������: ")]
    [Tooltip("�������� ������:")]
    [SerializeField] public string NameWeapon;
    [Tooltip("������ ������:")]
    [SerializeField] public Sprite IconWeapon;

    private int currentPrice;

    public int GetPrice(int wave)
    {
        currentPrice = Price + (int)(Price * wave * wave * 0.01f);
        return currentPrice; // ������ ���� �� ������������ ����� (wave)
    }

    public int GetSalePrice()
    {
        return Price - (int)((float)Price * ((float)DiscountProcent / 100.0f));
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
