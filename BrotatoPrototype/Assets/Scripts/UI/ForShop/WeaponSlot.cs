using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    public void OnPointerClick(PointerEventData eventData)
    {

        UIShop.instance.DisplayItemInfoWithBtn(itemInfo, true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIShop.instance.DisplayItemInfoWithBtn(itemInfo, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!UIShop.instance.dimmingPanel.activeInHierarchy)
        {
            UIShop.instance.DestroyItemInfo();
        }        
    }
}
