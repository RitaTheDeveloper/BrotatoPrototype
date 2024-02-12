using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBruiserController : EnemyController
{
    [SerializeField] float _cdRushTime;
    [SerializeField] float _stopTimeBeforeRush;
    [SerializeField] float _speedRush;
    [SerializeField] float _distance;

    private float _timer = 0f;
    Vector3 dirToTarget = Vector3.zero;
    Vector3 startPos = Vector3.zero;    

    private void Update()
    {
        if (target)
        {
            _timer += Time.deltaTime;
            if (_timer < _cdRushTime)
            {
                Chase();

            }
            else if (_timer > _cdRushTime && _timer < _cdRushTime + _stopTimeBeforeRush)
            {
                Stopping();
            }
            else
            {
                Rush();                
            }            
        }                       
    }

    private void Chase()
    {
        Debug.Log("קויחטל");
        navMeshAgent.speed = GetComponent<UnitParameters>().CurrentMoveSpeed;
        navMeshAgent.acceleration = 8f;
        navMeshAgent.SetDestination(target.position);
    }

    private void Stopping()
    {
        dirToTarget = (target.position - transform.position);
        Rotation(dirToTarget);
        startPos = transform.position;
        navMeshAgent.speed = 0f;
        Debug.Log("דמעמגטלסÿ");
    }

    private void Rush()
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
