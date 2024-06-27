
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image icon;
    public Image frame;
    public ItemShopInfo itemInfo;
    protected UIShop _uiShop;

    public void Init(UIShop uIShop)
    {
        _uiShop = uIShop;
    }

    public void AddItem(ItemShopInfo _itemInfo)
    {
        itemInfo = _itemInfo;
        icon.sprite = _itemInfo.IconWeapon;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        FrameOn();
        _uiShop.DisplayItemInfo(itemInfo, true, transform.position);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        FrameOn();
        _uiShop.DisplayItemInfo(itemInfo, false, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if( !_uiShop.dimmingPanel.activeInHierarchy)
        {
            FrameOff();
            _uiShop.DestroyItemInfo();
        }
    }

    public void FrameOff()
    {
        frame.enabled = false;
    }

    public void FrameOn()
    {
        frame.enabled = true;
    }
}
