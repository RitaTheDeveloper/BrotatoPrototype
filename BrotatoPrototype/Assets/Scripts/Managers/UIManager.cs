using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private GameObject abilitySelectionPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject restartBtn;
    [SerializeField] private GameObject menuBtn;

    [Header("for player:")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthTxt;

    [SerializeField] private Slider levelSlider;
    [SerializeField] private TextMeshProUGUI levelTxt;

    [SerializeField] private CharacteristicsUI characteristicsUI;
    [SerializeField] private AllAbilities allAbilities;

    private void Awake()
    {
        instance = this;
        AllOff();
    }

    public void ShowTime(float currentTime)
    {
        string timeString = string.Format("{0:00}:{1:00}", (Mathf.CeilToInt(currentTime) / 60), (Mathf.CeilToInt(currentTime) % 60));
        timeTxt.text = timeString;
    }

    public void OkOnClick()
    {
        AbilitySelectionPanelOff();
        allAbilities.ChooseAbilitiesForProposeAbilities();
        GameManager.instance.StartNextWave();
    }

    public void AbilitySelectionPanelOn()
    {
        characteristicsUI.UpdateCharacterisctics();
        abilitySelectionPanel.SetActive(true);
    }

    private void AbilitySelectionPanelOff()
    {
        abilitySelectionPanel.SetActive(false);
    }

    public void Win()
    {
        winPanel.SetActive(true);
        restartBtn.SetActive(true);
        menuBtn.SetActive(true);
    }

    public void Lose()
    {
        losePanel.SetActive(true);
        restartBtn.SetActive(true);
        menuBtn.SetActive(true);
    }

    public void OnClickRestart()
    {
        AllOff();
        GameManager.instance.Restart();
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void AllOff()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        restartBtn.SetActive(false);
    }

    public void DisplayHealth(float currentHp, float startHp)
    {
        if (currentHp < 0)
        {
            currentHp = 0;
        }

        healthSlider.value = currentHp / startHp;
        healthTxt.text = (int)currentHp + "/" + (int)startHp;
    }

    public void DisplayLevel(int currentLvl, float XpPercentage)
    {
        levelSlider.value = XpPercentage;
        levelTxt.text = "LV." + currentLvl;
    }
}
