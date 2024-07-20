using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HeroSelectionPanel : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Image currentImgHero;
    [SerializeField] private TextMeshProUGUI nameHeroTxt;
    [SerializeField] private CharacteristicsUI characteristicsUI;
    [SerializeField] private Animator effectSmokeAnimator;
    [SerializeField] private TextMeshProUGUI heroDescription;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform panelOfIcons;
    [SerializeField] private GameObject blockInfo;
    [SerializeField] private TextMeshProUGUI blockTextInfo;
    [SerializeField] private TextMeshProUGUI heroLvl;
    [SerializeField] private Button choseBtn;
    public GameObject[] playerPrefabs;
    private int indexOfHero = 0;
    private List<Button> _iconsBtns;
    private List<GameObject> charatersIcons = new List<GameObject>();
    private GameObject _player;
    private UiPlayerInfo _uiPlayerInfo;
    private SaveController _saveController;
    private void Awake()
    {
        //CreateIconsForMenu();
        indexOfHero = 0;
    }
    private void Start()
    {
        _saveController = GameManager.instance.GetComponent<SaveController>();
        currentImgHero.sprite = playerPrefabs[indexOfHero].GetComponent<UiPlayerInfo>().player2d;
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
        SetHeroLvl();     
        heroDescription.text = _uiPlayerInfo.description;
        _player.GetComponent<PlayerCharacteristics>().Init();
        characteristicsUI.UpdateCharacterisctics(_player.GetComponent<PlayerCharacteristics>());
        if (index != indexOfHero)
        {            
            ImageAlphaOff();
            effectSmokeAnimator.SetTrigger("change");
            //currentImgHero.sprite = player.GetComponent<UiPlayerInfo>().player2d;
            StartCoroutine(ChangeSprite(_player));
           
        }
        _player.GetComponent<PlayerCharacteristics>().Init();
        characteristicsUI.UpdateCharacterisctics(_player.GetComponent<PlayerCharacteristics>());
        indexOfHero = index;
        choseBtn.interactable = true;
    }

    public void OnClickLockHero(int index)
    {
        _player = playerPrefabs[index];
        _uiPlayerInfo = _player.GetComponent<UiPlayerInfo>();
        nameHeroTxt.text = _uiPlayerInfo.nameHero;
        SetHeroLvl();
        if (index != indexOfHero)
        {
            ImageAlphaOff();
            effectSmokeAnimator.SetTrigger("change");
            StartCoroutine(ChangeSprite(_player));
        }
        heroDescription.text = _uiPlayerInfo.description;
        indexOfHero = index;
        blockInfo.SetActive(true);
        blockTextInfo.text = "Доступ к персонажу откроется при прохождении " + "\n" + _player.GetComponent<WaveUnlockComponent>().GetCountWaveRequired() + " волн";
        choseBtn.interactable = false;
    }

    private IEnumerator ChangeSprite(GameObject player)
    {
        yield return new WaitForSeconds(0.4f);
        currentImgHero.sprite = player.GetComponent<UiPlayerInfo>().player2d;
    }

    public void ChooseTheHero()
    {
        ImageAlphaOff();
        //effectSmokeAnimator.gameObject.SetActive(false);
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

    private void SetHeroLvl()
    {        
        heroLvl.text = _saveController.GetCharacterLvl(_player.gameObject.name).ToString();
        Debug.Log(_saveController.GetCharacterLvl(_player.gameObject.name).ToString());
    }
}
    

