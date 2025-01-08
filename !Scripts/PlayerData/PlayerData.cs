using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{

    public float baseSpeed = 5f;
    public float walkSpeed = 0.4f;
    public float runSpeed = 1.25f;
    public float combatSpeed = 1.4f;
    public float dashSpeed = 2.5f;

    public float groundDrag = 5f;
    public float jumpForce = 10f;
    public float jumpCooldown = 1f;
    public float airMultiplier = 0.5f;
    public float gravity = 9.8f;

    public float dashForce = 10f;
    public float dashSpeedModifier = 2.5f;

    public float dashCd = 1.75f;
    public bool dashCooldown = false;
    public int dashCount = 0;

    public float rollSpeed = 1f;
    public float hardLandTreshHold = 10f;

    // Health and stuff
    public float maxHealth = 1000f;
    public float maxHealthIncreaseModifier = 0.1f;
    public float healthRegenerationCooldown = 1f;
    public float healthRegenAmount = 10f;


    public float attack = 50f;
    public float attackIncreaseModifier = 0.1f;

    public float maxXp = 500f;
    public float experienceIncreaseModifier = 0.25f;

    public float gold = 10000f;

    public int level = 1;
}
