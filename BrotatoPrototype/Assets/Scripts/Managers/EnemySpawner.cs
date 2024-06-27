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
    [SerializeField] private bool _isNotRandom = false;

    [Header("Если спавнится за раз больше одного юнита, укажите радиус этой кучки врагов")]
    [SerializeField] private float _radius = 2f;

    [SerializeField] private GameObject markPrefab;
    [SerializeField] private float markDisplayTime = 1f;

    private float _endSpawnTime; // время до конца волны, когда перестаем спавнить
    private Transform container;
    private Vector3 randomPosition;
    private bool isBeginningOfWave;
    private Transform _target;
    float _timeUntilSpawn;
    private WaveController _waveController;
    private int countOfEnemies = 0;
    private Vector3 _specificPoint;

    private void Awake()
    {
        container = GameObject.Find("Enemies").transform;
        isBeginningOfWave = true;
}

    private void Start()
    {
        _waveController = GameManager.instance.GetCurrentWave();
        _target = GameManager.instance.player.transform;
        if (_isNotRandom)
        {
            randomPosition = _specificPoint;
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
        //if(_waveController.CurrentTime <= _endSpawnTime + markDisplayTime)
        //{
        //    stopSpawn = true;
        //    Debug.Log("перестаем спавнить");
        //}
    }

    private void Update()
    {
        //Vector3 point;
        //if (RandomPoint(_target.position, _radiusFromPlayer, out point))
        //{
        //    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);

        //}
    }

    public void SetParameters(EnemyController enemyController, float cdSpawn, float startSpawnTime, float endSpawnTime, int amountOfEnemiesInPack, float radiusOfPack, float radiusFromPlayer, bool isSpecificPoint, Vector2 specificPoint )
    {
        _enemyPrefab = enemyController.gameObject;
        _startSpawnTime = startSpawnTime;
        _endSpawnTime = endSpawnTime;
        _minSpawnTime = cdSpawn;
        _maxSpawnTime = cdSpawn;
        _amountOfEnemies = amountOfEnemiesInPack;
        _isNotRandom = isSpecificPoint;
        _radiusFromPlayer = radiusFromPlayer;
        _radius = radiusOfPack;
        _specificPoint = new Vector3(specificPoint.x, 0, specificPoint.y);
        markPrefab = _enemyPrefab.GetComponent<UnitParameters>().GetMark();
        Debug.Log("cd = " + cdSpawn);
    }

    private float SpawnTime()
    {
        if (isBeginningOfWave)
        {
            return _startSpawnTime;
        }
        else
        {
            //return Random.Range(_minSpawnTime, _maxSpawnTime);
            return _minSpawnTime;
        }
    }

    private void SpawnEnemy(Vector3 position)
    {
        //PlaySoundOfSpawnEnemy();
        countOfEnemies++;
        var enemyPosition = new Vector3(position.x, _enemyPrefab.transform.position.y, position.z);
        var enemy = Instantiate(_enemyPrefab, enemyPosition, transform.rotation);
        enemy.transform.parent = container;

        UnitParameters enemyParameters = enemy.GetComponent<UnitParameters>();
        enemyParameters.AmountOfGoldForKill = _waveController.distrubitionOfGoldToMobs.GetNumberOfGoldOrExp();
        enemyParameters.AmountOfExperience = _waveController.distrubitionOfExpToMobs.GetNumberOfGoldOrExp();
        _waveController.counterOfMobs++;
        Debug.Log("заспавнен " + _waveController.counterOfMobs + "; время " + (_waveController.time - _waveController.GetCurrentTime()) + " из " + _waveController.GetAmountOfTotalEnemiesPerWave() + " v " + countOfEnemies) ;
    }

    private IEnumerator SpawnOneEnemy()
    {
        while (countOfEnemies < _waveController.GetAmountOfTotalEnemiesPerWave() && _waveController.CurrentTime >= _endSpawnTime + SpawnTime() && _target)
        {
            _timeUntilSpawn = SpawnTime()- Time.fixedDeltaTime;
            float timeMark = _timeUntilSpawn - markDisplayTime;
            float timeSpawnenemy = markDisplayTime;
            if(timeMark < 0)
            {
                timeMark = 0f;
                timeSpawnenemy = _timeUntilSpawn;
            }
            // делаем марку
            
            yield return new WaitForSeconds(timeMark);            
            Vector3 positionEnemy;
            Vector3 point;
            if (RandomPoint(randomPosition, _radius, out point))
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
            yield return new WaitForSeconds(timeSpawnenemy);
            
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
            if (_isNotRandom)
            {
                randomPosition = _specificPoint;
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
