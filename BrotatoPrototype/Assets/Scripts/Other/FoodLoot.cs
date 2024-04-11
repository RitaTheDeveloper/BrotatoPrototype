using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodLoot : Loot
{
    protected override void AddLoot(GameObject other)
    {
        other.GetComponent<PlayerSatiety>().ChangeSatiety(amountOfLoot);
    }
}
