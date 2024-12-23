using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] private Loot _loot;
    [SerializeField] private int amountOfLoot;

    public void SpawnLoot()
    {
        float randomDelay = Random.Range(-3f, 3f);
        Vector3 position = new Vector3(transform.position.x + randomDelay, _loot.transform.position.y, transform.position.z + randomDelay);
        var loot = Instantiate(_loot, position, Quaternion.identity);
        loot.transform.parent = GameObject.Find("Loot").transform;
        loot.SetAmountOfLoot(amountOfLoot);
    }
}
