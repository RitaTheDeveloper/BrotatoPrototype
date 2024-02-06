using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Префаб юнита")]
    [SerializeField] private GameObject _enemyPrefab;

    [Header("количество юнитов")]
    [SerializeField] private int amountOfEnemies;

    [Header("время перед спавном")]
    [SerializeField] private float _spawnTime;

    [Header("Если спавнится за раз больше одного юнита, укажите радиус этой кучки врагов")]
    [SerializeField] private float radius = 0f;

    [SerializeField] private GameObject markPrefab;


}
