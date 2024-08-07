using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCharacterComponent : MonoBehaviour
{
    protected GameManager gameManager;

    public virtual void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }


    public virtual bool UnlockCharacter()
    {
        return true;
    }
    
}
