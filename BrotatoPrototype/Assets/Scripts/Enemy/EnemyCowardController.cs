using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCowardController : EnemyController
{
    [SerializeField] private float _distanceFromPlayer;
    [SerializeField] private float _escapeDelaytime;
    [SerializeField] private float _escapeSpeed;

    private float _timer = 0f;
    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > _distanceFromPlayer)
        {
            Chase();
            Debug.Log("чейзим!");
           // _timer = 0f;
        }
        else if (distance < _distanceFromPlayer)
        {
            _timer += Time.fixedDeltaTime;

            if (_timer >= _escapeDelaytime)
            {
                // run!
                Debug.Log("убегаем!");
                Run();
            }
        }
        //else
        //{
        //    _timer += Time.fixedDeltaTime;

        //    if (_timer >= _escapeDelaytime)
        //    {
        //        // run!
        //        Debug.Log("убегаем!");
        //        Run();
        //    }
        //}
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
        //Vector3 newPosWithCorrectY = new Vector3(newPos.x, transform.position.y, newPos.z);
        //transform.position = Vector3.MoveTowards(transform.position, newPosWithCorrectY, Time.deltaTime * 8f);
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

        //if (Vector3.Distance(transform.position, newPos) <= 2f)
        //{
        //    Debug.Log("обнуляем");
        //    _timer = 0f;
        //}
    }
}
