using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private Transform segmentsTransform;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI currentNumberOfWavesTxt;

    CharacterLevelSetting[] _levelSettings;
    private int _maxNumberOfWaves;
    private List<int> _numberOfWavesOfSegmentList = new List<int>();

    public void CreateProgressBar(CharacterLevelSetting[] levelSettings, int currentNumberOfwaves, CharacterLevel characterLevel)
    {
        DestroyAllSegments();

        _levelSettings = levelSettings;
        var size = _levelSettings.Length;

        for (int i = 0; i < size; i++)
        {
            var segment = Instantiate(segmentPrefab, segmentsTransform);
            UISegmentCharacterProgressBar uiSegment = segment.GetComponent<UISegmentCharacterProgressBar>();
            uiSegment.Init(_levelSettings[i].numberOfWaves, characterLevel, currentNumberOfwaves);
            _numberOfWavesOfSegmentList.Add(_levelSettings[i].numberOfWaves);
        }

        DisplayCurrentNumberOfWaves(currentNumberOfwaves);
    }

    private void DestroyAllSegments()
    {
        foreach (Transform element in segmentsTransform)
        {
            Destroy(element.gameObject);
        }
    }

    private void DisplayCurrentNumberOfWaves(int currentNumberOfwaves)
    {
        //var size = _levelSettings.Length;
        //_maxNumberOfWaves = _levelSettings[size - 1].numberOfWaves;
        //progressSlider.value = (float)currentNumberOfwaves / (float)_maxNumberOfWaves;

        currentNumberOfWavesTxt.text = currentNumberOfwaves.ToString();

        float baseStep = 0f;
        float myValue = 0f;
        float partValue = 0f;

        if (currentNumberOfwaves != 0)
        {
            baseStep = 1f / (float)_numberOfWavesOfSegmentList.Count;
            myValue = 0f;
            int lastnumberOfWavesCompleted = 0;
            int lastNumberNotCompleted = 0;
            for (int i = 0; i < _numberOfWavesOfSegmentList.Count; i++)
            {
                if (currentNumberOfwaves >= _numberOfWavesOfSegmentList[i])
                {
                    myValue += baseStep;
                    lastnumberOfWavesCompleted = _numberOfWavesOfSegmentList[i];
                    if (i < _numberOfWavesOfSegmentList.Count - 1)
                    {
                        lastNumberNotCompleted = _numberOfWavesOfSegmentList[i + 1];
                    }
                    else
                    {
                        lastNumberNotCompleted = _numberOfWavesOfSegmentList[i];
                    }
                }
            }
            int diffOflastSegments = lastNumberNotCompleted - lastnumberOfWavesCompleted;
            int partOfCurrentNumber = currentNumberOfwaves - lastnumberOfWavesCompleted;
            partValue = (float)partOfCurrentNumber / (float)diffOflastSegments;

        }


        progressSlider.value = myValue + baseStep * partValue;
    }
}
