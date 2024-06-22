using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI typeItem;
    public TextMeshProUGUI tierItem;
    public Button sellBtn;
    public TextMeshProUGUI price;
    public string id;
    [SerializeField] private CharacteristicsInfoPanelForWeaponAndItem characteristicsInfo;

    private bool isWeapon;

    public void SetUp(ItemShopInfo itemInfo)
    {
        nameItem.text = itemInfo.NameWeapon;
        tierItem.text = itemInfo.LevelItem.TierString;
        characteristicsInfo.SetDescriptionOfCharacteristics(itemInfo);
        price.text = itemInfo.GetSalePrice().ToString();
        id = itemInfo.IdWeapon;
        sellBtn.onClick.RemoveAllListeners();
        sellBtn.onClick.AddListener(SellItem);

        isWeapon = itemInfo.gameObject.GetComponent<Weapon>() ? true : false;
        if (isWeapon)
        {
            if (itemInfo.GetComponent<Weapon>().type == Weapon.Type.Melee)
            {
                typeItem.text = "ближний бой";
            }
            else
            {
                typeItem.text = "дальний бой";
            }
        }
        else
        {
            typeItem.text = "снаряжение";
        }
    }

    public void SellItem()
    {
        UIShop.instance.ButtonSoldSlot(id);
        UIShop.instance.DestroyItemInfo(); 
        if (isWeapon)
        {
            UIShop.instance.DeleteAllWeaponElements();
            UIShop.instance.CreateWeaponElements(ShopController.instance.GetWeaponController().GetAllWeapons());
        }
        else
        {
            UIShop.instance.DeleteAllItemElements();
            UIShop.instance.CreateItemsElements(ShopController.instance.GetPlayerInventory().GetAllItems());
        }
    }
}
