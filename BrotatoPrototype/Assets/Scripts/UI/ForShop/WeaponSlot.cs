using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    public void OnPointerClick(PointerEventData eventData)
    {
        FrameOn();
        UIShop.instance.DisplayWeaponInfo(itemInfo, true, transform.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FrameOn();
        UIShop.instance.DisplayWeaponInfo(itemInfo, false, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!UIShop.instance.dimmingPanel.activeInHierarchy)
        {
            FrameOff();
            UIShop.instance.DestroyItemInfo();
        }        
    }

    public void FrameOff()
    {
        frame.enabled = false;
    }

    private void FrameOn()
    {
        frame.enabled = true;
    }
}


