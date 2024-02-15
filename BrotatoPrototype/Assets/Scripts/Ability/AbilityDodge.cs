using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDodge : Ability
{
    [SerializeField] private float probabilityOfDodge;

    public override void UseAbility()
    {
        GameObject player = GameManager.instance.player;
        var currentProbDodge = player.GetComponent<PlayerCharacteristics>().CurrentProbabilityOfDodge;
        player.GetComponent<PlayerCharacteristics>().CurrentProbabilityOfDodge = currentProbDodge + probabilityOfDodge;
        player.GetComponent<PlayerHealth>().SetProbabilityOfDodge();
        UIManager.instance.OkOnClick();
    }

    public override string GetDescription()
    {
        return "+ " + probabilityOfDodge + "% к уклонению";
    }
}
