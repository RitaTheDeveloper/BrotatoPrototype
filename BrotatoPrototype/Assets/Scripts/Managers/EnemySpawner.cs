using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("������ �����")]
    [SerializeField] private EnemyController _enemyPrefab;

    [Header("��� �����")]
    [SerializeField] private TypeEnemy _typeEnemy;

    [Header("��� �����")]
    [SerializeField] private TierType _tierType;

    [Header("���������� ������")]
    [SerializeField] private int _amountOfEnemies = 1;

    [Header("����� ����� ����� ������ �������")]
    [SerializeField] private float _startSpawnTime = 1;

    [Header("����������� ����� � ������������ ����� ������")]
    [SerializeField] private float _minSpawnTime = 1;
    [SerializeField] private float _maxSpawnTime = 2;

    [Header("������ ������ �� ������")]
    [SerializeField] private float _radiusFromPlayer = 15f;

    [Header("�������, ���� ����� ������ ���������� �������, � Transform Position ��������� ����������")]
    [SerializeField] private bool _isNotRandom = false;

    [Header("���� ��������� �� ��� ������ ������ �����, ������� ������ ���� ����� ������")]
    [SerializeField] private float _radius = 2f;

    [SerializeField] private GameObject markPrefab;
    [SerializeField] private float markDisplayTime = 1f;

    private float _endSpawnTime; // ����� �� ����� �����, ����� ��������� ��������
    private Transform container;
    private Vector3 randomPosition;
    private bool isBeginningOfWave;
    private Transform _target;
    float _timeUntilSpawn;
    private WaveController _waveController;
    private int countOfEnemies = 0;
    private int totalAmountOfenemies = 0;
    private Vector3 _specificPoint;
    private bool _isSpawnerUnit = false;

    private ManagerEnemyTier _managerEnemyTier;

    private void Awake()
    {
        container = GameObject.Find("Enemies").transform;
        isBeginningOfWave = true;
    }

    private void Start()
    {
        _waveController = GameManager.instance.GetCurrentWave();
        if (_isSpawnerUnit == false)
        {
            _target = GameManager.instance.player.transform;           
        }
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
        //    Debug.Log("��������� ��������");
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

    public void InitSpawner(ManagerEnemyTier managerEnemyTier, int amount)
    {
        _managerEnemyTier = managerEnemyTier;
        totalAmountOfenemies = amount;
    }

    public void LoadPar(TypeEnemy typeEnemy, TierType tierType, float cdSpawn, int amountOfEnemiesInPack, int totalAmountOfunits)
    {
        _typeEnemy = typeEnemy;
        _tierType = tierType;
        _startSpawnTime = cdSpawn;
        _endSpawnTime = cdSpawn;
        _minSpawnTime = cdSpawn;
        _maxSpawnTime = cdSpawn;
        _amountOfEnemies = amountOfEnemiesInPack;
        _isSpawnerUnit = true;
        totalAmountOfenemies = totalAmountOfunits;
        _target = this.gameObject.transform;
    }

    public void SetParameters(EnemyController enemyController, float cdSpawn, float startSpawnTime, float endSpawnTime, int amountOfEnemiesInPack, float radiusOfPack, float radiusFromPlayer, bool isSpecificPoint, Vector2 specificPoint, TypeEnemy typeEnemy,  TierType tierType)
    {
        _enemyPrefab = enemyController;
        _typeEnemy = typeEnemy;
        _tierType = tierType;
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
        var enemyPosition = new Vector3(position.x, _enemyPrefab.transform.position.y, position.z);

        EnemyController enemy;

        if (_typeEnemy != TypeEnemy.NULL)
        {
            enemy = _managerEnemyTier.GetSpawnEnemy(_typeEnemy, _tierType, position, transform.rotation);
        }
        else
        {
            enemy = Instantiate(_enemyPrefab, enemyPosition, transform.rotation);
        }

        enemy.transform.parent = container;

        UnitParameters enemyParameters = enemy.GetComponent<UnitParameters>();
        if (_isSpawnerUnit)
        {
            enemyParameters.AmountOfGoldForKill = 0;
            enemyParameters.AmountOfExperience = 0;
        }
        else
        {
            enemyParameters.AmountOfGoldForKill = _waveController.distrubitionOfGoldToMobs.GetNumberOfGoldOrExp();
            enemyParameters.AmountOfExperience = _waveController.distrubitionOfExpToMobs.GetNumberOfGoldOrExp();
        }
        _waveController.counterOfMobs++;
        countOfEnemies++;
    }

    private IEnumerator SpawnOneEnemy()
    {
        while (countOfEnemies < totalAmountOfenemies && _waveController.CurrentTime >= _endSpawnTime + SpawnTime()+ Time.fixedDeltaTime && _target)
        {
            _timeUntilSpawn = SpawnTime()- Time.fixedDeltaTime;
            float timeMark = _timeUntilSpawn - markDisplayTime;
            float timeSpawnenemy = markDisplayTime;
            if(timeMark < 0)
            {
                timeMark = 0f;
                timeSpawnenemy = _timeUntilSpawn;
            }
            // ������ �����
            
            yield return new WaitForSeconds(timeMark);            
            Vector3 positionEnemy;
            Vector3 point;
            if (RandomPoint(randomPosition, _radius, out point))
            {
                positionEnemy = point;
            }
            else
            {
                positionEnemy = randomPosition;
            }
            GameObject mark = CreateMark(positionEnemy);
            if (mark)
            {
                mark.transform.parent = transform;
            }


            // ������� �����
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
