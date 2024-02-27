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
        
    [Header("Ќастраиваемые параметры: ")]
    [Tooltip("дальность:")]
    [SerializeField] protected float attackRange;
    [Tooltip("начальный урон:")]
    [SerializeField] protected float startDamage;
    [Tooltip("кол-во атак в секунду:")]
    [SerializeField] protected float startAttackSpeed;
    [Tooltip("веро€тность крит шанса:")]
    [Range(0,1)]
    [SerializeField] protected float startCritChance;

    [Space]
    [SerializeField] protected Animator animator;
    [SerializeField] protected float timeOfAttack = 0.5f; // for sword это врем€ нужно не хардкордить и анимацию ускор€ть, когда ускор€ем
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

    public string IdWeapon;
    [Tooltip("—тоимость оружи€:")]
    [SerializeField] public int Price;
    [Tooltip("—кидка при продаже %:")]
    [SerializeField] public int DiscountProcent;
    [Tooltip("”ровень предмета:")]
    [SerializeField] public int LevelItem;
    [Tooltip("ћинимальна€ волна:")]
    [SerializeField] public int MinWave;

    [Header("ѕараметры отображени€: ")]
    [Tooltip("Ќазвание оружи€:")]
    [SerializeField] public string NameWeapon;
    [Tooltip("“ип оружи€:")]
    [SerializeField] public string TypeWeapon;
    [Tooltip("»конка оружи€:")]
    [SerializeField] public Sprite IconWeapon;

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
        IdWeapon = this.name;
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
                   // Debug.Log("враги на одинаковом рассто€нии");
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
            Quaternion rotation = Quaternion.Slerp(weaponHolder.rotation, Quaternion.LookRotation(dir), 6f * Time.deltaTime);
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
        // нам не нужно уменьшать врем€ анимации, только увеличивать
        if (currentAttackSpeed > 1)
        {
            _currentTimeOfAttack = timeOfAttack / currentAttackSpeed;
        }
        else
        {
            _currentTimeOfAttack = timeOfAttack;
        }
    }
    public int GetPrice(int wave)
    {
        return Price + wave + (int)(Price * wave * 0.01f); // расчет цены за определенную волну (wave)
    }
}
