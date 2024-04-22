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
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textType;
    public TextMeshProUGUI textCost;
    public TextMeshProUGUI description;
    public Image image;
    public TextMeshProUGUI lockButtonText;
    public Image backgroud;
    public Button buyBtn;
    [SerializeField] private CharacteristicsInfoPanelForWeaponAndItem characteristicsInfo;

    private void Awake()
    {
        
    }

    private void Start()
    {
       // PotOff();
    }

    public void OnClickBuyItem()
    {
        buyBtn.onClick.AddListener(OnBuyItem);
    }

    public void OnBuyItem()
    {
        UIShop.instance.ButtonBuySlot(SlotNumber);
    }

    public void PotOff()
    {
        pot.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        textName.text = "";
        textType.text = "";
        //description.text = "";
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
