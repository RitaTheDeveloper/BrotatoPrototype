using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveUnlockComponent : UnlockCharacterComponent
{
    [Tooltip("The number of waves required to unlock this character")]
    [SerializeField] private int _wavesRequired;
    [SerializeField] private int _accountLevelReguired;


    public override bool UnlockCharacter()
    {
        if (GameManager.instance == null)
            return false;
        if (_wavesRequired <= GameManager.instance.GetComponent<SaveController>().GetData().WaveEnded)
        {
            return true;
        }
        return false;

    }

    public int GetCountWaveRequired()
    {
        return _wavesRequired;
    }
}
