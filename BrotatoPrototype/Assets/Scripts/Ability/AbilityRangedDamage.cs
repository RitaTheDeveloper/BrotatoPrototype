using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRangedDamage : Ability
{
    public float rangedDamage = 1;

    public override void UseAbility()
    {
        GameObject player = GameManager.instance.player;
        var currentRangedDamage = player.GetComponent<PlayerCharacteristics>().CurrentRangedDamage;
        player.GetComponent<PlayerCharacteristics>().CurrentRangedDamage = currentRangedDamage + rangedDamage;
        UIManager.instance.OkOnClick();
    }

    public override string GetDescription()
    {
        return "+ " + rangedDamage + " к дальнему урону";
    }
}
