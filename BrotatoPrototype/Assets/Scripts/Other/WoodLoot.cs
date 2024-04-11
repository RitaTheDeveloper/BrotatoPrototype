using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLoot : MonoBehaviour
{
    private int _amountOfWood;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerInventory>().ChangeWood(_amountOfWood);
            Destroy(gameObject);
        }
    }

    public void SetAmountOfWood(int amount)
    {
        _amountOfWood = amount;
    }
}
