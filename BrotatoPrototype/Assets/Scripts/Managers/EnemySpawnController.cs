using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
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

    [SerializeField] private GameObject mark;

    [SerializeField] private Transform _container;
    private float markDisplayTime = 1f;

    private bool isBeginningOfWave;
    private Transform _target;


    private void Start()
    {
        StartCoroutine(SpawnOneEnemy(transform.position));
    }

    private void SpawnEnemy(Vector3 position)
    {
        var enemy = Instantiate(_enemyPrefab, position, transform.rotation);
        enemy.transform.parent = _container;
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

    private IEnumerator SpawnOneEnemy(Vector3 position)
    {
        mark.SetActive(false);

        while (_target)
        {
            float time = SpawnTime();
            yield return new WaitForSeconds(time + markDisplayTime);
            mark.SetActive(true);
            Debug.Log("mark" + time + markDisplayTime);

            yield return new WaitForSeconds(markDisplayTime);
            isBeginningOfWave = false;
            mark.SetActive(false);
            SpawnEnemy(position);
        }
    }
}
