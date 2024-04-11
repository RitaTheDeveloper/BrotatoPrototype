using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCowardController : EnemyController
{
    [SerializeField] private float _distanceFromPlayer;
    [SerializeField] private float _escapeSpeed;
    private float distanceDelay = 1f;

    private float _timer = 0f;
    private void FixedUpdate()
    {
        //float distance = Vector3.Distance(transform.position, target.position);

        //if (distance >= _distanceFromPlayer + distanceDelay)
        //{
        //    Chase();
        //}
        ////else if (distance >=_distanceFromPlayer && distance < _distanceFromPlayer + distanceDelay)
        ////{
        ////    navMeshAgent.speed = 0f;
        ////}
        //else if (distance < _distanceFromPlayer)
        //{
        //    Run();
        //}       
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance >= _distanceFromPlayer + distanceDelay)
        {
            Chase();
        }
        //else if (distance >=_distanceFromPlayer && distance < _distanceFromPlayer + distanceDelay)
        //{
        //    navMeshAgent.speed = 0f;
        //}
        else if (distance < _distanceFromPlayer)
        {
            Run();
        }
    }

    private void Chase()
    {
        navMeshAgent.speed = GetComponent<UnitParameters>().CurrentMoveSpeed;
        navMeshAgent.acceleration = 8f;
        navMeshAgent.SetDestination(target.position);
    }

    private void Run()
    {
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 newPos = transform.position - dirToTarget * 10f;
        if (navMeshAgent.enabled)
        {
            navMeshAgent.speed = _escapeSpeed;
            navMeshAgent.acceleration = 300f;
            navMeshAgent.SetDestination(newPos);

            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude < 0.3f)
                    {
                        Debug.Log("обнуляем");
                        _timer = 0f;
                    }
                }
            }
        }
                
    }
}
