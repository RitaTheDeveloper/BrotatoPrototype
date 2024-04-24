using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanelWithoutSellBtn : MonoBehaviour
{
    public Image icon;
    public Image background;
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI typeItem;
    public TextMeshProUGUI description;
    public string id;

    public void SetUp(ItemShopInfo itemInfo)
    {
        icon.sprite = itemInfo.IconWeapon;
        background.color = itemInfo.LevelItem.BackgroundColor;
        nameItem.text = itemInfo.NameWeapon;
        id = itemInfo.IdWeapon;
    }
}
