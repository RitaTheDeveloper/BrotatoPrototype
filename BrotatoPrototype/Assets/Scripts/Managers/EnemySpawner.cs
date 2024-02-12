using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Префаб юнита")]
    [SerializeField] private GameObject _enemyPrefab;

    [Header("количество юнитов")]
    [SerializeField] private int amountOfEnemies;

    [Header("время перед самым первым спавном")]
    [SerializeField] private float _startSpawnTime;

    [Header("Минимальное время и максимальное время спавна")]
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;

    [Header("Радиус спавна от игрока")]
    [SerializeField] private float _radiusFromPlayer = 15f;

    [Header("Галочка, если нужно задать конкретную позицию, в Transform Position выставите координаты")]
    [SerializeField] private bool isNotRandom = false;

    [Header("Если спавнится за раз больше одного юнита, укажите радиус этой кучки врагов")]
    [SerializeField] private float radius = 0f;

    [SerializeField] private GameObject markPrefab;
    [SerializeField] private float markDisplayTime = 1f;

    private Transform container;
    private Vector3 randomPosition;
    private bool isBeginningOfWave;
    private Transform _target;
    float _timeUntilSpawn;

    private void Awake()
    {
        container = GameObject.Find("Enemies").transform;
        isBeginningOfWave = true;
    }

    private void Start()
    {
        _target = GameManager.instance.player.transform;
        if (isNotRandom)
        {
            randomPosition = transform.position;
        }
        else
        {
            randomPosition = RandomPositionInCircle(_radiusFromPlayer, _target.position);
        }
        //randomPosition = RandomPositionOutCircle(25f, _target.position);
        StartCoroutine(ChangeRandomPos());
        Spawn(randomPosition);
       // StartCoroutine(ChangeRandomPos());
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
        var enemyPosition = new Vector3(position.x, _enemyPrefab.transform.position.y, position.z);
        var enemy = Instantiate(_enemyPrefab, enemyPosition, transform.rotation);
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
        Vector3 markPosition = new Vector3(position.x, markPrefab.transform.position.y, position.z);
        return Instantiate(markPrefab, position, markPrefab.transform.rotation);
    }

    private void DestroyMark(GameObject mark)
    {
        Destroy(mark);
    }

    private void Spawn(Vector3 position)
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            StartCoroutine(SpawnOneEnemy());
        }
    }

    private IEnumerator ChangeRandomPos()
    {
        while (_target)
        {
            yield return new WaitForSeconds(markDisplayTime);
            if (isNotRandom)
            {
                randomPosition = transform.position;
            }
            else
            {
                randomPosition = RandomPositionInCircle(_radiusFromPlayer, _target.position);
            }
            
            //randomPosition = RandomPositionOutCircle(25f, _target.position);
        }
    }

    private Vector3 RandomPositionInCircle(float radius, Vector3 target)
    {

        Vector3 point;
        Vector3 position;
        if (RandomPoint(target, radius, out point))
        {
            position = point;
        }
        else
        {
            Debug.Log("Не могу найти позицию 1");
            position = Vector3.zero;
        }

        return position;
    }

    private Vector3 RandomPositionOutCircle(float radius, Vector3 center)
    {
        float ang = Random.value * 360;
        Vector3 position;
        position.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        position.y = center.y;
        position.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 1.0f, NavMesh.AllAreas))
        {
            position = hit.position;
            
        }
       
        Debug.DrawRay(position, Vector3.up, Color.red, 1.0f);
        return position;
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 35; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 3f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
