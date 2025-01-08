using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaScript : MonoBehaviour
{
    public Transform player;
    public LayerMask whatIsPlayer;
    public float sightRange;
    public bool playerInSightRange = false;
    private bool hasGivenWarning = false; 

    private void Update()
    {
        if (playerInSightRange && !hasGivenWarning)
        {
            GiveWarning(); 
            hasGivenWarning = true; 
        }
        else if (!playerInSightRange)
        {
            hasGivenWarning = false; 
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    }

    public abstract void GiveWarning();
}