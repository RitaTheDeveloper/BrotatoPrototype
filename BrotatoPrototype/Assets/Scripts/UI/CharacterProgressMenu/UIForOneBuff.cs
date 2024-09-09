using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIForOneBuff : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _description;


    public void SetUI(Sprite icon, string description, string value)
    {
        _iconImage.sprite = icon;
       // _description.text = description;
        _description.text = description + ": " + "<color=#00e384>" + "+" + value + "</color>";
    }
}
