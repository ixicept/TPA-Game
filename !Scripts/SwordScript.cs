using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public int damage;
    public MovementScript movementScript;
    public float timeBetweenSwings;
    public Transform attackPoint;
    public LayerMask whatIsEnemy;
    public TextDamage text;

    private bool isCombat;
    private bool readyToSwing = true;
    private int comboCount = 1;
    private float lastAttackTime;

    void Start()
    {
        text = GetComponent<TextDamage>();
    }

    public void Update()
    {
        GetComponent<MovementScript>();
        isCombat = movementScript.isCombat;
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToSwing && isCombat)
        {
            float timeSinceLastAttack = Time.time - lastAttackTime;

            if (timeSinceLastAttack <= 5 && comboCount <= 4)
            {
                comboCount++;
                if (comboCount == 4)
                {
                    comboCount = 1;
                    damage = 10;
                }
            }
            SwingSword();

        }
    }

    void SwingSword()
    {
        lastAttackTime = Time.time;
        readyToSwing = false;
        damage = (int)(damage * Mathf.Sqrt(comboCount));
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, 1f, whatIsEnemy);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy with damage: " + damage);
                Debug.Log("combo: " + comboCount);
                enemy.GetComponent<EnemyScript>().TakeDamage(damage);
            }
            else if (enemy.CompareTag("Boss"))
            {
                Debug.Log("Hit boss with damage: " + damage);
                enemy.GetComponent<BossScript>().TakeDamage(damage);
            }
            else if (enemy.CompareTag("Minion"))
            {
                Debug.Log("Hit minion with damage: " + damage);
                enemy.GetComponent<MinionScript>().TakeDamage(damage);
            }
        }

        Invoke("ResetSwing", timeBetweenSwings);
    }
    void ResetSwing()
    {
        readyToSwing = true;
    }

}
