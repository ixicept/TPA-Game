using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniondmg : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public MinionScript minionScript;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision detected with player");
            playerHealth.TakeDamage(minionScript.damage);
        }
    }
}
