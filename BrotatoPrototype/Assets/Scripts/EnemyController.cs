using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum State { Idle, Chasing, Attacking};
    State currentState;

    [SerializeField] private float attackDistance = 1.5f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private NavMeshAgent navMeshAgent;
    private Transform target;
    private LivingEntity livingEntity;

    private float nextAttackTime;
    private float myCollisionRadius;
    private float targetCollisionRadius;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        myCollisionRadius = navMeshAgent.stoppingDistance;
        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //_targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        StartCoroutine(UpdatePath());
    }

    private void Update()
    {

        if (Time.time > nextAttackTime && target)
        {

            if (Vector3.Distance(target.position, transform.position) < attackDistance)
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                //StartCoroutine(Attack());
                target.GetComponent<LivingEntity>().TakeHit(1f);
            }
        }
    }

    private void Init()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        livingEntity = GetComponent<LivingEntity>();
    }

    IEnumerator Attack()
    {        
        currentState = State.Attacking;
        navMeshAgent.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);
        //Vector3 attackPosition = new Vector3(target.position.x, transform.position.y, target.position.z);

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

    IEnumerator UpdatePath()
    {
        float refreshRate = 1f;

        while(target != null)
        {
            if(currentState == State.Chasing)
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
