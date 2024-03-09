using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Image background;

    public ItemShopInfo itemInfo;
    public void AddItem(StandartItem _itemInfo)
    {
        itemInfo = gameObject.AddComponent<ItemShopInfo>();

        itemInfo.IdWeapon = _itemInfo.IdItem;
        itemInfo.LevelItem = _itemInfo.LevelItem;
        itemInfo.TypeWeapon = _itemInfo.TypeItem;
        itemInfo.IconWeapon = _itemInfo.IconItem;

        icon.sprite = _itemInfo.IconItem;
        background.color = _itemInfo.LevelItem.BackgroundColor;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIShop.instance.DisplayItemInfoWithoutBtn(itemInfo, itemInfo.TypeWeapon, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIShop.instance.DestroyItemInfo();
    }
}
