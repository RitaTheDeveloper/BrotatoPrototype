using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanelWithSellBtn : MonoBehaviour
{
    public Image icon;
    public Image background;
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI typeItem;
    public TextMeshProUGUI description;    
    public Button sellBtn;
    public TextMeshProUGUI price;
    public string id;

    public void SetUp(ItemShopInfo itemInfo)
    {
        icon.sprite = itemInfo.IconWeapon;
        background.color = itemInfo.LevelItem.BackgroundColor;
        nameItem.text = itemInfo.NameWeapon;
        price.text = itemInfo.GetSalePrice().ToString();
        id = itemInfo.IdWeapon;
        sellBtn.onClick.RemoveAllListeners();
        sellBtn.onClick.AddListener(SellItem);
    }

    void SellItem()
    {
        UIShop.instance.ButtonSoldSlot(id);
        UIShop.instance.DestroyItemInfo();
        UIShop.instance.DeleteAllWeaponElements();
        UIShop.instance.CreateWeaponElements(ShopController.instance.GetWeaponController().GetAllWeapons());
    }

}
