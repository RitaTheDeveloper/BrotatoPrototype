using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Weapon : MonoBehaviour
{
    public enum Type { mellee, range};
    [Header("Настраиваемые параметры: ")]
    [Tooltip("дальность:")]
    [SerializeField] protected float attackRange;
    [Tooltip("начальный урон:")]
    [SerializeField] protected float startDamage;
    [Tooltip("кол-во атак в секунду:")]
    [SerializeField] protected float startAttackSpeed;
    [Tooltip("вероятность крит шанса:")]
    [Range(0,1)]
    [SerializeField] protected float startCritChance;  

    [Space]
    [SerializeField] protected float currentAttackSpeed;
    [SerializeField] protected float currentDamage;
    [SerializeField] protected float currentCritChance;
    public GameObject nearestEnemy;
    private GameObject[] allEnemies;
    private float distance;
    protected Quaternion startRotationWeaponHolder;
    protected Transform weaponHolder;
    protected PlayerCharacteristics playerCharacteristics;

    public virtual void Attack()
    {

    }

    public void Init()
    {
        weaponHolder = transform.parent;
        playerCharacteristics = GetComponentInParent<PlayerCharacteristics>();
        SetAttackSpeed();
        SetCritChance();
        currentDamage = startDamage;
        startRotationWeaponHolder = weaponHolder.rotation;
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
                distance = Vector3.Distance(weaponHolder.position, allEnemies[i].transform.position);

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

    public void FindTheNearestEnemy2()
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
                    distance = Vector3.Distance(weaponHolder.position, allEnemies[i].transform.position);
                    distanceAndEnemy.Add(distance, allEnemies[i]);
                }
                catch (System.ArgumentException)
                {
                    Debug.Log("враги на одинаковом расстоянии");
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
            Vector3 dir = nearestEnemy.transform.position - weaponHolder.position;
            Quaternion rotation = Quaternion.Slerp(weaponHolder.rotation, Quaternion.LookRotation(dir), 6f * Time.deltaTime);
            rotation.x = 0f;
            rotation.z = 0f;
            weaponHolder.rotation = rotation;

            //weaponHolder.LookAt(nearestEnemy.transform);
        }
    }

    public void SetAttackSpeed()
    {
        currentAttackSpeed = startAttackSpeed + startAttackSpeed * playerCharacteristics.CurrentAttackSpeedPercentage / 100f;
    }

    public void SetCritChance()
    {
        currentCritChance = startCritChance + playerCharacteristics.CurrentCritChancePercentage / 100f;
    }

    public virtual void SetDamage()
    {
        
    }

    protected virtual void ReturnWeponHolderRotationToStarting()
    {
        weaponHolder.rotation = startRotationWeaponHolder;
    }

}
