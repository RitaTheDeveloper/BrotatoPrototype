using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMagnetDistance : Ability
{
    [SerializeField] private float _magnetDistance;

    public override void UseAbility()
    {
        GameObject player = GameManager.instance.player;
        var currentDistance = player.GetComponent<PlayerCharacteristics>().CurrentMagnetDistance;
        player.GetComponent<PlayerCharacteristics>().CurrentMagnetDistance = currentDistance + _magnetDistance;
        player.GetComponent<LevelSystem>().SetMagnetDistance();
        UIManager.instance.OkOnClick();
    }

    public override string GetDescription()
    {
        return "+ " + _magnetDistance + " к радиусу сбора ресурсов";
    }
}
