using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotItemForSaleData : MonoBehaviour
{
    public int SlotNumber;
    public string SlotEntytiID;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textType;
    public TextMeshProUGUI textCost;
    public TextMeshProUGUI description;
    public Image image;
    public TextMeshProUGUI lockButtonText;
    public Image backgroud;
    public Button buyBtn;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }
    public void OnClickBuyItem()
    {
        buyBtn.onClick.AddListener(OnBuyItem);
    }

    public void OnBuyItem()
    {
        UIShop.instance.ButtonBuySlot(SlotNumber);
    }
}
