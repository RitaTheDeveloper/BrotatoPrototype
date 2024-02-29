using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IKnockbackable
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
    private Coroutine MoveCoroutine;
    private Rigidbody _rigidbody;
    private float startPositionY;
    
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        currentState = State.Chasing;
        _rigidbody = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent.speed = GetComponent<UnitParameters>().CurrentMoveSpeed;
        MoveCoroutine = StartCoroutine(UpdatePath());
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
        startPositionY = transform.position.y;
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

    public void StopMoving()
    {
        if (MoveCoroutine != null)
        {
            StopCoroutine(MoveCoroutine);
        }
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
    }

    public void GetKnocked(Vector3 force)
    {
        StopCoroutine(MoveCoroutine);
        MoveCoroutine = StartCoroutine(ApplyKnock(force));
    }

    private IEnumerator ApplyKnock(Vector3 force)
    {
        yield return null;

        navMeshAgent.enabled = false;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        Physics.gravity = new Vector3(0, -2.5F, 0);
        _rigidbody.AddForce(force);

        yield return new WaitForFixedUpdate();
        yield return new WaitUntil(() => _rigidbody.velocity.magnitude < 0.05f);
        yield return new WaitForSeconds(0.5f);

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        navMeshAgent.Warp(transform.position);
        navMeshAgent.enabled = true;

        yield return null;

        MoveCoroutine = StartCoroutine(UpdatePath());
    }
}
