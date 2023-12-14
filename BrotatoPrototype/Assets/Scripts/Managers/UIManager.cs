using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private GameObject abilitySelectionPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void Awake()
    {
        instance = this;
    }

    public void ShowTime(float currentTime)
    {
        string timeString = string.Format("{0:00}:{1:00}", (Mathf.CeilToInt(currentTime) / 60), (Mathf.CeilToInt(currentTime) % 60));
        timeTxt.text = timeString;
    }

    public void OkOnClick()
    {
        AbilitySelectionPanelOff();
        GameManager.instance.StartNextWave();
    }

    public void AbilitySelectionPanelOn()
    {
        abilitySelectionPanel.SetActive(true);
    }

    private void AbilitySelectionPanelOff()
    {
        abilitySelectionPanel.SetActive(false);
    }

    public void Win()
    {
        winPanel.SetActive(true);
    }

    public void Lose()
    {
        losePanel.SetActive(true);
    }

    public void OnClickRestart()
    {

    }
}
