using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UISatiety : MonoBehaviour
{
    [SerializeField] private Slider satietySlider;
    [SerializeField] private TextMeshProUGUI satietyTxt;

    public void DisplaySatiety(float currentSatiety, float startSatiety)
    {
        satietySlider.value = currentSatiety / startSatiety;
        satietyTxt.text = currentSatiety + "/" + startSatiety;
    }
}
