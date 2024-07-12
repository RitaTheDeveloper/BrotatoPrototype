using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDimmingPanel : MonoBehaviour, IPointerClickHandler
{
    private UIShop _uIShop;

    private void Start()
    {
        _uIShop = GetComponentInParent<UIShop>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _uIShop.FrameOffWeaponSlot();
        _uIShop.FrameOffItemSlot();
        _uIShop.DestroyItemInfo();
    }
}
