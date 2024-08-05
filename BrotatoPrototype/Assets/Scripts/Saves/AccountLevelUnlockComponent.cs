using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountLevelUnlockComponent : UnlockCharacterComponent
{
    [SerializeField] private int _accountLevelReguired = 1;

    public override bool UnlockCharacter()
    {        
        if (_accountLevelReguired <= gameManager.AccountLevel.CurrentLvl)
        {
            return true;
        }
        return false;
    }
}
