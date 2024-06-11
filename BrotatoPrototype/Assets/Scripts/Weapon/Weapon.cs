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

    [Header("������������� ���������: ")]
    [Tooltip("���������:")]
    [SerializeField] protected float attackRange;
    [Tooltip("��������� ����:")]
    [SerializeField] protected float startDamage;
    [Tooltip("���-�� ���� � �������:")]
    [SerializeField] protected float startAttackSpeed;
    [Tooltip("Basic Loop Attack")]
    [SerializeField] protected float _timeLoop = 2f;
    [Tooltip("����������� ���� �����:")]
    [Range(0, 1)]
    [SerializeField] protected float startCritChance;
    [SerializeField] protected bool knockBack = false;
    [SerializeField] protected float repulsiveForce = 15f;
    [Header("����� �� �������� ���")]
    [Range(0, 100)]
    [SerializeField] protected float percantageOfMelleDamage = 100;
    [Header("����� �� �������� ���")]
    [Range(0, 100)]
    [SerializeField] protected float percantageOfRangedDamage = 0;
    [Tooltip("�������� ���� ������")]
    [SerializeField] public string soundName = "default";

    [Space]
    [SerializeField] protected Animator animator;
    [SerializeField] protected float timeOfAttack = 0.5f; // for sword ��� ����� ����� �� ����������� � �������� ��������, ����� ��������
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

    protected float _currentAnimationTime;
    protected float _currentDelayAttack;

    public float AttackRange { get => attackRange; }
    public float StartDamage { get => startDamage; }
    public float StartAttackSpeed { get => startAttackSpeed;}
    public float StartCritChance { get => startCritChance; }

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

        _startAnimationSpeed = myTime;

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
                   // Debug.Log("����� �� ���������� ����������");
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
        }
    }

    protected void SetAttackSpeed()
    {
        _currentAnimationTime = _startAnimationSpeed * (1 - playerCharacteristics.CurrentAttackSpeedPercentage / 100f - startAttackSpeed / 100f);
        _currentDelayAttack = (_timeLoop - _startAnimationSpeed) * (1 - playerCharacteristics.CurrentAttackSpeedPercentage / 100f - startAttackSpeed / 100f);
    }

    protected void SetCritChance()
    {
        currentCritChance = startCritChance + playerCharacteristics.CurrentCritChancePercentage / 100f;
    }

    protected virtual void SetDamage()
    {
        var dmg = startDamage + playerCharacteristics.CurrentRangedDamage * percantageOfRangedDamage / 100f + playerCharacteristics.CurrentMelleeDamage * percantageOfMelleDamage / 100f;

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
        animator.speed = _currentAnimationTime;
    }

    public void PlaySoundAttack()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play(soundName, this.gameObject.transform.position);
        }
    }
}
