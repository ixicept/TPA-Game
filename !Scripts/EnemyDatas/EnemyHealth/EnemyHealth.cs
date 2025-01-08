using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float lerpSpeed = 0.05f;
    public Slider healthSlider;
    public Slider easeSlider;   
    void Start()
    {
        currentHealth = maxHealth;

    }
    void Update()
    {
        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(10);
            Debug.Log("enemy Damage");
        }

        if(healthSlider.value != easeSlider.value)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, currentHealth, lerpSpeed);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
