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
    [Tooltip("����������� �����:")]
    [SerializeField] public int MinWave;

    [Header("��������� �����������: ")]
    [Tooltip("�������� ������:")]
    [SerializeField] public string NameWeapon;
    [Tooltip("��� ������:")]
    [SerializeField] public string TypeWeapon;
    [Tooltip("������ ������:")]
    [SerializeField] public Sprite IconWeapon;

    private int currentPrice;

    public int GetPrice(int wave)
    {
        currentPrice = Price + wave + (int)(Price * wave * 0.01f);
        return currentPrice; // ������ ���� �� ������������ ����� (wave)
    }

    public int GetSalePrice()
    {
        return currentPrice - (int)((float)currentPrice * ((float)DiscountProcent / 100.0f));
    }

    private void Awake()
    {
        IdWeapon = gameObject.name;
    }
}
