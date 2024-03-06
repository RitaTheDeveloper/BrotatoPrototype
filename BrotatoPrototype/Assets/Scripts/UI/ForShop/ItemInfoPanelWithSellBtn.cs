using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanelWithSellBtn : MonoBehaviour
{
    public Image icon;
    public Image background;
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI description;    
    public Button sellBtn;
    public TextMeshProUGUI price;

    public void SetUp(string _nameItem, Sprite _icon, Color _tierColor, string _description)
    {
        icon.sprite = _icon;
        background.color = _tierColor;
        nameItem.text = _nameItem;
        description.text = _description;
        sellBtn.onClick.RemoveAllListeners();
        // вот тут нужно повесить на кнопку метод с индексом продаваемого оружия
       // sellBtn.onClick.AddListener()
    }
}
