using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAttackSpeed : Ability
{
    [SerializeField] private float attackSpeedPercentage = 5;

    public override void UseAbility()
    {
        GameObject player = GameManager.instance.player;
        var currentAttackSpeedPercentage = player.GetComponent<PlayerCharacteristics>().CurrentAttackSpeedPercentage;
        player.GetComponent<PlayerCharacteristics>().CurrentAttackSpeedPercentage = currentAttackSpeedPercentage + attackSpeedPercentage;
        UIManager.instance.OkOnClick();
    }

    public override string GetDescription()
    {
        return "+ " + attackSpeedPercentage + "% к скорости атаки";
    }    
}
