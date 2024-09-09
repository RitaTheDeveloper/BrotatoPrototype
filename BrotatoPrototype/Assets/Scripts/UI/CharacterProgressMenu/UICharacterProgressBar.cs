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

    public void CreateProgressBar(CharacterLevelSetting[] levelSettings, int currentNumberOfwaves, CharacterLevel characterLevel)
    {
        DestroyAllSegments();

        _levelSettings = levelSettings;
        var size = _levelSettings.Length;

        for (int i = 0; i < size; i++)
        {
            var segment = Instantiate(segmentPrefab, segmentsTransform);
            UISegmentCharacterProgressBar uiSegment = segment.GetComponent<UISegmentCharacterProgressBar>();
            uiSegment.Init(_levelSettings[i].numberOfWaves, characterLevel);
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
        var size = _levelSettings.Length;
        _maxNumberOfWaves = _levelSettings[size - 1].numberOfWaves;
        progressSlider.value = (float)currentNumberOfwaves / (float)_maxNumberOfWaves;

        currentNumberOfWavesTxt.text = currentNumberOfwaves.ToString();
    }
}
