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
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform panelOfIcons;
    public GameObject[] playerPrefabs;
    private int indexOfHero = 0;
    private List<Button> _iconsBtns;

    private void Start()
    {
        CreateIconsForMenu();

        indexOfHero = 0;
        OnClickIconHero(indexOfHero);
        _iconsBtns[0].Select();
    }

    public void OnClickIconHero(int index)
    {
        indexOfHero = index;
        var player = playerPrefabs[index];
        nameHeroTxt.text = player.GetComponent<UiPlayerInfo>().nameHero;
        currentImgHero.sprite = player.GetComponent<UiPlayerInfo>().player2d;
        player.GetComponent<PlayerCharacteristics>().Init();
        characteristicsUI.UpdateCharacterisctics(player.GetComponent<PlayerCharacteristics>());
    }

    public void ChooseTheHero()
    {
        mainMenu.SetActive(false);
        GameManager.instance.SetHeroIndex(indexOfHero);
        GameManager.instance.Init();
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
            // меняем спрайты состояний
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
}
