using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UISegmentAccount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberOfLevelsTxt;
    [SerializeField] private Image characterImg;

    UICharacterInfoForAccount _uICharacterInfoForAccount;
    private bool _unlocked;

    public void Init(UICharacterInfoForAccount uICharacterInfoForAccount, int level, bool unlocked)
    {        
        _numberOfLevelsTxt.text = level.ToString();
        _unlocked = unlocked;
        _uICharacterInfoForAccount = uICharacterInfoForAccount;
        if (_unlocked)
        {
            characterImg.sprite = _uICharacterInfoForAccount.characterIcon;
        }
        else
        {
            characterImg.sprite = _uICharacterInfoForAccount.lockedIcon;
        }
        
    } 
}
