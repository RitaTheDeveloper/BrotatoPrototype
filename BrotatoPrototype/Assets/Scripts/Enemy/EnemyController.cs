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
    protected UnitParameters unitParameters;
    public Transform target;
    protected LivingEntity livingEntity;
    protected float damage;
    private float nextAttackTime;
    
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent.speed = GetComponent<UnitParameters>().CurrentMoveSpeed;
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

        target.GetComponent<LivingEntity>().TakeHit(damage, false);

        currentState = State.Chasing;
        navMeshAgent.enabled = true;
    }

    protected virtual void Init()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        livingEntity = GetComponent<LivingEntity>();
        unitParameters = GetComponent<UnitParameters>();
        damage = unitParameters.CurrentDamage;
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
