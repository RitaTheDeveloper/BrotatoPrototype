using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject lowHpImg;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject menuWithHeroSelection;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject conformationWindow;
    [SerializeField] private GameObject prompt;
    [SerializeField] private TextMeshProUGUI waveNumberTxt;
    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private TextMeshProUGUI waveCompletedTxt;
    [SerializeField] private string waveCompletedStr = "Волна пройдена!";
    [SerializeField] private TextMeshProUGUI loseTxt;
    [SerializeField] private TextMeshProUGUI winTxt;
    [SerializeField] private GameObject winSun;
    [SerializeField] private GameObject waveCompletedMenu;
    [SerializeField] private GameObject waveResultsMenu;
    [SerializeField] private GameObject upgradesMenu;
    [SerializeField] private TextMeshProUGUI waveResultsTxt;
    [SerializeField] private GameObject abilitySelectionPanel;
    [SerializeField] private GameObject winAndLosePanel;
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
    [SerializeField] private UIWaveResults uIWaveResults;
    [SerializeField] private UIUnlockedHeroes uiUnlockedHeroes;
    private Animator _animator;

    [Header("for player:")]
    [SerializeField] private UIHealth [] uIHealths;
    [SerializeField] private UISatiety[] uISatieties;

    [SerializeField] private Slider levelSlider;
    [SerializeField] private TextMeshProUGUI levelTxt;

    [SerializeField] private CharacteristicsUI [] characteristicsUIs;
    [SerializeField] private AllAbilities allAbilities;

    private TextAnim textAnim;
    private int _numberOfLeveledUpForCurrentWave;
    private GameObject _levelUp;
    private GameObject _foodUp;
    private GameObject _woodUp;
    private Color _startTimeColor;
    private HeroSelectionPanel _heroSelectionPanel;

    [SerializeField] private ShopPhrasesController shopPhrasesController;

    private void Awake()
    {
        instance = this;
        _animator = GetComponent<Animator>();
        AllOff();
        _startTimeColor = timeTxt.color;
        textAnim = GetComponent<TextAnim>();
        //menuWithHeroSelection.SetActive(true);
        mainMenu.SetActive(true);
        _heroSelectionPanel = menuWithHeroSelection.GetComponent<HeroSelectionPanel>();
        shopPhrasesController = gameObject.GetComponent<ShopPhrasesController>();
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
        PlayShopBackgroundSound(false);
        shop.DestroyAllPopUpWindows();
        AllOff();
        //OpenCloseWindow.CloseWindow(shop.gameObject);
        GameManager.instance.StartNextWave();
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.PlayBackgroundMusicFromShop();
        }
        if (shopPhrasesController != null)
        {
            shopPhrasesController.OnShopOut();
        }
    }

    public void WaveIsCompleted(int numberOfLeveledUpForCurrentWave)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMovement(false);
        }
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.ChangeBackgroundMusicToPercs();
        }
        _numberOfLeveledUpForCurrentWave = numberOfLeveledUpForCurrentWave;
        // анимация
        textAnim.TypingText(waveCompletedTxt, waveCompletedStr, 0.5f);
        LeanTween.alpha(waveCompletedMenu.GetComponent<RectTransform>(), 1f, 1f).setEase(LeanTweenType.easeInCirc);
        OpenCloseWindow.OpenWindowWithDelay(waveResultsMenu, 2.5f);
        textAnim.TypingText(waveResultsTxt, "Итоги волны " + GameManager.instance.WaveCounter + " из 15", 3f);

        uIWaveResults.UpdateWaveResults(GameManager.instance.player.GetComponent<PlayerCharacteristics>());
    }

    public void WaveCompletedMenuOn(int numberOfLeveledUpForCurrentWave)
    {
        PlayerCharacteristics playerCharacteristics = GameManager.instance.player.GetComponent<PlayerCharacteristics>();
        _numberOfLeveledUpForCurrentWave = numberOfLeveledUpForCurrentWave;
        // waveCompletedMenu.SetActive(true);
        OpenCloseWindow.OpenWindow(waveCompletedMenu);

        if (_numberOfLeveledUpForCurrentWave > 0)
        {
            AbilitySelectionPanelOn();
            foreach(CharacteristicsUI characteristicsUI in characteristicsUIs)
            {
                characteristicsUI.UpdateCharacterisctics(playerCharacteristics);
            }            
            _numberOfLeveledUpForCurrentWave--;
        }
        else
        {
            //AbilitySelectionPanelOff();
            WaveCompletedMenuOff();
            foreach (CharacteristicsUI characteristicsUI in characteristicsUIs)
            {
                characteristicsUI.UpdateCharacterisctics(playerCharacteristics);
            }
            OpenShop();
        }
    }

    public void OpenShop()
    {        
        shop.UpdateUIShop();
        shop.gameObject.SetActive(true);
        shopPhrasesController.OnShopIn();
        PlaySoundOpenDoorInShop();
        PlayShopBackgroundSound(is_play: true);
        //OpenCloseWindow.OpenWindow(shop.gameObject);
    }

    public void WaveCompletedMenuOff()
    {
        OpenCloseWindow.CloseWindow(waveCompletedMenu);
        Color tmpcolor = waveCompletedMenu.GetComponent<Image>().color;
        tmpcolor.a = 0f;
        waveCompletedMenu.GetComponent<Image>().color = tmpcolor;
       // waveCompletedMenu.SetActive(false);
    }

    private void AbilitySelectionPanelOn()
    {
        //characteristicsUI.UpdateCharacterisctics();
        abilitySelectionPanel.SetActive(true);
        AllOff();
        OpenCloseWindow.OpenWindow(upgradesMenu);
    }

    private void AbilitySelectionPanelOff()
    {
        
        abilitySelectionPanel.SetActive(false);
    }

    public void Win()
    {
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.StopAllMusic();
        }
        PlayWinSound();
        winAndLosePanel.SetActive(true);
        // делаем виньетку
        LeanTween.alpha(winAndLosePanel.GetComponent<RectTransform>(), 1f, 1f).setEase(LeanTweenType.easeInCirc);
        //OpenCloseWindow.OpenWindowWithDelay(winSun, 1f);
        textAnim.TypingText(winTxt, "Победа!", 0.5f);
        winPanel.SetActive(true);
        //restartBtn.SetActive(true);
        //menuBtn.SetActive(true);
    }

    public void Lose()
    {
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.StopAllMusic();
        }
        PlayLoseSound();
        winAndLosePanel.SetActive(true);
        // делаем виньетку
        LeanTween.alpha(winAndLosePanel.GetComponent<RectTransform>(), 1f, 1f).setEase(LeanTweenType.easeInCirc);
        // пишем текст
        textAnim.TypingText(loseTxt, "Поражение!", 0.5f);
        losePanel.SetActive(true);

        //restartBtn.SetActive(true);
        // menuBtn.SetActive(true);
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
        //menuWithHeroSelection.SetActive(true);
        mainMenu.SetActive(true);
        ResetMusic();
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.PlayMainMenuMusicFromFight();
        }
        //OpenCloseWindow.OpenWindow(menuWithHeroSelection);
    }

    private void AllOff()
    {
        mainMenu.SetActive(false);
        winAndLosePanel.SetActive(false);
        pauseMenu.SetActive(false);
        conformationWindow.SetActive(false);
        settingsPanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        restartBtn.SetActive(false);
        //waveCompletedMenu.SetActive(false);
        menuBtn.SetActive(false);

        OpenCloseWindow.CloseWindow(waveResultsMenu);
        //OpenCloseWindow.CloseWindow(shop.gameObject);
        OpenCloseWindow.OpenWindow(waveCompletedMenu);
        OpenCloseWindow.CloseWindow(upgradesMenu);
        waveCompletedTxt.text = "";
        shop.gameObject.SetActive(false);
        menuWithHeroSelection.SetActive(false);
        PlayShopBackgroundSound(false);
    }

    public void OnClickOpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void OnClickSettingsPanelExit()
    {
        settingsPanel.SetActive(false);
    }

    public void DisplayHealth(float currentHp, float startHp, float maxStartHp, float satiety)
    {
        foreach(UIHealth uIHealth in uIHealths)
        {
            uIHealth.DisplayHealth(currentHp, startHp, maxStartHp, satiety);
        }        
    }

    public void DisplaySatiety(float currentSatiety, float startSatiety, bool isFull)
    {
        foreach(UISatiety uISatiety in uISatieties)
        {
            uISatiety.DisplaySatiety(currentSatiety, startSatiety, isFull);
        }        
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

    private void PlaySoundOpenDoorInShop()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("DoorInShop");
        }
    }

    private void PlayShopBackgroundSound(bool is_play)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayShopBackGround(is_play);
        }
    }

    public void OpenMenuHeroSelection()
    {
        AllOff();
        menuWithHeroSelection.SetActive(true);
        _heroSelectionPanel.SelectedIcon();
    }

    public void PauseMenu(bool turnOn)
    {
        pauseMenu.SetActive(turnOn);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickContinue()
    {
        GameManager.instance.PauseOff();
    }

    public void OnClickMenuInPause()
    {
        conformationWindow.SetActive(true);
    }

    public void OnClickYesGoToMenu()
    {
        conformationWindow.SetActive(false);
        OnClickMenu();
        GameManager.instance.PauseOff();
        PlayMainMenuMusic();
    }

    public void OnClickNoGoToMenu()
    {
        conformationWindow.SetActive(false);
    }

    public void ShowPromptInGame()
    {
        PromptAnimation();
    }

    private void PromptAnimation()
    {
        _animator.SetTrigger("ShowPrompt");
    }

    public void DisplayUnLockedNewHeroes(List<GameObject> unlockedPlayers)
    {
        if(unlockedPlayers.Count > 0)
        {
            uiUnlockedHeroes.gameObject.SetActive(true);
            uiUnlockedHeroes.DisplayUnlockedHeroes(unlockedPlayers);
        }
        else
        {
            uiUnlockedHeroes.gameObject.SetActive(false);
        }

    }

    private void PlayLoseSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("LoseSound");
        }
    }

    private void PlayWinSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("WinSound");
        }
    }

    private void ResetMusic()
    {
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.ResetState();
        }
    }

    private void PlayMainMenuMusic()
    {
        if (BackgroundMusicManger.instance != null)
        {
            BackgroundMusicManger.instance.PlayMainMenuMusicFromFight();
            BackgroundMusicManger.instance.PlayMainMenuMusicFromShopMusic();
        }
    }

    public void LowHPImageOn(bool isOn)
    {
        lowHpImg.SetActive(isOn);
    }

    public UIShop GetUIShop()
    {
        return shop;
    }
}

