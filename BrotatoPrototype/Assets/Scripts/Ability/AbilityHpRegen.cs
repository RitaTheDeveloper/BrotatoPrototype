using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHpRegen : Ability
{
    [SerializeField] private float hpRegenPerSecond = 0.5f;

    public override void UseAbility()
    {
        GameObject player = GameManager.instance.player;
        var currentHpRegenPerSec = player.GetComponent<PlayerCharacteristics>().CurrentHpRegen;
        player.GetComponent<PlayerCharacteristics>().CurrentHpRegen = currentHpRegenPerSec + hpRegenPerSecond;
        player.GetComponent<PlayerHealth>().SetHpRegenPerSecond();
        UIManager.instance.OkOnClick();
    }

    public override string GetDescription()
    {
        return "+ " + hpRegenPerSecond + " хп к регенерации";
    }
}
