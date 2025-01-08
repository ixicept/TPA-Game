using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossdmg : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public BossScript bossScript;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision detected with player");
            playerHealth.TakeDamage(bossScript.damage);
        }
    }

}
