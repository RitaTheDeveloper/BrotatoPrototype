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

    private List<int> _numberOfLevelsOfSegmentList = new List<int>();

    public void CreateProgressBar(AccountLevelSetting[] accountLevelSettings, int sumOfLevels)
    {
        DestroyAllSegments();
        _numberOfLevelsOfSegmentList = new List<int>();
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
            _numberOfLevelsOfSegmentList.Add(_accountLevelSettings[i].numberOfCharacterLevels);
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
        //var size = _accountLevelSettings.Length;
        //_maxNumberOfLevels = _accountLevelSettings[size - 1].numberOfCharacterLevels;
        //progressSlider.value = (float)currentNumberOfLevels / (float)_maxNumberOfLevels;

        currentNumberOfLevelsTxt.text = currentNumberOfLevels.ToString();

        float baseStep = 0f;
        float myValue = 0f;
        float partValue = 0f;

        if (currentNumberOfLevels != 0)
        {
            baseStep = 1f / (float)_numberOfLevelsOfSegmentList.Count;
            myValue = 0f;
            int lastnumberOfWavesCompleted = 0;
            int lastNumberNotCompleted = _numberOfLevelsOfSegmentList[0];
            for (int i = 0; i < _numberOfLevelsOfSegmentList.Count; i++)
            {
                if(currentNumberOfLevels >= _numberOfLevelsOfSegmentList[i])
                {
                    myValue += baseStep;
                    lastnumberOfWavesCompleted = _numberOfLevelsOfSegmentList[i];
                    if (i + 1 < _numberOfLevelsOfSegmentList.Count)
                    {
                        lastNumberNotCompleted = _numberOfLevelsOfSegmentList[i + 1];
                    }
                    else
                    {
                        lastNumberNotCompleted = _numberOfLevelsOfSegmentList[i];
                    }
                }
            }
            int diffOflastSegments = lastNumberNotCompleted - lastnumberOfWavesCompleted;
            int partOfCurrentNumber = currentNumberOfLevels - lastnumberOfWavesCompleted;

            partValue = (float)partOfCurrentNumber / (float)diffOflastSegments;
        }

        Debug.Log("myValue = " + myValue + " baseStep = " + baseStep + "partvalue = " + partValue);
        progressSlider.value = myValue + baseStep * partValue;
    }
}
