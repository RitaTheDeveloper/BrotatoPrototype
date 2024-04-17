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

        maxhealthSlider.value = (maxStartHp - startHp) / maxStartHp;
        currentHealthSlider.value = currentHp / startHp;
        healthTxt.text = (int)currentHp + "/" + (int)startHp + "(" + (int)maxStartHp + ")";
    }
}
