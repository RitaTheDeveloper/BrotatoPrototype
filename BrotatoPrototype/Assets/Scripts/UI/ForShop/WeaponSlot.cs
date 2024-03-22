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

    private bool _onClick = false;

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
        if (!_onClick) { _onClick = true; }
        else { _onClick = false; }
        
        UIShop.instance.DisplayItemInfoWithBtn(itemInfo, transform.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIShop.instance.DisplayItemInfoWithBtn(itemInfo, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_onClick)
        {
            UIShop.instance.DestroyItemInfo();
        }        
    }
}
