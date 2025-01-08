using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAnimation : MonoBehaviour
{

    Animator animator;
    public MinionScript rhinoScript;
    void Start()
    {
        rhinoScript = GetComponent<MinionScript>();
        animator = GetComponent<Animator>();
    }

    void Update()

    {
        animator.SetBool("isAttacking", rhinoScript.isAttacking && !rhinoScript.isWalking && !rhinoScript.isChasing);
        animator.SetBool("isWalking", rhinoScript.isWalking && !rhinoScript.isChasing && !rhinoScript.isAttacking);
        animator.SetBool("isChasing", rhinoScript.isChasing & !rhinoScript.isAttacking & !rhinoScript.isAttacking);
        animator.SetBool("isDead", rhinoScript.isDead);
    }
}
