using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDamagePercentage : Ability
{
    [SerializeField] private float _damagePercentage;

    public override void UseAbility()
    {
        GameObject player = GameManager.instance.player;
        var currentDamagePercentage = player.GetComponent<PlayerCharacteristics>().CurrentDamagePercentage;
        player.GetComponent<PlayerCharacteristics>().CurrentDamagePercentage = currentDamagePercentage + _damagePercentage;
        UIManager.instance.OkOnClick();
    }

    public override string GetDescription()
    {
        return "+ " + _damagePercentage + "% к урону";
    }
}
