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
    private UIShop _uIShop;

    private bool isWeapon;


    public void Init(UIShop uIShop)
    {
        _uIShop = uIShop;
    }

    public void SetUp(ItemShopInfo itemInfo)
    {
        nameItem.text = itemInfo.NameWeapon;
        tierItem.text = itemInfo.LevelItem.TierString;
        characteristicsInfo.SetDescriptionOfCharacteristics(itemInfo);
        price.text = itemInfo.GetSalePrice().ToString();
        id = itemInfo.IdWeapon;
        sellBtn.onClick.RemoveAllListeners();
        sellBtn.onClick.AddListener(SellItem);

        isWeapon = itemInfo.gameObject.GetComponent<BaseWeapon>() ? true : false;
        if (isWeapon)
        {
            if (itemInfo.GetComponent<BaseWeapon>().type == BaseWeapon.Type.Melee)
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
        _uIShop.SellItem(isWeapon, id);
    }
}
