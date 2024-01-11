using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterController : EnemyController
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float damage;
    [SerializeField] private float range;

    private Projectile _projectile;

    protected override void Attacking()
    {
        _projectile = Instantiate(_projectilePrefab, _shootPoint.position, _shootPoint.rotation);
        _projectile.SetRange(range);
        _projectile.SetDamage(damage);        
    }

    protected override IEnumerator UpdatePath()
    {
        while (target != null)
        {
            if (currentState == State.Chasing)
            {
                //Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
                //if (!livingEntity.dead)
                //{
                //    navMeshAgent.SetDestination(targetPosition);
                //}

                if (Vector3.Distance(transform.position, target.position) < navMeshAgent.stoppingDistance + 2f)
                {
                    Debug.Log("ух, близко");
                     Vector3 dirToTarget = (target.position - transform.position).normalized;
                    //Quaternion lookRotation = Quaternion.LookRotation(dirToTarget);
                    //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                    transform.LookAt(transform.position - dirToTarget);
                    Vector3 newPos = transform.position - dirToTarget * 5f;
                   // Debug.Log(newPos);
                    //transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * 7f);
                   // navMeshAgent.SetDestination(newPos);
                }
                else
                {
                    Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
                    navMeshAgent.SetDestination(targetPosition);
                }
            }


            yield return new WaitForSeconds(refreshRateOfUpdatePath);
        }
    }
}
