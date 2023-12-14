using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Weapon : MonoBehaviour
{
    public float attackRange;
    public GameObject nearestEnemy;
    private GameObject[] allEnemies;
    private float distance;
    private Transform weaponHolder;

    public virtual void Attack()
    {

    }

    public void Init()
    {
        weaponHolder = transform.parent;
    }

    public void FindTheNearestEnemy()
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        Dictionary<float, GameObject> distanceAndEnemy = new Dictionary<float, GameObject>();

        if (allEnemies.Length > 0)
        {

            for (int i = 0; i < allEnemies.Length; i++)
            {
                distance = Vector3.Distance(transform.position, allEnemies[i].transform.position);

                if (distance < attackRange)
                {
                    try
                    {
                        // добавляем в словарь врагов по дистанции
                        distanceAndEnemy.Add(distance, allEnemies[i]);
                    }
                    catch (System.ArgumentException)
                    {
                        Debug.Log("враги на одинаковом расстоянии");
                    }
                }
            }
        }

        if (distanceAndEnemy.Count > 0)
        {
            float minDistance = distanceAndEnemy.Keys.Min();
            nearestEnemy = distanceAndEnemy[minDistance];
        }
    }

    public void RotateWeaponHolder()
    {
        if (nearestEnemy)
        {
            weaponHolder.LookAt(nearestEnemy.transform);
        }        
    }
}
