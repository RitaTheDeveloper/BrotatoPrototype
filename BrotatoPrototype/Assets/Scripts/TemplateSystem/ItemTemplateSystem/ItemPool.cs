using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    private readonly ItemCreator itemCreator = new ItemCreator();
    private readonly WeaponCreator weaponCreator = new WeaponCreator();

    [SerializeField] ShopController shopController;
    [Space]
    [Header("Items Creation Section")]
    [SerializeField] List<Item> startItemList = new List<Item>();
    [SerializeField] List<StandartItem> createdItems = new List<StandartItem>();
    
    [Space]
    [Header("Weapon Creation Section")]
    [SerializeField] List<Weapon> startWeaponList = new List<Weapon>();
    [SerializeField] List<BaseWeapon> createdWeapons = new List<BaseWeapon>();


    private void Start()
    {
        if(shopController == null)
        {
            throw new NotSupportedException($"shopController must be valid.");
        }

        CreateItems();
        shopController.ItemList = createdItems;

        CreateWeapons();
        shopController.WeaponList = createdWeapons;
    }

    private void CreateItems()
    {
        Transform parent = transform;
        foreach (Item item in startItemList)
        {
            Item itemT1 = itemCreator.CreateItem(item, TierType.FirstTier);
            itemCreator.SetParentItem(itemT1, parent);
            createdItems.Add(itemT1.GetComponent<StandartItem>());

            Item itemT2 = itemCreator.CreateItem(item, TierType.SecondTier);
            itemCreator.SetParentItem(itemT2, parent);
            createdItems.Add(itemT2.GetComponent<StandartItem>());

            Item itemT3 = itemCreator.CreateItem(item, TierType.ThirdTier);
            itemCreator.SetParentItem(itemT3, parent);
            createdItems.Add(itemT3.GetComponent<StandartItem>());

            Item itemT4 = itemCreator.CreateItem(item, TierType.FourthTier);
            itemCreator.SetParentItem(itemT4, parent);
            createdItems.Add(itemT4.GetComponent<StandartItem>());
        }
    }

    private void CreateWeapons()
    {
        Transform parent = transform;
        foreach (Weapon weapon in startWeaponList)
        {
            Weapon weaponT1 = weaponCreator.CreateWeapon(weapon, TierType.FirstTier);
            itemCreator.SetParentItem(weaponT1, parent);
            createdWeapons.Add(weaponT1.GetComponent<BaseWeapon>());

            Weapon weaponT2 = weaponCreator.CreateWeapon(weapon, TierType.SecondTier);
            itemCreator.SetParentItem(weaponT2, parent);
            createdWeapons.Add(weaponT2.GetComponent<BaseWeapon>());

            Weapon weapon3 = weaponCreator.CreateWeapon(weapon, TierType.ThirdTier);
            itemCreator.SetParentItem(weapon3, parent);
            createdWeapons.Add(weapon3.GetComponent<BaseWeapon>());

            Weapon weaponT4 = weaponCreator.CreateWeapon(weapon, TierType.FourthTier);
            itemCreator.SetParentItem(weaponT4, parent);
            createdWeapons.Add(weaponT4.GetComponent<BaseWeapon>());
        }
    }
}
