using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Image frame;

    public ItemShopInfo itemInfo;
    public void AddItem(ItemShopInfo _itemInfo)
    {
        itemInfo = _itemInfo;
        icon.sprite = _itemInfo.IconWeapon;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        frame.enabled = true;
        UIShop.instance.DisplayItemInfoWithoutBtn(itemInfo, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        frame.enabled = false;
        UIShop.instance.DestroyItemInfo();
    }
}
