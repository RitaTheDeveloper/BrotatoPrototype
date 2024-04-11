using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    protected int amountOfLoot;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AddLoot(other.gameObject);
            Destroy(gameObject);
        }
    }

    protected virtual void AddLoot(GameObject other)
    {
        
    }

    public void SetAmountOfLoot(int amount)
    {
        amountOfLoot = amount;
    }
}
