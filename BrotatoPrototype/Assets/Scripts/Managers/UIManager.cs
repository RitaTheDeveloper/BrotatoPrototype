using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] TextMeshProUGUI timeTxt;

    private void Awake()
    {
        instance = this;
    }

    public void ShowTime(float currentTime)
    {
        string timeString = string.Format("{0:00}:{1:00}", (Mathf.CeilToInt(currentTime) / 60), (Mathf.CeilToInt(currentTime) % 60));
        timeTxt.text = timeString;
    }

}
