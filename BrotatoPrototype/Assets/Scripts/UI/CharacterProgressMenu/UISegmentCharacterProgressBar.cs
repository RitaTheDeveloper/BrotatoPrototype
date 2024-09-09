using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISegmentCharacterProgressBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberOfWavesTxt;
    [SerializeField] private UIInfoLevelReward _uiInfoLevelReward;

    public void Init(int numberOfWaves, CharacterLevel characterLevel)
    {
        _numberOfWavesTxt.text = numberOfWaves.ToString() + " волн";
        _uiInfoLevelReward.SetDescriptionLevelReward(characterLevel);
    }
}
