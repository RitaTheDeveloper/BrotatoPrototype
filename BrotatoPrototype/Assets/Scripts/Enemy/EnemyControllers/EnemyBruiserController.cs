using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBruiserController : EnemyController
{
    [SerializeField] private float _cdRushTime;
    [SerializeField] private float _stopTimeBeforeRush;
    [SerializeField] private float _speedRush;
    [SerializeField] private float _distance;
    [SerializeField] private Animator _animator;
    private bool isGetReady = false;
    private float startAnimationSpeed;
    private float animationSpeed;

    private float _timer = 0f;
    Vector3 dirToTarget = Vector3.zero;
    Vector3 startPos = Vector3.zero;    

    private void Update()
    {
        if (target && !GameManager.instance.GameIsOver)
        {
            _timer += Time.deltaTime;
            if (_timer < _cdRushTime)
            {
                Chase();
                if (_animator)
                {
                    // _animator.speed = 1f;
                    _animator.SetTrigger("chase");
                }
            }
            else if (_timer > _cdRushTime && _timer < _cdRushTime + _stopTimeBeforeRush)
            {                                
                Stopping();
                // _animator.speed = 1f;
                
            }
            else
            {
                Rush();
                if (_animator) _animator.SetTrigger("rush");
                // _animator.speed = 1.5f;
                isGetReady = false;
            }            
        }                       
    }

    public override void LoadPar(EnemyTierSettingStandart enemyTierSetting)
    {
        _cdRushTime = enemyTierSetting.CDDirection;
        _stopTimeBeforeRush = enemyTierSetting.TimeStopDirection;
        _speedRush = enemyTierSetting.SpeedDirection;
        _distance = enemyTierSetting.Distance;

        base.LoadPar(enemyTierSetting);
    }

    private void Chase()
    {
        if (navMeshAgent.enabled)
        {
            navMeshAgent.speed = GetComponent<UnitParameters>().CurrentMoveSpeed;
            navMeshAgent.acceleration = 8f;
            navMeshAgent.SetDestination(target.position);
        }
        
    }

    private void Stopping()
    {
        dirToTarget = (target.position - transform.position);
        Rotation(dirToTarget);
        startPos = transform.position;
        navMeshAgent.speed = 0f;
        if (!isGetReady)
        {
            if(_animator) _animator.SetTrigger("getReady");
            isGetReady = true;
        }
    }

    private void Rush()
    {
        if(navMeshAgent.enabled)
        {
            navMeshAgent.speed = _speedRush;
            navMeshAgent.acceleration = 500f;
            Vector3 newPos = startPos + dirToTarget.normalized * _distance;
            navMeshAgent.SetDestination(newPos);

            if (!navMeshAgent.pathPending)
            {
                //if (Vector3.Distance(transform.position, target.position) <= navMeshAgent.stoppingDistance)
                //{
                //    _timer = 0f;
                //}
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude < 0.3f)
                    {
                        _timer = 0f;
                    }
                }
            }
        }
        
    }

    protected override void Attacking()
    {
        base.Attacking();
        _timer = 0f;
    }

    protected override void Init()
    {
        base.Init();
    }

    protected override IEnumerator UpdatePath()
    {
        yield return null;
     
    }

    private void Rotation(Vector3 direction)
    {
        Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 3f * Time.deltaTime);
        rotation.x = 0f;
        rotation.z = 0f;
        transform.rotation = rotation;
    }
}
