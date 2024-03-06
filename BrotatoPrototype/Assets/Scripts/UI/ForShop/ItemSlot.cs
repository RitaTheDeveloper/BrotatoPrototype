using System.Collections;
using System.Collections.Generic;
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
        background.color = Color.grey;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIShop.instance.DisplayItemInfo(itemInfo.NameWeapon, icon.sprite, background.color, itemInfo.TypeWeapon, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIShop.instance.DestroyItemInfo();
    }
}
