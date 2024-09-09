using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInfoLevelReward : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _iconAndTxtPrefab;
    [SerializeField] private GameObject _panelPrefab;
    [SerializeField] private Sprite _glowBox;
    [SerializeField] UICharacteristicScriptable uICharacteristicScriptable;
    CharacterLevel _characterLevel;
    private CharacteristicBuff[] _baffs;
    private TooltipComponent tooltipComponent;
    private Sprite _normalBox;

    private void Awake()
    {
        _normalBox = GetComponent<Image>().sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {        
        ChangeSprite(_glowBox);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeSprite(_normalBox);
    }

    public void SetDescriptionLevelReward(CharacterLevel _characterLevel)
    {
        _baffs = _characterLevel.Baffs;
        //for (int i = 0; i < _baffs.Length; i++)
        //{
        //    ReturnBuffIncreaseDescription buffDes = new ReturnBuffIncreaseDescription();
        //     s += "+ " + _baffs[i].value + buffDes.BuffIncreaseDescription(_baffs[i].characteristic) + "\n";
        //}

        tooltipComponent = GetComponent<TooltipComponent>();

        var _panel = Instantiate(_panelPrefab);
        for (int i = 0; i < _baffs.Length; i++)
        {
            var uiPrefab = Instantiate(_iconAndTxtPrefab, _panel.transform);
            foreach (var buff in uICharacteristicScriptable.uiCharacteristics)
            {
                if (_baffs[i].characteristic == buff.characteristic)
                {
                    Sprite icon = buff.icon;
                    string des = buff.name;
                    string value = _baffs[i].value.ToString();
                    uiPrefab.GetComponent<UIForOneBuff>().SetUI(icon, des, value);
                }
            }
        }
       
        tooltipComponent.SetUIPrefab(_panel);
        tooltipComponent.SetText("");
    }

    private void ChangeSprite(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }

}
