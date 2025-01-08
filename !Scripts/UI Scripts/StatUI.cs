using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerHealth playerHealth;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI goldText;

    private void Update()
    {
        levelText.text = playerData.level.ToString();
        maxHealthText.text = playerHealth.health.ToString();
        attackText.text = playerData.attack.ToString();
        goldText.text = playerData.gold.ToString();
    }

}
