using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI typeItem;
    public TextMeshProUGUI tierItem;
    public TextMeshProUGUI description;
    public Button sellBtn;
    public TextMeshProUGUI price;
    public string id;
    [SerializeField] private CharacteristicsInfoPanelForWeaponAndItem characteristicsInfo;

    public void SetUp(ItemShopInfo itemInfo)
    {       
        nameItem.text = itemInfo.NameWeapon;
        typeItem.text = "снаряжение";
        tierItem.text = itemInfo.LevelItem.TierString;
        characteristicsInfo.SetDescriptionOfCharacteristics(itemInfo);
        price.text = itemInfo.GetSalePrice().ToString();
        id = itemInfo.IdWeapon;
        sellBtn.onClick.RemoveAllListeners();
        sellBtn.onClick.AddListener(SellItem);
    }

    public void SellItem()
    {
        UIShop.instance.ButtonSoldSlot(id);
        UIShop.instance.DestroyItemInfo();
        UIShop.instance.DeleteAllItemElements();
        UIShop.instance.CreateItemsElements(ShopController.instance.GetPlayerInventory().GetAllItems());
    }
}
