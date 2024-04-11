using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldLoot : Loot
{
    protected override void AddLoot(GameObject other)
    {
        other.GetComponent<PlayerInventory>().ChangeMoney(amountOfLoot);
    }
}
