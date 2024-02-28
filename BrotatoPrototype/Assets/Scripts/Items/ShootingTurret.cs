using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingTurret : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private float cdAttack;
    [SerializeField] private float damage;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Projectile projectile;
    public GameObject nearestEnemy;
    private GameObject[] allEnemies;
    private float _distance;
    private float _timer;

    private void Update()
    {
        RotateMuzzle();
    }
    private void FixedUpdate()
    {
        FindTheNearestEnemy();
        _timer += Time.fixedDeltaTime;
        if (_timer >= cdAttack && nearestEnemy && Vector3.Distance(transform.position, nearestEnemy.transform.position) < attackRange)
        {
            Attack();
            _timer = 0f;
        }
    }

    private void FindTheNearestEnemy()
    {
        nearestEnemy = null;
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        Dictionary<float, GameObject> distanceAndEnemy = new Dictionary<float, GameObject>();

        if (allEnemies.Length > 0)
        {
            for (int i = 0; i < allEnemies.Length; i++)
            {
                try
                {
                    _distance = Vector3.Distance(transform.position, allEnemies[i].transform.position);
                    distanceAndEnemy.Add(_distance, allEnemies[i]);
                }
                catch (System.ArgumentException)
                {
                    // Debug.Log("враги на одинаковом расстоянии");
                }
            }
        }

        if (distanceAndEnemy.Count > 0)
        {
            float minDistance = distanceAndEnemy.Keys.Min();
            nearestEnemy = distanceAndEnemy[minDistance];
        }
    }

    private void RotateMuzzle()
    {
        if (nearestEnemy)
        {
            Vector3 dir = nearestEnemy.transform.position - muzzle.position;
            Quaternion rotation = Quaternion.Slerp(muzzle.rotation, Quaternion.LookRotation(dir), 50f * Time.deltaTime);
            //rotation.x = 0f;
            //rotation.z = 0f;
            muzzle.rotation = rotation;

            //muzzle.LookAt(nearestEnemy.transform);
        }
    }

    private void Attack()
    {
        var bullet = Instantiate(projectile, shootPoint.position, shootPoint.rotation);
        bullet.SetDamage(damage);
        bullet.SetRange(30f);
    }
}
