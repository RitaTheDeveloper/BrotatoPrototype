using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMelleeDamage : Ability
{
    public float melleeDamage = 2f;

    public override void UseAbility()
    {
        GameObject player = GameManager.instance.player;
        var currentMelleDamage = player.GetComponent<PlayerCharacteristics>().CurrentMelleeDamage;
        player.GetComponent<PlayerCharacteristics>().CurrentMelleeDamage = currentMelleDamage + melleeDamage;
        UIManager.instance.OkOnClick();
    }

    public override string GetDescription()
    {
        return "+ " + melleeDamage + " к ближнему урону";
    }
}
