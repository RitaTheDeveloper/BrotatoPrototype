using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCritChanceProbability : Ability
{
    [SerializeField] private float critChancePercentage = 3;

    public override void UseAbility()
    {
        GameObject player = GameManager.instance.player;
        var currentCritChancePercentage = player.GetComponent<PlayerCharacteristics>().CurrentCritChancePercentage;
        player.GetComponent<PlayerCharacteristics>().CurrentCritChancePercentage = currentCritChancePercentage + critChancePercentage;
        UIManager.instance.OkOnClick();
    }

    public override string GetDescription()
    {
        return "+ " + critChancePercentage + "% к крит шансу";
    }
}
