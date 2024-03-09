using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StandartItem : MonoBehaviour
{
    [SerializeField] public string IdItem;

    [SerializeField] public PlayerCharacteristics CharacteristicsItem;
    [Tooltip("��������� ��������:")]
    [SerializeField] public int Price;
    [Tooltip("������ ��� ������� %:")]
    [SerializeField] public int DiscountPercentageItem;
    [Tooltip("������� ��������:")]
    [SerializeField] public RareItemsDataStruct LevelItem;

    [Header("��������� �����������: ")]
    [Tooltip("�������� ��������:")]
    [SerializeField] public string NameItem;
    [Tooltip("��� ��������:")]
    [SerializeField] public string TypeItem;
    [Tooltip("������ ��������:")]
    [SerializeField] public Sprite IconItem;

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
        currentPrice = Price + wave + (int)(Price * wave * 0.01f);
        return currentPrice; // ������ ���� �� ������������ ����� (wave)
    }

    public int GetSalePrice()
    {
        return currentPrice - (int)((float)currentPrice * ((float)DiscountPercentageItem / 100.0f));
    }
}
