using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IvanEnemyController : MonoBehaviour
{
    public enum State { Idle, Chasing, Attacking };
    protected State currentState;

    [SerializeField] protected float attackDistance = 1.5f;
    [SerializeField] protected float timeBetweenAttacks = 1f;

    protected NavMeshAgent navMeshAgent;
    protected Transform target;
    protected LivingEntity livingEntity;

    private float nextAttackTime;
    private float myCollisionRadius;
    private float targetCollisionRadius;

    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void Start()
    {
        myCollisionRadius = navMeshAgent.stoppingDistance;
        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdatePath());
    }

    protected virtual void Update()
    {
        if (Time.time > nextAttackTime)
        {
            if (Vector3.Distance(target.position, transform.position) < attackDistance)
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }
    }

    private void Init()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        livingEntity = GetComponent<LivingEntity>();
    }

    protected virtual IEnumerator Attack()
    {
        currentState = State.Attacking;
        navMeshAgent.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);

        float attackSpeed = 2f;
        float percent = 0f;

        while (percent <= 1)
        {
            percent += attackSpeed * Time.deltaTime;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }

        currentState = State.Chasing;
        navMeshAgent.enabled = true;
    }

    protected virtual IEnumerator UpdatePath()
    {
        float refreshRate = 1f;

        while (target != null)
        {
            if (currentState == State.Chasing)
            {
                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);
                if (!livingEntity.dead)
                {
                    navMeshAgent.SetDestination(targetPosition);
                }
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}

