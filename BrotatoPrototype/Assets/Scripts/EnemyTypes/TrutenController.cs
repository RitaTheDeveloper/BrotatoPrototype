using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrutenController : IvanEnemyController
{
    [SerializeField] private float runAwayDistance = 3f;
    [SerializeField] private float spawnDistance = 8f;
    [SerializeField] private float hp = 8f;
    [SerializeField] private float hpIncreasePerWave = 1.0f;
    [SerializeField] private float speed = 200f;
    [SerializeField] private float dmg = 1f;
    [SerializeField] private float dmgIncreasePerWave = 0.6f;
    public GameObject projectilePrefab; // Префаб снаряда
    public Transform projectileSpawnPoint; // Точка спавна снаряда
    [SerializeField] private float projectileSpeed = 10f; // Скорость снаряда
    private bool isAttacking = false;

    protected override void Update()
    {
        base.Update();
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(dirToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        if (Vector3.Distance(target.position, transform.position) < runAwayDistance)
        {
            Vector3 newPos = transform.position - dirToTarget*runAwayDistance;
            transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * speed);
        }
        else if (!isAttacking)
        {
            StartCoroutine(Attack());
        }
    }




    protected override IEnumerator Attack()
    {
        isAttacking = true;
        while (Vector3.Distance(target.position, transform.position) >= runAwayDistance)
        {
            currentState = State.Attacking;
            navMeshAgent.enabled = false;

            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * projectileSpeed;

            yield return new WaitForSeconds(3f);

            currentState = State.Chasing;
            navMeshAgent.enabled = true;
        }
        isAttacking = false;
    }

    public void Spawn()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnDistance;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, spawnDistance, 1);
        transform.position = hit.position;
    }

    public void IncreaseStats()
    {
        hp += hpIncreasePerWave;
        dmg += dmgIncreasePerWave;
    }
}

