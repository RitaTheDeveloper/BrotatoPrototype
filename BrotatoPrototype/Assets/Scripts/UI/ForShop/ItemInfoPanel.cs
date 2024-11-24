using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;

public class ItemInfoPanel : MonoBehaviour, IPointerClickHandler
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
        LocalizeStringEvent localize;
        localize = nameItem.GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry(itemInfo.NameWeapon);
        localize.RefreshString();

        localize = tierItem.GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry(itemInfo.LevelItem.TierString);
        localize.RefreshString();

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
                localize = typeItem.GetComponent<LocalizeStringEvent>();
                localize.SetTable("UI Text");
                localize.SetEntry("ближний бой");
                localize.RefreshString();
            }
            else
            {
                localize = typeItem.GetComponent<LocalizeStringEvent>();
                localize.SetTable("UI Text");
                localize.SetEntry("дальний бой");
                localize.RefreshString();
            }
        }
        else
        {
            localize = typeItem.GetComponent<LocalizeStringEvent>();
            localize.SetTable("UI Text");
            localize.SetEntry("снаряжение");
            localize.RefreshString();
        }
    }

    public void SellItem()
    {
        _uIShop.SellItem(isWeapon, id);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _uIShop.FrameOffWeaponSlot();
        _uIShop.FrameOffItemSlot();
        _uIShop.DestroyItemInfo();
    }
}
