using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISegmentCharacterProgressBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberOfWavesTxt; 
    
    public void Init(int numberOfWaves)
    {
        _numberOfWavesTxt.text = numberOfWaves.ToString() + " волн";
    }
}
