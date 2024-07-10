using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopContentGenerator : MonoBehaviour
{
    private readonly ICreator creator = new Creator();

    [SerializeField] ShopController shopController;

    [Space]
    [Header("To Create")]
    [SerializeField] List<Item> baseItems = new List<Item>();
    [SerializeField] List<Weapon> baseWeapons = new List<Weapon>();
    
    [Space]
    [Header("Created")]
    [SerializeField] List<StandartItem> items = new List<StandartItem>();
    [SerializeField] List<BaseWeapon> weapons = new List<BaseWeapon>();

    private Transform itemParent;
    private Transform weaponParent;

    private void Start()
    {
        // getting shop reference
        if(shopController == null)
        {
            throw new NotSupportedException($"shopController must be valid.");
        }

        // preparing child transforms to store created items/weapons
        itemParent = CreateEmptyChildren("Items").transform;
        weaponParent = CreateEmptyChildren("Weapons").transform;

        // creating items
        CreateItems();
        shopController.ItemList = items;

        // creating weapons
        CreateWeapons();
        shopController.WeaponList = weapons;
    }

    private GameObject CreateEmptyChildren(string name)
    {
        GameObject emptyGO = new GameObject(name);
        emptyGO.transform.SetParent(transform);
        return emptyGO;
    }

    private void CreateItems()
    {
        Transform parent = itemParent;
        foreach (Item baseItem in baseItems)
        {
            foreach (TierType tier in Enum.GetValues(typeof(TierType)))
            {
                Item item = creator.CreateItem(baseItem, tier);
                item.transform.SetParent(parent);
                items.Add(item.GetComponent<StandartItem>());
            }
        }
    }

    private void CreateWeapons()
    {
        Transform parent = weaponParent;
        foreach (Weapon baseWeapon in baseWeapons)
        {
            foreach (TierType tier in Enum.GetValues(typeof(TierType)))
            {
                Weapon weapon = creator.CreateWeapon(baseWeapon, tier);
                weapon.transform.SetParent(parent);
                weapons.Add(weapon.GetComponent<BaseWeapon>());

                weapon.gameObject.SetActive(false);
            }
        }
    }
}
