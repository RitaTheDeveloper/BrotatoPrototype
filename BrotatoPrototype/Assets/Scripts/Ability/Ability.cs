using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public string nameOfAbility;
    public Sprite icon;

    public virtual void UseAbility()
    {

    }

    public virtual string GetDescription()
    {
        return "";
    }

    public string GetName()
    {
        return nameOfAbility;
    }

    public Sprite GetSprite()
    {
        return icon;
    }
}
