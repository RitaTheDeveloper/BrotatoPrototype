using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] private GameObject menuWithHeroSelection;
    [SerializeField] private TextMeshProUGUI waveNumberTxt;
    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private GameObject waveCompletedMenu;
    [SerializeField] private GameObject abilitySelectionPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject restartBtn;
    [SerializeField] private GameObject menuBtn;
    [SerializeField] private Transform levelUpMenu;
    [SerializeField] private Transform foodAndWoodMenu;
    [SerializeField] private GameObject leveUpUiPrefab;
    [SerializeField] private GameObject foodUpUiPrefab;
    [SerializeField] private GameObject woodUpUiPrefab;
    [SerializeField] private TextMeshProUGUI amountOfCurrencyTxt;
    [SerializeField] private UIShop shop;

    [Header("for player:")]
    [SerializeField] private UIHealth [] uIHealths;

    [SerializeField] private UISatiety uISatiety;

    [SerializeField] private Slider levelSlider;
    [SerializeField] private TextMeshProUGUI levelTxt;

    [SerializeField] private CharacteristicsUI characteristicsUI;
    [SerializeField] private AllAbilities allAbilities;

    private int _numberOfLeveledUpForCurrentWave;
    private GameObject _levelUp;
    private GameObject _foodUp;
    private GameObject _woodUp;
    private Color _startTimeColor;

    private void Awake()
    {
        instance = this;
        AllOff();
        menuWithHeroSelection.SetActive(true);
        _startTimeColor = timeTxt.color;
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
            timeTxt.color = _startTimeColor;
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
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.PlayBackgroundMusicFromShop();
        }
    }

    public void WaveCompletedMenuOn(int numberOfLeveledUpForCurrentWave)
    {
        PlayerCharacteristics playerCharacteristics = GameManager.instance.player.GetComponent<PlayerCharacteristics>();
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMovement(false);
        }
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.ChangeBackgroundMusicToPercs();
        }
        _numberOfLeveledUpForCurrentWave = numberOfLeveledUpForCurrentWave;
        waveCompletedMenu.SetActive(true);

        if (_numberOfLeveledUpForCurrentWave > 0)
        {
            AbilitySelectionPanelOn();
            characteristicsUI.UpdateCharacterisctics(playerCharacteristics);
            _numberOfLeveledUpForCurrentWave--;
        }
        else
        {
            AbilitySelectionPanelOff();
            characteristicsUI.UpdateCharacterisctics(playerCharacteristics);
            // ��������� �������
            OpenShop();
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
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.PlayMainMenuMusicFromFight();
        }
        winPanel.SetActive(true);
        restartBtn.SetActive(true);
        menuBtn.SetActive(true);
    }

    public void Lose()
    {
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.PlayMainMenuMusicFromFight();
        }
        losePanel.SetActive(true);
        restartBtn.SetActive(true);
        menuBtn.SetActive(true);
    }

    public void OnClickRestart()
    {
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.ReloadManager();
        }
        PlaySoundOfButtonPress();
        AllOff();
        RemoveAllUpElements();
        GameManager.instance.Restart();
        shop.GetComponent<ShopController>().ResetShop();
    }

    public void OnClickMenu()
    {
        // SceneManager.LoadScene(0);
        AllOff();
        RemoveAllUpElements();
        GameManager.instance.DestroyGameScene();
        menuWithHeroSelection.SetActive(true);
    }

    private void AllOff()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        restartBtn.SetActive(false);
        waveCompletedMenu.SetActive(false);
        menuBtn.SetActive(false);
        shop.gameObject.SetActive(false);
        menuWithHeroSelection.SetActive(false);
    }

    public void DisplayHealth(float currentHp, float startHp, float maxStartHp)
    {
        foreach(UIHealth uIHealth in uIHealths)
        {
            uIHealth.DisplayHealth(currentHp, startHp, maxStartHp);
        }        
    }

    public void DisplaySatiety(float currentSatiety, float startSatiety, bool isFull)
    {
        uISatiety.DisplaySatiety(currentSatiety, startSatiety, isFull);
    }

    public void DisplayWaveNumber(int waveNumber)
    {
        waveNumberTxt.text = "волна " + waveNumber;
    }

    public void DisplayLevel(int currentLvl, float XpPercentage)
    {
        levelSlider.value = XpPercentage;
        levelTxt.text = "ур." + currentLvl;
    }

    public void DisplayAmountOfCurrency(int totalAmountOfCurrency)
    {
        amountOfCurrencyTxt.text = totalAmountOfCurrency.ToString();
    }

    public void DisplayLevelUp(int numberOfLevelUp)
    {
        PlaySoundOfLevelUp();
        if (numberOfLevelUp < 2)
        {
            _levelUp =  Instantiate(leveUpUiPrefab, levelUpMenu.transform);
            _levelUp.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }
        else
        {
            _levelUp.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            _levelUp.GetComponentInChildren<TextMeshProUGUI>().text = numberOfLevelUp + "<sup>х</sup>";
        }
                
    }

    public void DisplayFoodUp(int numberOfFoodUp)
    {
        PlaySoundOfFoodUp();
        if (numberOfFoodUp < 2)
        {
            _foodUp = Instantiate(foodUpUiPrefab, foodAndWoodMenu.transform);
            _foodUp.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }
        else
        {
            _foodUp.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            _foodUp.GetComponentInChildren<TextMeshProUGUI>().text = numberOfFoodUp + "<sup>х</sup>";
        }
    }
    public void DisplayWoodUp(int numberOfWoodUp)
    {
        PlaySoundOfWoodUp();
        if (numberOfWoodUp < 2)
        {
            _woodUp = Instantiate(woodUpUiPrefab, foodAndWoodMenu.transform);
            _woodUp.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }
        else
        {
            _woodUp.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            _woodUp.GetComponentInChildren<TextMeshProUGUI>().text = numberOfWoodUp + "<sup>х</sup>";
        }
    }

    public void RemoveAllUpElements()
    {
        RemoveAllFoodUpAndWoodUp();
        RemoveAllLevelUpElements();
    }

    private void RemoveAllLevelUpElements()
    {
        foreach(Transform levelUpElement in levelUpMenu.transform)
        {
            Destroy(levelUpElement.gameObject);
        }
    }

    private void RemoveAllFoodUpAndWoodUp()
    {
        foreach (Transform upElement in foodAndWoodMenu.transform)
        {
            Destroy(upElement.gameObject);
        }
    }

    private void PlaySoundOfButtonPress()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("ClickElement");
        }
    }

    private void PlaySoundOfLevelUp()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("LevelUp");
        }
    }

    private void PlaySoundOfFoodUp()
    {
        if (AudioManager.instance != null)
        {
            //
        }
    }

    private void PlaySoundOfWoodUp()
    {
        if (AudioManager.instance != null)
        {
            //
        }
    }
}
