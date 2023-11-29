using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private enum SpawnType { Single, Group};

    [Header("Префаб юнита")]
    [SerializeField] private GameObject _enemyPrefab;

    [Header("Тип спавна")]
    [SerializeField] private SpawnType spawnType;

    [Header("Минимальное время и максимальное время спавна")]
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;

    [Header("Если тип спавна - группа, то укажите радиус и кол-во юнитов")]
    [SerializeField] private float radius;
    [SerializeField] private int amountOfEnemies;


    private float _timeUntilSpawn;

    private void Awake()
    {
        SetTimeUntilSpawn();
    }

    private void FixedUpdate()
    {
        _timeUntilSpawn -= Time.deltaTime;
        if (_timeUntilSpawn <= 0)
        {
            Spawn(spawnType);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minSpawnTime, _maxSpawnTime);
    }

    private void Spawn(SpawnType spawnType)
    {
        if (spawnType == SpawnType.Single)
        {
            SpawnOneEnemy(transform.position);
        }
        else if(spawnType == SpawnType.Group)
        {
            SpawnInGroup();
        }
        
    }

    private void SpawnOneEnemy(Vector3 position)
    {
        Instantiate(_enemyPrefab, position, transform.rotation);
    }

    private void SpawnInGroup()
    {
        Vector3 posRandomInCircle;
        Vector3 positionEnemy;
        for (int i = 0; i < amountOfEnemies; i++)
        {
            posRandomInCircle = Random.insideUnitCircle * radius;
            positionEnemy = new Vector3(posRandomInCircle.x + transform.position.x, transform.position.y, posRandomInCircle.y + transform.position.z);
            SpawnOneEnemy(positionEnemy);
        }
    }


}
