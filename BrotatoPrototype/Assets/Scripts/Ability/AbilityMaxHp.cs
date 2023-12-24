using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMaxHp : Ability
{
    [SerializeField] private float amountOfHp = 3f;

    public override void UseAbility()
    {
        GameObject player = GameManager.instance.player;
        var maxHp = player.GetComponent<PlayerCharacteristics>().CurrentMaxHp;
        player.GetComponent<PlayerCharacteristics>().CurrentMaxHp = maxHp + amountOfHp;
        player.GetComponent<PlayerHealth>().SetMaxHP();
        UIManager.instance.OkOnClick();
    }

    public override string GetDescription()
    {
        return "+ " + amountOfHp + " к здоровью";
    }
}
