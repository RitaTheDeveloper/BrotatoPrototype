using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UISatiety : MonoBehaviour
{
    [SerializeField] private Slider satietySlider;
    [SerializeField] private TextMeshProUGUI satietyTxt;
    [SerializeField] private GameObject pointer;

    public void DisplaySatiety(float currentSatiety, float startSatiety, bool isFull)
    {
        satietySlider.value = currentSatiety / startSatiety;
        satietyTxt.text = (float)System.Math.Round(currentSatiety, 1) + "/" + startSatiety;
        pointer.SetActive(!isFull);
    }
}
