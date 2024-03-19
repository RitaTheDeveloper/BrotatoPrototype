using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Weapon : MonoBehaviour
{
    public enum Tier { one, two, three, four }
    [SerializeField] private Tier tier;
    [SerializeField] WeaponModifiers weaponModifiers;
    Dictionary<Tier, WeaponModifiers.Modifiers> modifiers = new Dictionary<Tier, WeaponModifiers.Modifiers>();
        
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
    [SerializeField] protected bool knockBack = false;

    [Space]
    [SerializeField] protected Animator animator;
    [SerializeField] protected float timeOfAttack = 0.5f; // for sword это время нужно не хардкордить и анимацию ускорять, когда ускоряем
    protected float currentAttackSpeed;
    protected float currentDamage;
    protected float currentCritChance;
    public GameObject nearestEnemy;
    private GameObject[] allEnemies;
    private float distance;
    protected Quaternion startRotationWeaponHolder;
    protected Transform weaponHolder;
    protected PlayerCharacteristics playerCharacteristics;
    private float _startAnimationSpeed;
    protected float _currentTimeOfAttack;

    private void Awake()
    {
        SetCharacteristicsDependingOnTier();
    }

    protected void Init()
    {
        weaponHolder = transform.parent;
        playerCharacteristics = GetComponentInParent<PlayerCharacteristics>();
        _startAnimationSpeed = animator.speed;
        SetAttackSpeed();
        SetCritChance();
        SetAnimationSpeed(currentAttackSpeed);
        SetTimeOfAnimation(currentAttackSpeed);
        currentDamage = startDamage;
        startRotationWeaponHolder = weaponHolder.rotation;
    }

    protected virtual void Attack()
    {

    }

    protected void FindTheNearestEnemy()
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

    protected virtual void RotateWeaponHolder()
    {
        if (nearestEnemy)
        {
            Vector3 dir = nearestEnemy.transform.position - weaponHolder.position;
            Quaternion rotation = Quaternion.Slerp(weaponHolder.rotation, Quaternion.LookRotation(dir), 20f * Time.deltaTime);
            rotation.x = 0f;
            rotation.z = 0f;
            weaponHolder.rotation = rotation;

            //weaponHolder.LookAt(nearestEnemy.transform);
        }
    }

    protected void SetAttackSpeed()
    {
        currentAttackSpeed = startAttackSpeed + startAttackSpeed * playerCharacteristics.CurrentAttackSpeedPercentage / 100f;
    }

    protected void SetCritChance()
    {
        currentCritChance = startCritChance + playerCharacteristics.CurrentCritChancePercentage / 100f;
    }

    protected virtual void SetDamage()
    {
        
    }

    protected virtual void ReturnWeponHolderRotationToStarting()
    {
        weaponHolder.rotation = startRotationWeaponHolder;
    }

    private void SetCharacteristicsDependingOnTier()
    {
        modifiers.Add(Tier.one, weaponModifiers.modifiers[0]);
        modifiers.Add(Tier.two, weaponModifiers.modifiers[1]);
        modifiers.Add(Tier.three, weaponModifiers.modifiers[2]);
        modifiers.Add(Tier.four, weaponModifiers.modifiers[3]);

        var myWeaponModifiers = modifiers[tier];
        startDamage = startDamage * myWeaponModifiers.forDamage;
        startAttackSpeed = startAttackSpeed * myWeaponModifiers.forAttackSpeed;
        startCritChance = startCritChance * myWeaponModifiers.forCritChance;
    }
    protected void SetAnimationSpeed(float currentAttackSpeed)
    {
        // нам не нужно уменьшать скорость анимации, только увеличивать
        if (currentAttackSpeed > 1)
        {
            animator.speed = _startAnimationSpeed * currentAttackSpeed;
            //animator.SetFloat("Speed", _startAnimationSpeed * currentAttackSpeed);
            //Debug.Log("multiplier"+ _startAnimationSpeed * currentAttackSpeed);
        }
    }

    protected void SetTimeOfAnimation(float currentAttackSpeed)
    {
        // нам не нужно уменьшать время анимации, только увеличивать
        if (currentAttackSpeed > 1)
        {
            _currentTimeOfAttack = timeOfAttack / currentAttackSpeed;
        }
        else
        {
            _currentTimeOfAttack = timeOfAttack;
        }
    }
}
