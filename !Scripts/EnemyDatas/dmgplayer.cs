using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmgplayer : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public RhinoScript rhinoScript;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision detected with player");
            playerHealth.TakeDamage(rhinoScript.damage);
        }
    }

}
