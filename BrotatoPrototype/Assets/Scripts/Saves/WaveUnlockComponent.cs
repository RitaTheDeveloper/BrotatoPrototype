using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveUnlockComponent : UnlockCharacterComponent
{
    [Tooltip("The number of waves required to unlock this character")]
    [SerializeField] private int _wavesRequired;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
