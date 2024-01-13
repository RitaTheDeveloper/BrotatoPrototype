using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum State { Idle, Chasing, Attacking, RunAway};
    protected State currentState;

    [SerializeField] protected float attackDistance = 1.5f;
    [SerializeField] protected float timeBetweenAttacks = 0f;
    [SerializeField] protected float refreshRateOfUpdatePath = 1f;
    [SerializeField] protected Animator animator;

    protected NavMeshAgent navMeshAgent;
    public Transform target;
    protected LivingEntity livingEntity;

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

    private void FixedUpdate()
    {
        if (Time.time > nextAttackTime && target)
        {
            if (target && Vector3.Distance(target.position, transform.position) < attackDistance)
            {
                nextAttackTime = Time.time + timeBetweenAttacks;

                if (animator != null)
                {
                    animator.SetBool("attack", true);
                }

                Attacking();
            }
            else
            {
                if (animator != null)
                {
                    animator.SetBool("attack", false);
                }
            }
        }

        if (target == null)
        {
            currentState = State.Idle;
            navMeshAgent.enabled = false;
        }
    }

    protected virtual void Attacking()
    {
        currentState = State.Attacking;
        navMeshAgent.enabled = false;     
        
        target.GetComponent<LivingEntity>().TakeHit(1f);

        currentState = State.Chasing;
        navMeshAgent.enabled = true;
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

    protected virtual IEnumerator UpdatePath()
    {
        while(target != null)
        {
            if(currentState == State.Chasing)
            {
                Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
                if (!livingEntity.dead)
                {
                    navMeshAgent.SetDestination(targetPosition);
                }
            }
            
            yield return new WaitForSeconds(refreshRateOfUpdatePath);
        }
    }
}
