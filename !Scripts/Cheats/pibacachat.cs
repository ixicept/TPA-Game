 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pibacachat : MonoBehaviour
{
    public GameObject canvasObject;
    public PlayerData playerData;
    public PlayerHealth playerHealth;
    public void PIBACACHATWOI()
    {
        playerHealth.health = 10000f;
        playerHealth.maxHealth = 10000f;
        playerHealth.healthSlider.maxValue = playerHealth.maxHealth;
        playerHealth.healthSlider.value = playerHealth.health;
        playerHealth.healthText.text = playerHealth.health.ToString();
        canvasObject.SetActive(false);
        StartCoroutine(ShowCanvasForDuration(3f));
    }

    IEnumerator ShowCanvasForDuration(float duration)
    {
        canvasObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        canvasObject.SetActive(false);
    }
}
