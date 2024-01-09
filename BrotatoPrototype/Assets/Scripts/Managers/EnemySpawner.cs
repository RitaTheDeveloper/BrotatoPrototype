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

    [SerializeField] private GameObject mark;
    private float markDisplayTime = 1f;

    private Transform container;
    private float _timeUntilSpawn;

    private void Awake()
    {
        SetTimeUntilSpawn();
        MarkOff();
        container = GameObject.Find("Enemies").transform;
    }

    private void FixedUpdate()
    {
        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= markDisplayTime)
        {
            MarkOn();
        }

        if (_timeUntilSpawn <= 0)
        {
            MarkOff();
            Spawn(spawnType);
            SetTimeUntilSpawn();
        }
    }

    private void MarkOn()
    {
        mark.SetActive(true);
    }

    private void MarkOff()
    {
        mark.SetActive(false);
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
        var enemyPosition = new Vector3(position.x, _enemyPrefab.transform.position.y, position.z);
        var enemy =  Instantiate(_enemyPrefab, enemyPosition, transform.rotation);
        enemy.transform.parent = container;
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
