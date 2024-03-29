using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI waveNumberTxt;
    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private GameObject waveCompletedMenu;
    [SerializeField] private Button nextWaveBtn;
    [SerializeField] private GameObject abilitySelectionPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject restartBtn;
    [SerializeField] private GameObject menuBtn;
    [SerializeField] private Transform levelUpMenu;
    [SerializeField] private GameObject leveUpUiPrefab;
    [SerializeField] private TextMeshProUGUI amountOfCurrencyTxt;
    [SerializeField] private UIShop shop;

    [Header("for player:")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthTxt;

    [SerializeField] private Slider levelSlider;
    [SerializeField] private TextMeshProUGUI levelTxt;

    [SerializeField] private CharacteristicsUI characteristicsUI;
    [SerializeField] private AllAbilities allAbilities;

    private int _numberOfLeveledUpForCurrentWave;

    private void Awake()
    {
        instance = this;
        AllOff();
    }

    public void ShowTime(float currentTime)
    {
        // string timeString = string.Format("{0:00}:{1:00}", (Mathf.CeilToInt(currentTime) / 60), (Mathf.CeilToInt(currentTime) % 60));
        int time = Mathf.CeilToInt(currentTime);
        if (time < 6)
        {
            timeTxt.color = Color.red;
        }
        else
        {
            timeTxt.color = Color.white;
        }

        timeTxt.text = time.ToString();
    }

    public void OkOnClick()
    {
        PlaySoundOfButtonPress();
        WaveCompletedMenuOn(_numberOfLeveledUpForCurrentWave);
        allAbilities.ChooseAbilitiesForProposeAbilities();       
    }

    public void OnClickNextWave()
    {
        AllOff();
        GameManager.instance.StartNextWave();
    }

    public void WaveCompletedMenuOn(int numberOfLeveledUpForCurrentWave)
    {
        _numberOfLeveledUpForCurrentWave = numberOfLeveledUpForCurrentWave;
        Debug.Log("������� ������� = " + numberOfLeveledUpForCurrentWave);
        waveCompletedMenu.SetActive(true);

        if (_numberOfLeveledUpForCurrentWave > 0)
        {
            AbilitySelectionPanelOn();
            characteristicsUI.UpdateCharacterisctics();
            _numberOfLeveledUpForCurrentWave--;
        }
        else
        {
            AbilitySelectionPanelOff();
            characteristicsUI.UpdateCharacterisctics();
            // ��������� �������
            OpenShop();
            nextWaveBtn.gameObject.SetActive(true);
        }
    }

    public void OpenShop()
    {
        shop.gameObject.SetActive(true);
        shop.UpdateUIShop();
    }

    public void WaveCompletedMenuOff()
    {
        waveCompletedMenu.SetActive(false);
    }

    private void AbilitySelectionPanelOn()
    {
        //characteristicsUI.UpdateCharacterisctics();
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
        PlaySoundOfButtonPress();
        AllOff();
        RemoveAllLevelUpElements();
        GameManager.instance.Restart();
        shop.GetComponent<ShopController>().ResetShop();
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
        waveCompletedMenu.SetActive(false);
        menuBtn.SetActive(false);
        nextWaveBtn.gameObject.SetActive(false);
        shop.gameObject.SetActive(false);
    }

    public void DisplayHealth(float currentHp, float startHp)
    {
        if (currentHp < 0)
        {
            currentHp = 0;
        }
        else if (currentHp > 0f && currentHp < 1f)
        {
            currentHp = 1f;
        }

        healthSlider.value = currentHp / startHp;
        healthTxt.text = (int)currentHp + "/" + (int)startHp;
    }

    public void DisplayWaveNumber(int waveNumber)
    {
        waveNumberTxt.text = "����� " + waveNumber;
    }

    public void DisplayLevel(int currentLvl, float XpPercentage)
    {
        levelSlider.value = XpPercentage;
        levelTxt.text = "LV." + currentLvl;
    }

    public void DisplayAmountOfCurrency(int totalAmountOfCurrency)
    {
        amountOfCurrencyTxt.text = totalAmountOfCurrency.ToString();
    }

    public void DisplayLevelUp()
    {
        PlaySoundOfLevelUp();
        Instantiate(leveUpUiPrefab, levelUpMenu.transform);
    }

    public void RemoveAllLevelUpElements()
    {
        foreach(Transform levelUpElement in levelUpMenu.transform)
        {
            Destroy(levelUpElement.gameObject);
        }
    }

    private void PlaySoundOfButtonPress()
    {

    }

    private void PlaySoundOfLevelUp()
    {

    }
}
