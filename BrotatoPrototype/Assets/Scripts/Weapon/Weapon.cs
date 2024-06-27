using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Weapon : MonoBehaviour
{
    public enum Tier { one, two, three, four }
    public Tier tier;
    public enum Type { Melee, Gun }
    public Type type;
    [SerializeField] WeaponModifiers weaponModifiers;
    Dictionary<Tier, WeaponModifiers.Modifiers> modifiers = new Dictionary<Tier, WeaponModifiers.Modifiers>();

    [Header("Ќастраиваемые параметры: ")]
    [Tooltip("дальность:")]
    [SerializeField] protected float attackRange;
    [Tooltip("начальный урон:")]
    [SerializeField] protected float startDamage;
    [Tooltip("кол-во атак в секунду:")]
    [SerializeField] protected float startAttackSpeed;
    [Tooltip("Basic Loop Attack")]
    [SerializeField] protected float _timeLoop = 2f;
    [Tooltip("веро€тность крит шанса:")]
    [Range(0, 1)]
    [SerializeField] protected float startCritChance;
    [SerializeField] protected bool knockBack = false;
    [SerializeField] protected float repulsiveForce = 15f;
    [Header("—кейл от ближнего бо€")]
    [Range(0, 100)]
    [SerializeField] protected float percantageOfMelleDamage = 100;
    [Header("—кейл от дальнего бо€")]
    [Range(0, 100)]
    [SerializeField] protected float percantageOfRangedDamage = 0;
    [Tooltip("Ќазвание типа звуков")]
    [SerializeField] public string soundName = "default";

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
    protected float _startAnimationTime;
    protected float _startDelayTime;
    protected float _currentTimeLoop;
    protected float _currentTimeOfAttack;

    protected float _currentAnimationTime;
    protected float _currentDelayAttack;

    public float AttackRange { get => attackRange; }
    public float StartDamage { get => startDamage; }
    public float StartAttackSpeed { get => startAttackSpeed;}
    public float StartCritChance { get => startCritChance; }

    public WeaponBaff baff;

    private void Awake()
    {
        SetCharacteristicsDependingOnTier();
    }

    protected void Init()
    {
        playerCharacteristics = GetComponentInParent<PlayerCharacteristics>();

        weaponHolder = transform.parent;

        float myTime = 0;
        RuntimeAnimatorController myAnimatorClip = animator.runtimeAnimatorController;

        for (int i = 0; i < myAnimatorClip.animationClips.Length; i++)
            myTime = myAnimatorClip.animationClips[i].length;

        _startAnimationTime = myTime;
        _startDelayTime = _timeLoop - _startAnimationTime;

        SetAttackSpeed();
        SetCritChance();
        SetAnimationSpeed();

        currentDamage = startDamage;
        startRotationWeaponHolder = weaponHolder.rotation;
    }

    protected virtual void Attack()
    {        
        PlaySoundAttack();
    }

    protected void FindTheNearestEnemy()
    {
        nearestEnemy = null;
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = Mathf.Infinity;
        Vector3 position = weaponHolder.position;
        foreach (GameObject go in allEnemies)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                nearestEnemy = go;
                distance = curDistance;
            }
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
        }
    }

    protected void SetAttackSpeed()
    {
        float mod = 1 + playerCharacteristics.CurrentAttackSpeedPercentage / 100f + startAttackSpeed / 100f;
        _currentAnimationTime = _startAnimationTime / mod;
        _currentDelayAttack = _startDelayTime / mod;
        _currentTimeLoop = _currentDelayAttack + _currentAnimationTime;
    }

    protected void SetCritChance()
    {
        currentCritChance = startCritChance + playerCharacteristics.CurrentCritChancePercentage / 100f;
    }

    protected virtual void SetDamage()
    {
        var dmg = startDamage + playerCharacteristics.CurrentRangedDamage * percantageOfRangedDamage / 100f + playerCharacteristics.CurrentMeleeDamage * percantageOfMelleDamage / 100f;

        currentDamage = dmg + dmg * playerCharacteristics.CurrentDamagePercentage / 100f;
        currentDamage = Mathf.Round(currentDamage);

        if (currentDamage < 1)
            currentDamage = 1f;
    }

    private void SetCharacteristicsDependingOnTier()
    {
        modifiers.Add(Tier.one, weaponModifiers.modifiers[0]);
        modifiers.Add(Tier.two, weaponModifiers.modifiers[1]);
        modifiers.Add(Tier.three, weaponModifiers.modifiers[2]);
        modifiers.Add(Tier.four, weaponModifiers.modifiers[3]);

        var myWeaponModifiers = modifiers[tier];

        startDamage *= myWeaponModifiers.forDamage;
        startAttackSpeed *= myWeaponModifiers.forAttackSpeed;
        startCritChance *= myWeaponModifiers.forCritChance;
    }
    protected void SetAnimationSpeed()
    {
        animator.speed = 1 + playerCharacteristics.CurrentAttackSpeedPercentage / 100f + startAttackSpeed / 100f;
    }

    public void PlaySoundAttack()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play(soundName, this.gameObject.transform.position);
        }
    }
}

[System.Serializable]
public class WeaponBaff
{
    public CharacteristicType characteristic = CharacteristicType.None;
    public float value = 0f;
}
