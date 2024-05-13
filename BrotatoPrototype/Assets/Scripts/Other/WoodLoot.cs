using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLoot : Loot
{
    protected override void AddLoot(GameObject other)
    {        
        other.GetComponent<PlayerInventory>().WoodUp(amountOfLoot);
        PlaySoundTakeWood();
    }

    private void PlaySoundTakeWood()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("TakeWood");
        }
    }
}
