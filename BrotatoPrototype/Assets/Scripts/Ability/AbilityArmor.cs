using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityArmor : Ability
{
    public float amountOfArmor;

    public override void UseAbility()
    {
        GameObject player = GameManager.instance.player;
        var currentArmor = player.GetComponent<PlayerCharacteristics>().CurrentArmor;
        player.GetComponent<PlayerCharacteristics>().CurrentArmor = currentArmor + amountOfArmor;
        player.GetComponent<PlayerHealth>().SetArmor();
        UIManager.instance.OkOnClick();
    }

    public override string GetDescription()
    {
        return "+ " + amountOfArmor + " к броне";
    }
}
