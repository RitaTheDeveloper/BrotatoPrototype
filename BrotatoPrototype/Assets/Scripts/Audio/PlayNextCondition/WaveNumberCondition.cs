using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveNumberCondition : ConditionClass
{
    [Tooltip("Номер волны для завершения трека")]
    [SerializeField] public int WaveNumber;

    public override bool CanPlayNext()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.WaveCounter + 1 >= WaveNumber)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
