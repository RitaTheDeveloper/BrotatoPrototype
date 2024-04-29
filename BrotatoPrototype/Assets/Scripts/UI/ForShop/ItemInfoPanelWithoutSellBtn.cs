using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanelWithoutSellBtn : MonoBehaviour
{
    //public Image icon;
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI typeItem;
    public TextMeshProUGUI tierItem;
    public TextMeshProUGUI description;
    [SerializeField] private CharacteristicsInfoPanelForWeaponAndItem characteristicsInfo;

    public void SetUp(ItemShopInfo itemInfo)
    {       
        nameItem.text = itemInfo.NameWeapon;
        typeItem.text = "снаряжение";
        tierItem.text = itemInfo.LevelItem.TierString;
        characteristicsInfo.SetDescriptionOfCharacteristics(itemInfo);
    }
}
