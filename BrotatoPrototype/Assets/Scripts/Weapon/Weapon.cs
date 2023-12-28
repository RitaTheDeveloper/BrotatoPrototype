using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Weapon : MonoBehaviour
{
    public enum Type { mellee, range};
    public float attackRange; 
    public GameObject nearestEnemy;
    [Header("количество атак в секунду")]
    [SerializeField] protected float startAttackSpeed;
    [SerializeField] protected float currentAttackSpeed;
    private GameObject[] allEnemies;
    private float distance;
    protected Transform weaponHolder;
    private PlayerCharacteristics playerCharacteristics;

    public virtual void Attack()
    {

    }

    public void Init()
    {
        weaponHolder = transform.parent;
        playerCharacteristics = GetComponentInParent<PlayerCharacteristics>();
        SetAttackSpeed();
    }

    public void FindTheNearestEnemy()
    {
        nearestEnemy = null;
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

    public void SetAttackSpeed()
    {
        currentAttackSpeed = startAttackSpeed + startAttackSpeed * playerCharacteristics.CurrentAttackSpeedPercentage / 100f;
    }

}
