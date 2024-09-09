using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopContentGenerator : MonoBehaviour
{
    private readonly ICreator creator = new Creator();

    [SerializeField] ShopController shopController;
    [SerializeField] BuffShopController buffController;

    [Space]
    [Header("To Create")]
    [SerializeField] List<Item> baseItems = new List<Item>();
    [SerializeField] List<Weapon> baseWeapons = new List<Weapon>();
    [SerializeField] List<BuffPerLevel> baseBuffsPerLvl = new List<BuffPerLevel>();
    
    [Space]
    [Header("Created")]
    [SerializeField] List<StandartItem> items = new List<StandartItem>();
    [SerializeField] List<BaseWeapon> weapons = new List<BaseWeapon>();
    [SerializeField] List<UIBuffPerLvl> buffsPelLvl = new List<UIBuffPerLvl>();

    private Transform itemParent;
    private Transform weaponParent;
    private Transform buffPerLvlParent;

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
        buffPerLvlParent = CreateEmptyChildren("BuffsPerLvl").transform;

        // creating items
        CreateItems();
        shopController.ItemList = items;

        // creating weapons
        CreateWeapons();
        shopController.WeaponList = weapons;

        // creating buffs per level
        CreateBuffsPerLvl();
        buffController.AllBuffs = buffsPelLvl;
        buffController.DistributeBuffsAcrossTiers();
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
        items.Remove(items[0]); //лютый костыль, который нужно будет убрать
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
        weapons.Remove(weapons[0]); //лютый костыль, который нужно будет убрать

    }

    // переписать метод standart item - не нужен
    private void CreateBuffsPerLvl()
    {
        Transform parent = buffPerLvlParent;
        foreach (Item baseItem in baseBuffsPerLvl)
        {
            foreach (TierType tier in Enum.GetValues(typeof(TierType)))
            {
                Item buffPerLvl = creator.CreateItem(baseItem, tier);
                buffPerLvl.transform.SetParent(parent);
                buffsPelLvl.Add(buffPerLvl.GetComponent<UIBuffPerLvl>());
            }
        }
       // buffsPelLvl.Remove(buffsPelLvl[0]); //лютый костыль, который нужно будет убрать
    }
}
