using NTC.MonoCache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoCache
{
    [Header("Префаб юнита")]
    [SerializeField] private GameObject _enemyPrefab;

    [Header("количество юнитов")]
    [SerializeField] private int amountOfEnemies;

    [Header("время перед спавном")]
    [SerializeField] private float _spawnTime;

    [Header("Если спавнится за раз больше одного юнита, укажите радиус этой кучки врагов")]
    [SerializeField] private float radius = 0f;

    [SerializeField] private GameObject markPrefab;

    private LivingEntity livingEntity;
    private float _markDisplayTime = 1f;
    private Transform _container;

    private void Start()
    {
        livingEntity = GetComponent<LivingEntity>();
        _container = GameObject.Find("Enemies").transform;
    }
    protected override void FixedRun()
    {
        if (livingEntity.dead)
        {
            Debug.Log("сдох");
            Spawn();
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            StartCoroutine(SpawnOneEnemy());
        }
    }

    private IEnumerator SpawnOneEnemy()
    {
        Vector2 randomPos = Random.insideUnitCircle * radius;
        Vector3 position = new Vector3(transform.position.x + randomPos.x, 0, randomPos.y + transform.position.z);
        var mark = CreateMark(position);

        yield return new WaitForSeconds(_markDisplayTime);
        var enemy = Instantiate(_enemyPrefab, randomPos, Quaternion.identity);
        enemy.transform.parent = _container.parent;
        Destroy(mark);

    }

    private GameObject CreateMark(Vector3 position)
    {
        return Instantiate(markPrefab, position, markPrefab.transform.rotation);
    }

    private void DestroyMark(GameObject mark)
    {
        Destroy(mark);
    }
}
