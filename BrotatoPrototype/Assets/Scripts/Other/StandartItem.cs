using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StandartItem : MonoBehaviour
{
    [SerializeField] public string IdItem;
    [SerializeField] public Image IconItem;
    [SerializeField] public PlayerCharacteristics CharacteristicsItem;
    [SerializeField] public int RarityValueItem;
    [Tooltip("Стоимость предмета:")]
    [SerializeField] public int Price;
    [Tooltip("Скидка при продаже %:")]
    [SerializeField] public int DiscountPercentageItem;
    [Tooltip("Уровень предмета:")]
    [SerializeField] public int LevelItem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPrice(int wave)
    {
        return Price + wave + (int)(Price * wave * 0.01f); // расчет цены за определенную волну (wave)
    }
    
}
