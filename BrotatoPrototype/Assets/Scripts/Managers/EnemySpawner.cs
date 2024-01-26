using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Префаб юнита")]
    [SerializeField] private GameObject _enemyPrefab;

    [Header("Минимальное и максимальное количество юнитов")]
    [SerializeField] private int amountOfEnemies;

    [Header("время перед самым первым спавном")]
    [SerializeField] private float _startSpawnTime;

    [Header("Минимальное время и максимальное время спавна")]
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;

    [Header("Радиус спавна от игрока")]
    [SerializeField] private float _radiusFromPlayer = 5f;

    [Header("Если спавнится за раз больше одного юнита, укажите радиус этой кучки врагов")]
    [SerializeField] private float radius = 0f;

    [SerializeField] private GameObject markPrefab;
    private float markDisplayTime = 1f;

    private Transform container;
    private Vector3 randomPosition;
    private bool isBeginningOfWave;
    private Transform _target;
    private Transform _plane;
    float _timeUntilSpawn;

    private void Awake()
    {        
        container = GameObject.Find("Enemies").transform;
        isBeginningOfWave = true;
    }

    private void Start()
    {
        _target = GameManager.instance.player.transform;
        _plane = GameObject.Find("Plane").transform;
        randomPosition = RandomPositionRelativeToPlayer(_radiusFromPlayer);
        Spawn(randomPosition);
        StartCoroutine(ChangeRandomPos());
    }

    private void Update()
    {
        Vector3 point;
        if (RandomPoint(_target.position, _radiusFromPlayer, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            
        }
    }

    private float SpawnTime()
    {
        if (isBeginningOfWave)
        {
            return Random.Range(_startSpawnTime - 0.2f, _startSpawnTime + 0.2f);
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

    private IEnumerator SpawnOneEnemy()
    {
        while (_target)
        {
            _timeUntilSpawn = SpawnTime();
                       
            // делаем марку
            yield return new WaitForSeconds(_timeUntilSpawn - markDisplayTime);
            Vector3 positionEnemy;
            Vector3 point;
            if (RandomPoint(randomPosition, radius, out point))
            {
                positionEnemy = point;
            }
            else
            {
                Debug.Log("Не могу найти позицию 2");
                positionEnemy = randomPosition;
            }
                GameObject mark = CreateMark(positionEnemy);
            mark.transform.parent = transform;

            // спавним врага
            yield return new WaitForSeconds(markDisplayTime);
            isBeginningOfWave = false;
            DestroyMark(mark);
            SpawnEnemy(positionEnemy);
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
            StartCoroutine(SpawnOneEnemy());
        }
    }    

    private IEnumerator ChangeRandomPos()
    {
        while (_target)
        {
            yield return new WaitForSeconds(markDisplayTime);
            randomPosition = RandomPositionRelativeToPlayer(_radiusFromPlayer);
        }
    }

    private Vector3 RandomPositionRelativeToPlayer(float radius)
    {

        Vector3 point;
        Vector3 position;
        if (RandomPoint(_target.position, radius, out point))
        {
            position = point;
        }
        else
        {
            Debug.Log("Не могу найти позицию 1");
            position = _target.position;
        }
           
        return position;
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
