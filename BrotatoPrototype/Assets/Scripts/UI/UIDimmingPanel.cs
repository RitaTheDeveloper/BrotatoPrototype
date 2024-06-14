using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDimmingPanel : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UIShop.instance.FrameOffWeaponSlot();
        UIShop.instance.FrameOffItemSlot();
        UIShop.instance.DestroyItemInfo();
    }
}
