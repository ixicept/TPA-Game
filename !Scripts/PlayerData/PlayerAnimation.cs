using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    private MovementScript movementScript;
    void Start()
    {
        movementScript = GetComponentInParent<MovementScript>();
        animator = GetComponent<Animator>();
    }

    void Update()
       
    {
        animator.SetBool("isCombat", movementScript.isCombat);
        animator.SetBool("isRunning", movementScript.isRunning && !movementScript.isWalking && movementScript.isMoving);
        animator.SetBool("isWalking", movementScript.isWalking && !movementScript.isRunning && movementScript.isMoving);
        animator.SetBool("isJumping", movementScript.isJumping);
        animator.SetBool("isGround", movementScript.grounded);
        animator.SetBool("isHardLand", movementScript.isHardLanding);
    }
}
