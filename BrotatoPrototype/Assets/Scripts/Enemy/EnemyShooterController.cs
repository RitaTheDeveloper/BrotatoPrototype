using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterController : EnemyController
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float range;

    private Projectile _projectile;
    private Transform _containerOfBullets;
    public override void Start()
    {
        base.Start();
        _containerOfBullets = GameObject.Find("Bullets").transform;
    }

    private void Update()
    {
        if (currentState == State.RunAway && target)
        {
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            Vector3 newPos = transform.position - dirToTarget * 3f;
            Vector3 newPosWithCorrectY = new Vector3(newPos.x, transform.position.y, newPos.z);
            transform.position = Vector3.MoveTowards(transform.position, newPosWithCorrectY, Time.deltaTime * 8f);
            
            if (Vector3.Distance(transform.position, target.position) > navMeshAgent.stoppingDistance + 2f)
            {
                currentState = State.Chasing;
            }
        }
    }

    protected override void Attacking()
    {
        Vector3 position = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(position);
        _projectile = Instantiate(_projectilePrefab, _shootPoint.position, _shootPoint.rotation);
        _projectile.transform.parent = _containerOfBullets;
        _projectile.SetRange(range);
        _projectile.SetDamage(damage);        
    }

    protected override IEnumerator UpdatePath()
    {
        while (target != null)
        {
            if (currentState == State.Chasing)
            {                
                if (Vector3.Distance(transform.position, target.position) < navMeshAgent.stoppingDistance - 2f)
                {
                    currentState = State.RunAway;
                }
                else
                {
                    Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
                    navMeshAgent.enabled = true;
                    navMeshAgent.SetDestination(targetPosition);
                }
            }

            yield return new WaitForSeconds(refreshRateOfUpdatePath);
        }
    }
}
