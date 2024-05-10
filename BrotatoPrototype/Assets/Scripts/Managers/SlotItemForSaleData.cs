using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotItemForSaleData : MonoBehaviour
{
    public int SlotNumber;
    public string SlotEntytiID;
    public GameObject pot;
    [SerializeField] private Animator _potAnimator;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textTier;
    public TextMeshProUGUI textType;        
    public TextMeshProUGUI textCost;
    public Image image;
    public TextMeshProUGUI lockButtonText;
    public Button buyBtn;
    [SerializeField] private CharacteristicsInfoPanelForWeaponAndItem characteristicsInfo;


    private void Awake()
    {
       // _potAnimator = pot.GetComponentInChildren<Animator>();
    }

    public void DisplayInfoForWeapon(ItemShopInfo w, int currentWave)
    {
        textName.text = w.NameWeapon;
        if (w.GetComponent<Weapon>().type == Weapon.Type.Melee)
        {
            textType.text = "ближний бой";
        }
        else
        {
            textType.text = "дальний бой";
        }
        textTier.text = w.LevelItem.TierString;

        textCost.text = w.GetPrice(currentWave).ToString();
        _potAnimator.SetTrigger("change");
        image.sprite = w.IconWeapon;
        SetCharacteristicsInfo(w);
        buyBtn.onClick.RemoveAllListeners();
        OnClickBuyItem();
    }

    public void DisplayInfoForItem(StandartItem it, int currentWave)
    {
        textName.text = it.ShopInfoItem.NameWeapon;
        textType.text = "снаряжение";
        textTier.text = it.ShopInfoItem.LevelItem.TierString;
        textCost.text = it.ShopInfoItem.GetPrice(currentWave).ToString();
        image.sprite = it.ShopInfoItem.IconWeapon;
        SetCharacteristicsInfo(it.GetComponent<ItemShopInfo>());
        buyBtn.onClick.RemoveAllListeners();
        OnClickBuyItem();
    }

    public void OnClickBuyItem()
    {
        buyBtn.onClick.AddListener(OnBuyItem);
    }

    public void OnBuyItem()
    {
        Debug.Log("хочу купить");
        UIShop.instance.ButtonBuySlot(SlotNumber);
    }

    public void PotOff()
    {
        pot.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        textName.text = "";
        textTier.text = "";
        textType.text = "";
        characteristicsInfo.DeleteInfo();
    }

    public void PotOn()
    {
        pot.SetActive(true);
        buyBtn.gameObject.SetActive(true);
    }   

    public void SetCharacteristicsInfo(ItemShopInfo itemInfo)
    {
        characteristicsInfo.SetDescriptionOfCharacteristics(itemInfo);
    }
}
