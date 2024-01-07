using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelectionPanel : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    public void ChooseTheHero(int index)
    {
        mainMenu.SetActive(false);
        GameManager.instance.SetHeroIndex(index);
        GameManager.instance.Init();
    }
}
