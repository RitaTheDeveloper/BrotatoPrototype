using UnityEngine;

[CreateAssetMenu(fileName = "Base Characteristic Increment", menuName = "Items Template/BaseIncrement")]
public class BaseCharacteristicIncrement : ScriptableObject
{
    public float percentage = 0.005f;
    public float satiety = 0.05f;
    public float maxHealth = 0.5f;
    public float regenerationHP = 0.025f;
    public float dodge = 0.25f;
    public float armor = 0.25f;
    public float moveSpeed = 0.25f;
    public float attackSpeed = 0.5f;
    public float damage = 0.5f;
    public float meleeDamage = 0.2f;
    public float rangeDamage = 0.2f;
    public float chanceOfCrit = 0.1f;
    public float magneticRadius = 0.1f;
}
