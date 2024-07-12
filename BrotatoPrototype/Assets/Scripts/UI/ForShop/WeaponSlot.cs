using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSlot : ItemSlot
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        FrameOn();
        _uiShop.DisplayWeaponInfo(itemInfo, true, transform.position);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        FrameOn();
        _uiShop.DisplayWeaponInfo(itemInfo, false, transform.position);
    }
}


