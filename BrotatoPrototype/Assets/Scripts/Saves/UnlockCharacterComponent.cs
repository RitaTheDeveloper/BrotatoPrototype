using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCharacterComponent : MonoBehaviour
{
    protected GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }


    public virtual bool UnlockCharacter()
    {
        return true;
    }
}
