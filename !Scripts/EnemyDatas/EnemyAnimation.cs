using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator animator;
    public RhinoScript rhinoScript;
    void Start()
    {
       rhinoScript = GetComponent<RhinoScript>();
        animator = GetComponent<Animator>();
    }

    void Update()

    { 
        animator.SetBool("isAttacking", rhinoScript.isAttacking && !rhinoScript.isWalking && !rhinoScript.isChasing);
        animator.SetBool("isWalking", rhinoScript.isWalking && !rhinoScript.isChasing && !rhinoScript.isAttacking);
        animator.SetBool("isChasing", rhinoScript.isChasing & !rhinoScript.isAttacking & !rhinoScript.isAttacking);
        animator.SetBool("isDead", rhinoScript.isDead) ;
    }
}
