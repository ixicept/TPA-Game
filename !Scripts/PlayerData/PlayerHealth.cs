using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public PlayerData playerData;
    public float health;
    public float maxHealth;
    public TextMeshProUGUI healthText;
    public bool isDead;
    public TextDamage textDamage;
    private float healthRegenCooldown;
    private float healthRegenAmount;
    private float timeSinceLastDamage;

    public MovementScript playerMovement;
    public MovementDash playerDash;
    public GameObject freeCamGameObject;
    public Animator animator;


    void Start()
    {
        playerMovement.enabled = true;
        playerDash.enabled = true;
        freeCamGameObject.SetActive(true);
        maxHealth = playerData.maxHealth;
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        isDead = false;
        healthRegenCooldown = playerData.healthRegenerationCooldown;
        healthRegenAmount = playerData.healthRegenAmount;
        timeSinceLastDamage = 0f;
        UpdateHealthText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(100);
        }
        if (timeSinceLastDamage < healthRegenCooldown)
        {
            timeSinceLastDamage += Time.deltaTime;
        }

      
        if (!isDead && timeSinceLastDamage >= healthRegenCooldown)
        {
            RegenerateHealth();
        }

        if (Mathf.Abs(healthSlider.value - health) > Mathf.Epsilon)
        {
            LeanTween.value(gameObject, healthSlider.value, health, 0.5f)
                .setOnUpdate((float value) =>
                {
                    healthSlider.value = value;
                });
        }
    }

    void RegenerateHealth()
    {
        health += healthRegenAmount * Time.deltaTime;
        health = Mathf.Min(health, maxHealth); 
        UpdateHealthText();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Max(0f, health);
        timeSinceLastDamage = 0f;
        textDamage.CreatePopUp(transform.position, damage);
        UpdateHealthText();
        if (health == 0)
        {
            isDead = true;
            playerMovement.enabled = false;
            playerDash.enabled = false;
            freeCamGameObject.SetActive(false); 
            animator.SetBool("isDead", isDead);
            StartCoroutine(LoadSceneAfterDelay(3f));
        }
    }
    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        maxHealth = playerData.maxHealth;
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;

        LoadingScript.Instance.LoadScene("OverWorld");
    }

    void UpdateHealthText()
    {
        healthText.text = Mathf.RoundToInt(health).ToString();
    }

}
