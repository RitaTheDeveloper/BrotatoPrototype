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

    [Header("Хаотично или в конкретном месте")]
    [SerializeField] private bool isRandom = true;

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
    private Vector3 randomPosition;

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
            Spawn(spawnType);
            MarkOff();
            SetTimeUntilSpawn();
        }
    }

    private void MarkOn()
    {
        mark.SetActive(true);
    }

    private void MarkOff()
    {
        if (isRandom)
        {
            randomPosition = RandomPosition();
            mark.transform.position = randomPosition;
        }
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
            if (isRandom)
            {
                SpawnOneEnemy(randomPosition);
            }
            else
            {
                SpawnOneEnemy(transform.position);
            }
        }
        else if(spawnType == SpawnType.Group)
        {
            if (isRandom)
            {
                SpawnInGroup(randomPosition);
            }
            else
            {
                SpawnInGroup(transform.position);
            }
        }       
    }

    private void SpawnOneEnemy(Vector3 position)
    {
        var enemy =  Instantiate(_enemyPrefab, position, transform.rotation);
        enemy.transform.parent = container;
    }

    private void SpawnInGroup(Vector3 position)
    {
        Vector3 posRandomInCircle;
        Vector3 positionEnemy;
        for (int i = 0; i < amountOfEnemies; i++)
        {
            posRandomInCircle = Random.insideUnitCircle * radius;
            positionEnemy = new Vector3(posRandomInCircle.x + position.x, position.y, posRandomInCircle.y + position.z);
            SpawnOneEnemy(positionEnemy);
        }
    }

    private Vector3 RandomPosition()
    {
        float boundary = 18f;
        float x;
        float z;
        float y;
        x = Random.Range(-boundary, boundary);
        z = Random.Range(-boundary, boundary);
        y = _enemyPrefab.transform.position.y;

        Vector3 randomPos = new Vector3(x,y,z);

        return randomPos;
    }
}
