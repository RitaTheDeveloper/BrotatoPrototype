using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Префаб юнита")]
    [SerializeField] private GameObject _enemyPrefab;

    [Header("Количество юнитов")]
    [SerializeField] private int amountOfEnemies;

    [Header("время перед самым первым спавном")]
    [SerializeField] private float _startSpawnTime;

    [Header("Минимальное время и максимальное время спавна")]
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;

    [Header("Если спавнится за раз больше одного юнита, укажите радиус")]
    [SerializeField] private float radius = 0f;

    [SerializeField] private GameObject markPrefab;
    private float markDisplayTime = 1f;

    private Transform container;
    //private float _timeUntilSpawn;
    //private Vector3 randomPosition;
    private bool isBeginningOfWave;
    private Transform _target;

    private void Awake()
    {        
        container = GameObject.Find("Enemies").transform;
        isBeginningOfWave = true;
    }

    private void Start()
    {
        _target = GameManager.instance.player.transform;
        Spawn(RandomPosition());
    }  

    private float SpawnTime()
    {
        if (isBeginningOfWave)
        {
            return _startSpawnTime;
        }
        else
        {

            return Random.Range(_minSpawnTime, _maxSpawnTime);
        }

    }

    private void SpawnEnemy(Vector3 position)
    {
        var enemy =  Instantiate(_enemyPrefab, position, transform.rotation);
        enemy.transform.parent = container;
    }

    private IEnumerator SpawnOneEnemy(Vector3 position)
    {
        while (_target)
        {
            
            float time = SpawnTime();
            yield return new WaitForSeconds(time - markDisplayTime);
            GameObject mark = CreateMark(position);
            mark.transform.parent = transform;

            yield return new WaitForSeconds(markDisplayTime);
            isBeginningOfWave = false;
            DestroyMark(mark);
            SpawnEnemy(position);
        }
    }

    private GameObject CreateMark(Vector3 position)
    {
        return Instantiate(markPrefab, position, markPrefab.transform.rotation);
    }

    private void DestroyMark(GameObject mark)
    {
        Destroy(mark);
    }

    private void Spawn(Vector3 position)
    {
        for (int i= 0; i < amountOfEnemies; i++)
        {
            Vector3 posRandomInCircle;
            Vector3 positionEnemy;

            posRandomInCircle = Random.insideUnitCircle * radius;
            positionEnemy = new Vector3(posRandomInCircle.x + position.x, position.y, posRandomInCircle.y + position.z);

            StartCoroutine(SpawnOneEnemy(positionEnemy));
        }
    }

    //private IEnumerator SpawnInGroup(Vector3 position)
    //{
    //    Vector3 posRandomInCircle;
    //    Vector3 positionEnemy;
    //    //float time = SpawnTime();

    //    for (int i = 0; i < amountOfEnemies; i++)
    //    {
    //        posRandomInCircle = Random.insideUnitCircle * radius;
    //        positionEnemy = new Vector3(posRandomInCircle.x + position.x, position.y, posRandomInCircle.y + position.z);
    //        //SpawnOneEnemy(positionEnemy);
    //        StartCoroutine(SpawnOneEnemy(positionEnemy));
    //    }
    //    yield return null;
    //}

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
