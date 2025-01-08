using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RhinoData", menuName = "Rhino Data")]
public class RhinoData : ScriptableObject
{
    public int minLevel = 1;
    public int maxLevel = 20;
    public int minMaxHealth = 1000;
    public int minAttack = 100;
    public int minExperienceDrop = 100;
    public int minGoldDrop = 100;
    public float healthModifier = 0.1f;
    public float attackModifier = 0.1f;
    public float experienceDropModifier = 0.1f;
    public float goldDropModifier = 0.1f;

    public float BaseSpeed = 2f;
    public float RotationSpeed = 0.05f;
    public float WalkSpeedModifier = 1f;
    public float WalkRange = 10f;
    public float WalkCooldown = 3f;
    public float AggroRange = 15f;
    public float ChaseSpeedModifier = 1.5f;
    public float ChaseCooldown = 0.5f;
    public float GeneralAttackCooldown = 2.5f;
    public float TakeDamageCooldown = 0.5f;

    public float DamageMultiplier = 1f;
    public int HitCount = 1;
    public float AttackRange = 2.5f;
    public float AttackCooldown = 3f;
}
