using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISegmentCharacterProgressBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberOfWavesTxt;
    [SerializeField] private UIInfoLevelReward _uiInfoLevelReward;
    [SerializeField] private Image _boxImg;
    [SerializeField] private Sprite _openedBoxSprite;
    
    private void Awake()
    {
        
    }

    public void Init(int numberOfWaves, CharacterLevel characterLevel, int currentNumberOfWaves)
    {
        _numberOfWavesTxt.text = numberOfWaves.ToString() + " волн";
        _uiInfoLevelReward.SetDescriptionLevelReward(characterLevel);

        if(currentNumberOfWaves >= numberOfWaves)
        {
            _boxImg.sprite = _openedBoxSprite;
            _uiInfoLevelReward.BoxIsOpened = true;
        }
    }


}
