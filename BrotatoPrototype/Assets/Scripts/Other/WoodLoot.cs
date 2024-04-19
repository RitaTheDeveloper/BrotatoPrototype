using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLoot : Loot
{
    protected override void AddLoot(GameObject other)
    {
        other.GetComponent<PlayerInventory>().ChangeWood(amountOfLoot);
        other.GetComponent<PlayerInventory>().WoodUp();
    }
}
