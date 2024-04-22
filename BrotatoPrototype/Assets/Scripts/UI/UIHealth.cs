using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private Slider maxhealthSlider;
    [SerializeField] private Slider currentHealthSlider;
    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private GameObject stub;

    public void DisplayHealth(float currentHp, float startHp, float maxStartHp)
    {
        if (currentHp < 0)
        {
            currentHp = 0;
        }
        else if (currentHp > 0f && currentHp < 1f)
        {
            currentHp = 1f;
        }

        if (startHp == maxStartHp) stub.SetActive(false);
        else stub.SetActive(true);

        maxhealthSlider.value = Mathf.CeilToInt((maxStartHp - startHp) * 100f / maxStartHp) / 100f;        
        currentHealthSlider.value = currentHp / startHp;
        healthTxt.text = (int)currentHp + "/" + (int)startHp + "(" + (int)maxStartHp + ")";
        //maxhealthSlider.value = (100 - Mathf.CeilToInt(startHp * 100f / maxStartHp)) * 0.01f;
    }
}
