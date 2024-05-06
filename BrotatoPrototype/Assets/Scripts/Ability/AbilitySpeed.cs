using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpeed : Ability
{
    public float speedPercantage = 3;

    public override void UseAbility()
    {
        GameObject player = GameManager.instance.player;
        var currentSpeed = player.GetComponent<PlayerCharacteristics>().CurrentMoveSpeed;
        player.GetComponent<PlayerCharacteristics>().CurrentMoveSpeed = currentSpeed + speedPercantage;
        player.GetComponent<PlayerMovement>().SetSpeed();
        UIManager.instance.OkOnClick();
    }

    public override string GetDescription()
    {
        return "+ " + speedPercantage + "% к скорости";
    }
}
