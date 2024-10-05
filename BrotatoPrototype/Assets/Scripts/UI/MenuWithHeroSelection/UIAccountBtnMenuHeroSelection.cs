using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIAccountBtnMenuHeroSelection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lvlTxt;
    [SerializeField] private Slider _unlockHeroSlider;
    [SerializeField] private TextMeshProUGUI numberOfLevelsBeforeUnlockHerTxt;
    [SerializeField] private Image iconUnlockHero;

    private AccountLevel _accountLevel;

    public void Init(AccountLevel accountLevel)
    {
        _accountLevel = accountLevel;

        _lvlTxt.text = _accountLevel.CurrentLvl.ToString();

        int currentLvl = _accountLevel.CurrentLvl;
        currentLvl = 4;
        int maxLvsForUnlockingHero = _accountLevel.AccountLevelSettings[currentLvl].numberOfCharacterLevels;
        int currentSumOflvls = _accountLevel.GetSumOfLvlsOfOpenCharacters();
        SetTxtForSlider(maxLvsForUnlockingHero, currentSumOflvls);
        
        iconUnlockHero.sprite = _accountLevel.AccountLevelSettings[currentLvl].unlockable—haracter.GetComponent<UICharacterInfoForAccount>().lockedIcon;                     
    }

    private void SetTxtForSlider(int max, int current)
    {
        numberOfLevelsBeforeUnlockHerTxt.text = current + "/" + max;
    }
}
