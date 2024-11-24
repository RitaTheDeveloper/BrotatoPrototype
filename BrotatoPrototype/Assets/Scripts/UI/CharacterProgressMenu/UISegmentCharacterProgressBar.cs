using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UISegmentCharacterProgressBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberOfWavesTxt;
    [SerializeField] private UIInfoLevelReward _uiInfoLevelReward;
    [SerializeField] private Image _boxImg;
    [SerializeField] private Sprite _openedBoxSprite;
    public string numberOfWavesLocalization;


    private void Awake()
    {
        
    }

    public void Init(int numberOfWaves, CharacterLevel characterLevel, int currentNumberOfWaves)
    {
        numberOfWavesLocalization = numberOfWaves.ToString();
        LocalizeStringEvent localize;
        localize = _numberOfWavesTxt.gameObject.GetComponent<LocalizeStringEvent>();
        localize.SetTable("UI Text");
        localize.SetEntry("wave");
        localize.RefreshString();

        _uiInfoLevelReward.SetDescriptionLevelReward(characterLevel);

        if(currentNumberOfWaves >= numberOfWaves)
        {
            _boxImg.sprite = _openedBoxSprite;
            _uiInfoLevelReward.BoxIsOpened = true;
        }
    }


}
