using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionClass : MonoBehaviour 
{
    public virtual bool CanPlayNext()
    {
        return false;
    }
}
