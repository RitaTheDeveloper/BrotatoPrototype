using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInfoLevelReward : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descriptionTxt;
    CharacterLevel _characterLevel;
    private CharacteristicBuff[] _baffs;
    private TooltipComponent tooltipComponent;

    public void SetDescriptionLevelReward(CharacterLevel _characterLevel)
    {
        string s = "";
        _baffs = _characterLevel.Baffs;
        for (int i = 0; i < _baffs.Length; i++)
        {
            ReturnBuffIncreaseDescription buffDes = new ReturnBuffIncreaseDescription();
             s += "+ " + _baffs[i].value + buffDes.BuffIncreaseDescription(_baffs[i].characteristic) + "\n";
        }
       // _descriptionTxt.text = s;
        tooltipComponent = GetComponent<TooltipComponent>();
        tooltipComponent.SetText(s);
    }

}
