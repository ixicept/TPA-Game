using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int minimumLevel;
    public int maximumLevel;
    public int minimumMaxHealth;
    public int minimumAttack;
    public int minimumExperienceDrop;
    public int minimumGoldDrop;
    public float healthModifier;
    public float attackModifier;
    public float experienceDropModifier;
    public float goldDropModifier;
    public float walkPointRange;

    public float damageMultiplier;
    public int hitCount;
    public float attackRange;
    public float attackCooldown;

    public float baseSpeed;
    public float rotationSpeed;
    public float walkSpeedModifier;
    public float walkRange;
    public float walkCooldown;
    public float aggroRange;
    public float chaseSpeedModifier;
    public float chaseCooldown;

    public float generalAttackCooldown;
    public float takeDamageCooldown;
}
