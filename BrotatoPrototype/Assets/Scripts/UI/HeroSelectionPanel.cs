using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    private void Start()
    {
        playerPrefabs = GameManager.instance.PlayerPrefabs;
        //for (int i = 0; i < playerPrefabs.Length; i++)
        //{
        //    var icon = Instantiate(iconPrefab, panelOfIcons);
        //    icon.GetComponent<Button>().onClick.RemoveAllListeners();
        //    icon.GetComponent<Button>().onClick.AddListener(() => OnClickIconHero(i));
        //    icon.GetComponent<Image>().sprite = playerPrefabs[i].GetComponent<UiPlayerInfo>().icon;
        //}

        indexOfHero = 0;
        OnClickIconHero(indexOfHero);
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
        //OpenCloseWindow.CloseWindow(mainMenu);
        GameManager.instance.SetHeroIndex(indexOfHero);
        GameManager.instance.Init();
    }
}
