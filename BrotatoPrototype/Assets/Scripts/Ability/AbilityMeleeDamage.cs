using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMeleeDamage : Ability
{
    public float meleeDamage = 2f;

    public override void UseAbility()
    {
        GameObject player = GameManager.instance.player;
        var currentMelleDamage = player.GetComponent<PlayerCharacteristics>().CurrentMeleeDamage;
        player.GetComponent<PlayerCharacteristics>().CurrentMeleeDamage = currentMelleDamage + meleeDamage;
        UIManager.instance.OkOnClick();
    }

    public override string GetDescription()
    {
        return "+ " + meleeDamage + " к ближнему урону";
    }
}
