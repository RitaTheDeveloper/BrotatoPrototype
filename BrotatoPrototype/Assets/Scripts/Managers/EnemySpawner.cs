using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Префаб юнита")]
    [SerializeField] private GameObject _enemyPrefab;

    [Header("количество юнитов")]
    [SerializeField] private int _amountOfEnemies = 1;

    [Header("время перед самым первым спавном")]
    [SerializeField] private float _startSpawnTime = 1;

    [Header("Минимальное время и максимальное время спавна")]
    [SerializeField] private float _minSpawnTime = 1;
    [SerializeField] private float _maxSpawnTime = 2;

    [Header("Радиус спавна от игрока")]
    [SerializeField] private float _radiusFromPlayer = 15f;

    [Header("Галочка, если нужно задать конкретную позицию, в Transform Position выставите координаты")]
    [SerializeField] private bool isNotRandom = false;

    [Header("Если спавнится за раз больше одного юнита, укажите радиус этой кучки врагов")]
    [SerializeField] private float radius = 0f;

    [SerializeField] private GameObject markPrefab;
    [SerializeField] private float markDisplayTime = 1f;

    private float _endSpawnTime; // время до конца волны, когда перестаем спавнить
    private bool stopSpawn = false;
    private Transform container;
    private Vector3 randomPosition;
    private bool isBeginningOfWave;
    private Transform _target;
    float _timeUntilSpawn;

    private void Awake()
    {
        container = GameObject.Find("Enemies").transform;
        isBeginningOfWave = true;
        stopSpawn = false;
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

    private void FixedUpdate()
    {
        if(GameManager.instance.GetCurrentWave().GetCurrentTime() <= _endSpawnTime + markDisplayTime)
        {
            stopSpawn = true;
        }
    }

    private void Update()
    {
        //Vector3 point;
        //if (RandomPoint(_target.position, _radiusFromPlayer, out point))
        //{
        //    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);

        //}
    }

    public void SetParameters(EnemyController enemyController, float cdSpawn, float startSpawnTime, float endSpawnTime, int amountOfEnemiesInPack)
    {
        _enemyPrefab = enemyController.gameObject;
        _startSpawnTime = startSpawnTime;
        _endSpawnTime = endSpawnTime;
        _minSpawnTime = cdSpawn;
        _maxSpawnTime = cdSpawn;
        _amountOfEnemies = amountOfEnemiesInPack;
        Debug.Log("cd = " + cdSpawn);
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
        PlaySoundOfSpawnEnemy();
        var enemyPosition = new Vector3(position.x, _enemyPrefab.transform.position.y, position.z);
        var enemy = Instantiate(_enemyPrefab, enemyPosition, transform.rotation);
        enemy.transform.parent = container;
    }

    private IEnumerator SpawnOneEnemy()
    {
        while (!stopSpawn && _target)
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
            if (mark)
            {
                mark.transform.parent = transform;
            }
            

            // спавним врага
            yield return new WaitForSeconds(markDisplayTime);
            isBeginningOfWave = false;
            DestroyMark(mark);
            SpawnEnemy(positionEnemy);
        }
    }

    private GameObject CreateMark(Vector3 position)
    {
        PlaySoundOfMark();
        if (markPrefab)
        {
            Vector3 markPosition = new Vector3(position.x, markPrefab.transform.position.y, position.z);
            var mark = Instantiate(markPrefab, markPosition, markPrefab.transform.rotation);
            return mark;
        }
        else
        {
            return null;
        }
        
       // mark.transform.parent = container.transform;
        
    }

    private void PlaySoundOfMark()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("Xmark", this.gameObject.transform.position);
        }
    }

    private void PlaySoundOfSpawnEnemy()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("Spawn", this.gameObject.transform.position);
        }
    }

    private void DestroyMark(GameObject mark)
    {
        Destroy(mark);
    }

    private void Spawn(Vector3 position)
    {
        for (int i = 0; i < _amountOfEnemies; i++)
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
