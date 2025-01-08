using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    public Slider xpSlider;
    public PlayerData playerData;
    public TextMeshProUGUI levelText;
    public PlayerHealth playerHealth;
    public TextDamage textDamage;
    public TextDamage xpText;
    public TextDamage goldText;
    public GameObject levelVfx;
    private float xp;
    private float maxXP;
    

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
/*        playerData.maxHealth = (1000f * playerData.maxHealthIncreaseModifier) * playerData.level;*/
        maxXP = playerData.maxXp;
        xp = 0;
        xpSlider.maxValue = maxXP;
        xpSlider.value = xp;
        levelText.text = playerData.level.ToString();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GainXP(100);
            Debug.Log("gain xp");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            LevelUp();
            GainGold(100000);
        }

        if (xpSlider.value != xp)
        {
            xp = xpSlider.value;
        }
    }

    public void GainXP(float amount)
    {
        xpText.CreatePopUpString(transform.position, "+" + amount);
        LeanTween.value(gameObject, xpSlider.value, xpSlider.value + amount, 1f)
            .setOnUpdate((float value) =>
            {
                xpSlider.value = value;
            })
            .setOnComplete(() =>
            {
                if (xpSlider.value >= xpSlider.maxValue)
                {
                    LevelUp();
                }
            });
    }

    public void GainGold(int amount)
    {
        playerData.gold += amount;
        goldText.CreatePopUpString(transform.position, "+" + amount);
    }

    void LevelUp()
    {
                textDamage.CreatePopUpString(transform.position, "Level Up!");
        playerData.level++;
        levelText.text = playerData.level.ToString();

        maxXP = maxXP * playerData.experienceIncreaseModifier + maxXP;


        playerData.maxHealth = (playerData.maxHealth * playerData.maxHealthIncreaseModifier) + playerData.maxHealth;
        Debug.Log(playerHealth.maxHealth);
        playerHealth.maxHealth = playerData.maxHealth;
        playerHealth.health = playerData.maxHealth;
        playerHealth.healthSlider.maxValue = playerHealth.maxHealth;
        playerHealth.healthSlider.value = playerHealth.health;
        playerHealth.healthText.text = playerHealth.health.ToString();

        levelVfx.SetActive(true);
        if (xpSlider.value >= xpSlider.maxValue)
        {
            LeanTween.value(gameObject, xpSlider.value, 0f, 0.5f)
                .setOnUpdate((float value) =>
                {
                    xpSlider.value = value;
                });
        }


        playerData.attack = playerData.attack * playerData.attackIncreaseModifier + playerData.attack;
        xpSlider.maxValue = maxXP;
        Invoke("DeactivateVfx", 2f);
    }

    void DeactivateVfx()
    {
        levelVfx.SetActive(false);
    }

}
