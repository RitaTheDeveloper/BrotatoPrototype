using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UIForOneBuff : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _forValue;
    public string valueLocalization;


    public void SetUI(Sprite icon, string description, string value)
    {
        _forValue.text = "<color=#00e384>" + "+" + value + "</color>";

        LocalizeStringEvent localize;
        localize = _description.GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry(description);
        localize.RefreshString();
        valueLocalization = value;
        _iconImage.sprite = icon;
       // _description.text = description;
    }
}
