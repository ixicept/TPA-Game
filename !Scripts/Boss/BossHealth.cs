using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeSlider;
    public BossScript rhinoScript;
    public TextMeshProUGUI lvlTxt;
    public PlayerData playerData;
    void Start()
    {
        rhinoScript = GetComponent<BossScript>();
        Debug.Log(rhinoScript.maxHealth);
        healthSlider.maxValue = (float)rhinoScript.maxHealth;
        easeSlider.maxValue = (float)rhinoScript.maxHealth;
        lvlTxt.text = "Level " + rhinoScript.level.ToString();

        int playerLevel = playerData.level;

        int levelDifference = rhinoScript.level - playerLevel;
        Debug.Log("level difference: " + levelDifference);

        if (levelDifference >= 10)
        {
            lvlTxt.color = Color.red;
        }
        else if (levelDifference >= 4)
        {
            lvlTxt.color = Color.yellow;
        }
        else if (levelDifference >= -3)
        {
            lvlTxt.color = Color.green;
        }
        else
        {
            lvlTxt.color = Color.red;
        }
    }

    void Update()
    {
        Debug.Log(rhinoScript.currentHealth);
        Debug.Log(healthSlider.value);
        if (healthSlider.value != rhinoScript.currentHealth)
        {
            healthSlider.value = rhinoScript.currentHealth;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            rhinoScript.TakeDamage(100);
            Debug.Log("enemy Damage");
        }

        if (healthSlider.value != easeSlider.value)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, rhinoScript.currentHealth, 1f * Time.deltaTime);
        }
    }
}
