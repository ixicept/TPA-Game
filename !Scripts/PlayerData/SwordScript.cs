using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    private int damage;
    public PlayerData playerData;
    public MovementScript movementScript;
    public float timeBetweenSwings;
    public LayerMask whatIsEnemy;
    public TextDamage text;
    public Animator animator;

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
        isCombat = movementScript.isCombat;
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToSwing && isCombat)
        {
            animator.SetTrigger("isAttack");
            float timeSinceLastAttack = Time.time - lastAttackTime;

            if (timeSinceLastAttack <= 5 && comboCount <= 4)
            {
                comboCount++;
                if (comboCount == 4)
                {
                    comboCount = 1;
                }
            }
            SwingSword();

        }
    }

    void SwingSword()
    {
        movementScript.enabled = false;
        lastAttackTime = Time.time;
        readyToSwing = false;
        damage = (int)(playerData.attack * Mathf.Sqrt(comboCount));
        Invoke("ResetSwing", timeBetweenSwings);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((whatIsEnemy.value & 1 << other.gameObject.layer) != 0)
        {
            DealDamage(other.gameObject);
        }
    }

    void DealDamage(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy with damage: " + damage);
            enemy.GetComponent<RhinoScript>().TakeDamage(damage);
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
        else if (enemy.CompareTag("Minotaur"))
        {
            Debug.Log("Hit mino with damage: " + damage);
            enemy.GetComponent<MinoScript>().TakeDamage(damage);
        }
    }

    void ResetSwing()
    {
        readyToSwing = true;
        movementScript.enabled = true;
    }
}
