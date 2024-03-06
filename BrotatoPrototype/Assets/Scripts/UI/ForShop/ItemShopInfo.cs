using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopInfo : MonoBehaviour
{
    public string IdWeapon;
    [Tooltip("��������� ������:")]
    [SerializeField] public int Price;
    [Tooltip("������ ��� ������� %:")]
    [SerializeField] public int DiscountProcent;
    [Tooltip("������� ��������:")]
    [SerializeField] public int LevelItem;
    [Tooltip("����������� �����:")]
    [SerializeField] public int MinWave;

    [Header("��������� �����������: ")]
    [Tooltip("�������� ������:")]
    [SerializeField] public string NameWeapon;
    [Tooltip("��� ������:")]
    [SerializeField] public string TypeWeapon;
    [Tooltip("������ ������:")]
    [SerializeField] public Sprite IconWeapon;
}
