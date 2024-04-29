using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanelWithSellBtn : MonoBehaviour
{
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI typeItem;
    public TextMeshProUGUI tier;
    public Button sellBtn;
    public TextMeshProUGUI price;
    public string id;
    [SerializeField] private CharacteristicsInfoPanelForWeaponAndItem characteristicsInfo;

    public void SetUp(ItemShopInfo itemInfo)
    {
        //icon.sprite = itemInfo.IconWeapon;
        nameItem.text = itemInfo.NameWeapon;
        if (itemInfo.GetComponent<Weapon>().type == Weapon.Type.Melee)
        {
            typeItem.text = "ближний бой";
        }
        else
        {
            typeItem.text = "дальний бой";
        }
        tier.text = itemInfo.LevelItem.TierString;
        characteristicsInfo.SetDescriptionOfCharacteristics(itemInfo);
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
