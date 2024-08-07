using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountLevelUnlockComponent : UnlockCharacterComponent
{
    [SerializeField] private int _accountLevelReguired = 1;

    public override bool UnlockCharacter()
    {
        if (GameManager.instance == null)
            return false;

        if (_accountLevelReguired <= GameManager.instance.GetComponent<SaveController>().GetData().CurrentAccountLevel)
        {
            return true;
        }
        return false;
    }
}
