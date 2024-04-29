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
    public void AddItem(ItemShopInfo _itemInfo)
    {
        itemInfo = _itemInfo;
        icon.sprite = _itemInfo.IconWeapon;
        background.color = _itemInfo.LevelItem.BackgroundColor;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIShop.instance.DisplayItemInfoWithoutBtn(itemInfo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIShop.instance.DestroyItemInfo();
    }
}
