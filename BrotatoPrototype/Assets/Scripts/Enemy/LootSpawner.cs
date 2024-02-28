using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _lootList;

    public void SpawnLoot()
    {
        for(int i = 0; i < _lootList.Length; i++)
        {
            float randomDelay = Random.Range(-3f, 3f);
            Vector3 position = new Vector3(transform.position.x + randomDelay, _lootList[i].transform.position.y, transform.position.z + randomDelay);
            Instantiate(_lootList[i], position, Quaternion.identity);
        }
    }
}
