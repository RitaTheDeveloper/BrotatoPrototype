using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAccountProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private Transform segmentsTransform;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI currentNumberOfLevelsTxt;
    
    AccountLevelSetting[] _accountLevelSettings;
    private int _maxNumberOfLevels;

    public void CreateProgressBar(AccountLevelSetting[] accountLevelSettings, int sumOfLevels)
    {
        DestroyAllSegments();

        _accountLevelSettings = accountLevelSettings;
        var size = _accountLevelSettings.Length;

        for (int i = 0; i < size; i++)
        {
            var segment = Instantiate(segmentPrefab, segmentsTransform);
            UISegmentAccount uiSegment = segment.GetComponent<UISegmentAccount>();
            UICharacterInfoForAccount info = _accountLevelSettings[i].unlockable—haracter.GetComponent<UICharacterInfoForAccount>();
            bool unlocked = false;
            if (sumOfLevels >= _accountLevelSettings[i].numberOfCharacterLevels)
                unlocked = true;
            uiSegment.Init(info, _accountLevelSettings[i].numberOfCharacterLevels, unlocked);
        }

        DisplayCurrentNumberOfLevels(sumOfLevels);
    }

    private void DestroyAllSegments()
    {
        foreach (Transform element in segmentsTransform)
        {
            Destroy(element.gameObject);
        }
    }

    private void DisplayCurrentNumberOfLevels(int currentNumberOfLevels)
    {
        var size = _accountLevelSettings.Length;
        _maxNumberOfLevels = _accountLevelSettings[size - 1].numberOfCharacterLevels;
        progressSlider.value = (float)currentNumberOfLevels / (float)_maxNumberOfLevels;

        currentNumberOfLevelsTxt.text = currentNumberOfLevels.ToString();
    }
}
