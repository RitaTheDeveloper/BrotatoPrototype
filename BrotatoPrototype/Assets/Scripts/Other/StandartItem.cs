using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StandartItem : MonoBehaviour
{
    public string KeyItem;
    public Image IconItem;
    public PlayerCharacteristics CharacteristicsItem;
    public int RarityValueItem;
    public int Price;
    public int DiscountPercentageItem;
    public BoxCollider2D Collider; // в будущем для нажатия в ui, может стоит убрать, но хз как хранить item к которому привязан collider

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
