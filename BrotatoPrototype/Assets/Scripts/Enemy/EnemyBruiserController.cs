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
                Debug.Log("чейзим");
                navMeshAgent.speed = GetComponent<UnitParameters>().CurrentMoveSpeed;
                navMeshAgent.acceleration = 8f;
                //Rotation(target.position);
                navMeshAgent.SetDestination(target.position);

            }
            else if (_timer > _cdRushTime && _timer < _cdRushTime + _stopTimeBeforeRush)
            {
                dirToTarget = (target.position - transform.position);
                Rotation(dirToTarget);
                startPos = transform.position;
                navMeshAgent.speed = 0f;
                Debug.Log("готовимс€");
            }
            else
            {
                
                Debug.Log("бежим");
                navMeshAgent.speed = _speedRush;
                navMeshAgent.acceleration = 500f;
                Vector3 newPos = startPos + dirToTarget.normalized * _distance;
                navMeshAgent.SetDestination(newPos);
                                                
                if (!navMeshAgent.pathPending)
                {
                    if (Vector3.Distance(transform.position, target.position) <= navMeshAgent.stoppingDistance)
                    {
                        Debug.Log("ќбнул€ю!!!!!!!");
                        _timer = 0f;
                    }
                    if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                    {
                        if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude < 0.3f)
                        {
                            Debug.Log("ќбнул€ю!");
                            _timer = 0f;
                        }
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
       // material = GetComponentInChildren<MeshRenderer>().materials[0];
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

    private IEnumerator MoveToTarget(Transform obj, Vector3 target)
    {
        Vector3 startPosition = obj.position;
        float t = 0f;

        while(t < 1)
        {
            obj.position = Vector3.Lerp(startPosition, target, t * t * t);
            t += Time.deltaTime / 1.5f;
            yield return null;
        }
    }
}
