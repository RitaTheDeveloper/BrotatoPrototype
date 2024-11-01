using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HeroSelectionPanel : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject characterProgressMenu;
    [SerializeField] private Image currentImgHero;
    [SerializeField] private TextMeshProUGUI nameHeroTxt;
    [SerializeField] private CharacteristicsUI characteristicsUI;
    [SerializeField] private Animator effectSmokeAnimator;
    [SerializeField] private TextMeshProUGUI heroDescription;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform panelOfIcons;
    [SerializeField] private GameObject blockInfo;
    [SerializeField] private TextMeshProUGUI blockTextInfo;
    [SerializeField] private GameObject heroLvlObj;
    [SerializeField] private TextMeshProUGUI heroLvl;
    [SerializeField] private Button choseBtn;
    [SerializeField] private UIAccountBtnMenuHeroSelection accountBtn;
    [SerializeField] private GameObject accountProgressMenu;
    [SerializeField] private UIComicsController uiComicsController;
    

    public GameObject[] playerPrefabs;
    private int indexOfHero = 0;
    private List<Button> _iconsBtns;
    private List<GameObject> charatersIcons = new List<GameObject>();
    private GameObject _player;
    private UiPlayerInfo _uiPlayerInfo;
    private SaveController _saveController;
    private GameManager _gameManager;
    private UIManager _uIManager;
    private void Awake()
    {
        //CreateIconsForMenu();
        indexOfHero = 0;
        _gameManager = GameManager.instance;
        _uIManager = UIManager.instance;
    }
    private void Start()
    {        
        _saveController = GameManager.instance.GetComponent<SaveController>();
        currentImgHero.sprite = playerPrefabs[indexOfHero].GetComponent<UiPlayerInfo>().player2d;
       // DisplayParametersAccountBtn();
        OnClickIconHero(indexOfHero);
        SelectedIcon();
        blockInfo.SetActive(false);
    }

    public void OnClickIconHero(int index)
    {
        blockInfo.SetActive(false);
        _player = playerPrefabs[index];
        _uiPlayerInfo = _player.GetComponent<UiPlayerInfo>();
        nameHeroTxt.text = _uiPlayerInfo.nameHero;
        heroLvlObj.SetActive(true);
        SetHeroLvl(_player);     
        heroDescription.text = _uiPlayerInfo.description;
        if (index != indexOfHero)
        {            
            ImageAlphaOff();
            effectSmokeAnimator.SetTrigger("change");
            StartCoroutine(ChangeSprite(_player, false));           
        }
        UpdateCharacteristics(_player);
        indexOfHero = index;
        choseBtn.interactable = true;
    }

    public void OnClickLockHero(int index)
    {
        _player = playerPrefabs[index];
        _uiPlayerInfo = _player.GetComponent<UiPlayerInfo>();
        nameHeroTxt.text = _uiPlayerInfo.nameHero;
        heroLvlObj.SetActive(false);
       // SetHeroLvl(_player);
        if (index != indexOfHero)
        {
            ImageAlphaOff();
            effectSmokeAnimator.SetTrigger("change");
            StartCoroutine(ChangeSprite(_player, true));
        }
        heroDescription.text = _uiPlayerInfo.description;
        indexOfHero = index;
        blockInfo.SetActive(true);
       // blockTextInfo.text = "Доступ к персонажу откроется при прохождении " + "\n" + _player.GetComponent<WaveUnlockComponent>().GetCountWaveRequired() + " волн";
        choseBtn.interactable = false;
    }

    private IEnumerator ChangeSprite(GameObject player, bool isLocked)
    {
        yield return new WaitForSeconds(0.4f);
        if (isLocked)
        {
            currentImgHero.sprite = player.GetComponent<UiPlayerInfo>().player2dLock;
        }
        else
        {
            currentImgHero.sprite = player.GetComponent<UiPlayerInfo>().player2d;
        }

    }

    public void ChooseTheHero()
    {
        ImageAlphaOff();
        //effectSmokeAnimator.gameObject.SetActive(false);        
        GameManager.instance.WaveCounter = 0;
        uiComicsController.ComicsCheck(_uIManager);       
    }

    public void OnClickPlay()
    {
        mainMenu.SetActive(false);
        GameManager.instance.SetHeroIndex(indexOfHero);
        GameManager.instance.Init();
        UIManager.instance.ShowPromptInGame();
    }

    private void ImageAlphaOff()
    {
        var color = effectSmokeAnimator.gameObject.GetComponent<Image>().color;
        color.a = 0f;
        effectSmokeAnimator.gameObject.GetComponent<Image>().color = color;
    }

    private void CreateIconsForMenu()
    {
        GameManager.instance.LoadData();
        playerPrefabs = GameManager.instance.PlayerPrefabs;
        _iconsBtns = new List<Button>();
        for (int i = 0; i < playerPrefabs.Length; i++)
        {
            var icon = Instantiate(iconPrefab, panelOfIcons);                        
            //int tmp = i;
            if (!playerPrefabs[i].GetComponent<UnlockCharacterComponent>().UnlockCharacter())
            {
                int tmp = i;
                icon.GetComponent<Button>().onClick.RemoveAllListeners();
                icon.GetComponent<Button>().onClick.AddListener(() => OnClickLockHero(tmp));

                var ss = icon.GetComponent<Button>().spriteState;
                icon.GetComponent<Button>().image.sprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().unlockIcon;
                //ss.disabledSprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().unlockIcon;
                ss.highlightedSprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().unlockIconGlow;
                ss.selectedSprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().unlockIconGlow;
                ss.pressedSprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().unlockIconGlow;
                icon.GetComponent<Button>().spriteState = ss;

            }
            else
            {
                int tmp = i;
                icon.GetComponent<Button>().onClick.RemoveAllListeners();
                icon.GetComponent<Button>().onClick.AddListener(() => OnClickIconHero(tmp));

                var ss = icon.GetComponent<Button>().spriteState;
                icon.GetComponent<Button>().image.sprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().icon;
                ss.disabledSprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().unlockIcon;
                ss.highlightedSprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().glowIcon;
                ss.selectedSprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().glowIcon;
                ss.pressedSprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().glowIcon;
                icon.GetComponent<Button>().spriteState = ss;
               // SetHeroLvl(playerPrefabs[i]);
               // UpdateCharacteristics(playerPrefabs[i]);
            }
            
            icon.GetComponent<Button>().onClick.AddListener(() => PlaySoundCharacterSelect());
            //icon.GetComponent<Image>().sprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().icon;
            // ������ ������� ���������
           
            _iconsBtns.Add(icon.GetComponent<Button>());
            charatersIcons.Add(icon);
        }
    }

    public void SelectedIcon()
    {
        DestroyAndCreateNewIcons();
        _iconsBtns[indexOfHero].Select();
        DisplayParametersAccountBtn();
        SetHeroLvl(_player);
        UpdateCharacteristics(_player);
        
    }

    private void DestroyAndCreateNewIcons()
    {
        for (int i = 0; i < charatersIcons.Count; i++)
        {
            Destroy(charatersIcons[i]);
        }
        CreateIconsForMenu();
        _iconsBtns[indexOfHero].Select();
    }

    private void PlaySoundCharacterSelect()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("SelectCharacterIcon");
        }
    }

    public void PlaySoundOfButtonPress()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("ClickElement");
        }
    }

    private void SetHeroLvl(GameObject player)
    {
        if (_saveController)
        {
            heroLvl.text = _saveController.GetCharacterLvl(player.gameObject.name).ToString();
        }        
    }

    private void UpdateCharacteristics(GameObject player)
    {
        if (player)
        {
            player.GetComponent<PlayerCharacteristics>().Init();
            player.GetComponent<CharacterLevel>().UpgradeCharacteristics(player.GetComponent<PlayerCharacteristics>(), _saveController.GetCharacterLvl(player.gameObject.name));
            characteristicsUI.UpdateCharacterisctics(player.GetComponent<PlayerCharacteristics>());
            characteristicsUI.RemoveCharacteristicsHighlighting();
            CharacteristicBuff[] baffs = player.GetComponent<CharacterLevel>().Baffs;
            if (baffs != null)
            {
                foreach (CharacteristicBuff baff in baffs)
                {
                    characteristicsUI.HighlightUpgradedCharacteristics(baff.characteristic, Color.green);
                }
            }
        }
        
    }

    public void OnClickCharacterProgressMenu()
    {
        ImageAlphaOff();
        mainMenu.SetActive(false);
        characterProgressMenu.SetActive(true);
        characterProgressMenu.GetComponent<UICharacterProgressMenu>().Init();
        characterProgressMenu.GetComponent<UICharacterProgressMenu>().UpgradeUIParameters(_player);
    }

    public void DisplayParametersAccountBtn()
    {
        accountBtn.Init(GameManager.instance.AccountLevel);
    }

    public void OnClickAccountProgressMenu()
    {
        ImageAlphaOff();
        accountProgressMenu.SetActive(true);
        accountProgressMenu.GetComponent<UIAccountProgressMenu>().Init();
    }
}
    

