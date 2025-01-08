using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public MovementScript movementScript;
    public GameObject canvas;
    public GameObject crosshair; 
    public TextMeshProUGUI levelText; 
    public TextMeshProUGUI nameText; 
    public Slider healthBar;
    public TextMeshProUGUI currentHealthText; 
    public LayerMask whatIsEnemy; 
    public float lockRange = 10f;

    private GameObject lockedEnemy;


    void Update()
    {
        if (IsNearEnemy() && movementScript.isCombat)
        {
            crosshair.SetActive(true);
            canvas.SetActive(true);
        }
        else
        {
            crosshair.SetActive(false);
            canvas.SetActive(false);
        }

        if (movementScript.isCombat)
        {
            LockOntoNearestEnemy();
        }

        if (lockedEnemy != null)
        {

            UpdateUI();
        }
    }

    bool IsNearEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, lockRange, whatIsEnemy);
        return colliders.Length > 0;
    }

    void LockOntoNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, lockRange, whatIsEnemy);
        if (colliders.Length > 0)
        {
   
            float minDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = collider.gameObject;
                }
            }

            lockedEnemy = nearestEnemy;

            if (crosshair != null && lockedEnemy != null)
            {
                Vector3 spawnPosition = lockedEnemy.transform.position;
                spawnPosition.y += 2f; 
                crosshair.transform.position = spawnPosition;


                Vector3 directionToPlayer = transform.position - spawnPosition;
                crosshair.transform.rotation = Quaternion.LookRotation(directionToPlayer);
            }
        }
        else
        {
            lockedEnemy = null;
        }
    }

    void UpdateUI()
    {

        if (lockedEnemy.CompareTag("Enemy"))
        {
            levelText.text = "Level " + lockedEnemy.GetComponent<RhinoScript>().level.ToString();

            nameText.text = lockedEnemy.GetComponent<RhinoScript>().Enemyname;

            healthBar.maxValue = lockedEnemy.GetComponent<RhinoScript>().maxHealth;
            healthBar.value = lockedEnemy.GetComponent<RhinoScript>().currentHealth;

            currentHealthText.text = lockedEnemy.GetComponent<RhinoScript>().currentHealth.ToString();

            if (lockedEnemy.GetComponent<RhinoScript>().isDead)
            {
                canvas.SetActive(false);
                crosshair.SetActive(false);
            }
        }
        else if (lockedEnemy.CompareTag("Boss"))
        {
            levelText.text = "Level " + lockedEnemy.GetComponent<BossScript>().level.ToString();

            nameText.text = lockedEnemy.GetComponent<BossScript>().Enemyname;

            healthBar.maxValue = lockedEnemy.GetComponent<BossScript>().maxHealth;
            healthBar.value = lockedEnemy.GetComponent<BossScript>().currentHealth;

            currentHealthText.text = lockedEnemy.GetComponent<BossScript>().currentHealth.ToString();

            if (lockedEnemy.GetComponent<BossScript>().isDead)
            {
                canvas.SetActive(false);
                crosshair.SetActive(false);
            }
        }
        else if (lockedEnemy.CompareTag("Minion"))
        {
            levelText.text = "Level " + lockedEnemy.GetComponent<MinionScript>().level.ToString();

            nameText.text = lockedEnemy.GetComponent<MinionScript>().Enemyname;

            healthBar.maxValue = lockedEnemy.GetComponent<MinionScript>().maxHealth;
            healthBar.value = lockedEnemy.GetComponent<MinionScript>().currentHealth;

            currentHealthText.text = lockedEnemy.GetComponent<MinionScript>().currentHealth.ToString();

            if (lockedEnemy.GetComponent<MinionScript>().isDead)
            {
                canvas.SetActive(false);
                crosshair.SetActive(false);
            }
        }
        else if (lockedEnemy.CompareTag("Minotaur"))
        {

            levelText.text = "Level " + lockedEnemy.GetComponent<MinoScript>().level.ToString();

            nameText.text = lockedEnemy.GetComponent<MinoScript>().Enemyname;

            healthBar.maxValue = lockedEnemy.GetComponent<MinoScript>().maxHealth;
            healthBar.value = lockedEnemy.GetComponent<MinoScript>().currentHealth;

            currentHealthText.text = lockedEnemy.GetComponent<MinoScript>().currentHealth.ToString();

            if (lockedEnemy.GetComponent<MinoScript>().isDead)
            {
                canvas.SetActive(false);
                crosshair.SetActive(false);
            }
        }

    }

}
