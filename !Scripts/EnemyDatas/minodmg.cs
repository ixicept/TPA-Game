using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minodmg : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public MinoScript minoScript;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision detected with player");
            playerHealth.TakeDamage(minoScript.damage);
        }
    }
}
