using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinoHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeSlider;
    public MinoScript rhinoScript;
    public TextMeshProUGUI lvlTxt;
    public PlayerData playerData;
    void Start()
    {
        rhinoScript = GetComponent<MinoScript>();
        Debug.Log(rhinoScript.maxHealth);
        healthSlider.maxValue = (float)rhinoScript.maxHealth;
        easeSlider.maxValue = (float)rhinoScript.maxHealth;
        lvlTxt.text = "Level " + rhinoScript.level.ToString();

        int playerLevel = playerData.level;

        int levelDifference = rhinoScript.level - playerLevel;
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
