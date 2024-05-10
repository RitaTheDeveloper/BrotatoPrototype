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
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform panelOfIcons;
    public GameObject[] playerPrefabs;
    private int indexOfHero = 0;
    private List<Button> _iconsBtns;

    private void Awake()
    {
        CreateIconsForMenu();
        indexOfHero = 0;
    }
    private void Start()
    {       
        OnClickIconHero(indexOfHero);
        SelectedIcon();
    }

    public void OnClickIconHero(int index)
    {
        if(index != indexOfHero)
        {
            indexOfHero = index;
            var player = playerPrefabs[index];
            nameHeroTxt.text = player.GetComponent<UiPlayerInfo>().nameHero;
            var color = effectSmokeAnimator.gameObject.GetComponent<Image>().color;
            color.a = 0f;
            effectSmokeAnimator.gameObject.GetComponent<Image>().color = color;
            effectSmokeAnimator.SetTrigger("change");
            //currentImgHero.sprite = player.GetComponent<UiPlayerInfo>().player2d;
            StartCoroutine(ChangeSprite(player));
            player.GetComponent<PlayerCharacteristics>().Init();
            characteristicsUI.UpdateCharacterisctics(player.GetComponent<PlayerCharacteristics>());
        }        
    }

    private IEnumerator ChangeSprite(GameObject player)
    {
        yield return new WaitForSeconds(0.4f);
        currentImgHero.sprite = player.GetComponent<UiPlayerInfo>().player2d;
    }

    public void ChooseTheHero()
    {
        var color = effectSmokeAnimator.gameObject.GetComponent<Image>().color;
        color.a = 0f;
        effectSmokeAnimator.gameObject.GetComponent<Image>().color = color;
        //effectSmokeAnimator.gameObject.SetActive(false);
        mainMenu.SetActive(false);
        GameManager.instance.SetHeroIndex(indexOfHero);
        GameManager.instance.Init();
        UIManager.instance.ShowPromptInGame();
    }

    private void CreateIconsForMenu()
    {
        GameManager.instance.LoadData();
        playerPrefabs = GameManager.instance.PlayerPrefabs;
        _iconsBtns = new List<Button>();
        for (int i = 0; i < playerPrefabs.Length; i++)
        {
            var icon = Instantiate(iconPrefab, panelOfIcons);            
            icon.GetComponent<Button>().onClick.RemoveAllListeners();
            int tmp = i;
            icon.GetComponent<Button>().onClick.AddListener(() => OnClickIconHero(tmp));
            //icon.GetComponent<Image>().sprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().icon;
            // ������ ������� ���������
            var ss = icon.GetComponent<Button>().spriteState;
            icon.GetComponent<Button>().image.sprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().icon;
            ss.disabledSprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().unlockIcon;
            ss.highlightedSprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().glowIcon;
            ss.selectedSprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().glowIcon;
            ss.pressedSprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().glowIcon;
            icon.GetComponent<Button>().spriteState = ss;

            if (!playerPrefabs[i].GetComponent<UnlockCharacterComponent>().UnlockCharacter())
            {
                icon.GetComponent<Button>().interactable = false;
            }
            _iconsBtns.Add(icon.GetComponent<Button>());
        }
    }

    public void SelectedIcon()
    {
        _iconsBtns[indexOfHero].Select();
    }
}
