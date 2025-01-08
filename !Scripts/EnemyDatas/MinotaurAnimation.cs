using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurAnimation : MonoBehaviour
{
    Animator animator;
    public MinoScript rhinoScript;
    void Start()
    {
        rhinoScript = GetComponent<MinoScript>();
        animator = GetComponent<Animator>();
    }

    void Update()

    {
        animator.SetBool("isAttacking", rhinoScript.isAttacking && !rhinoScript.isWalking && !rhinoScript.isChasing);
        animator.SetBool("isWalking", rhinoScript.isWalking && !rhinoScript.isChasing && !rhinoScript.isAttacking);
        animator.SetBool("isChasing", rhinoScript.isChasing & !rhinoScript.isAttacking & !rhinoScript.isAttacking);
        animator.SetBool("isDead", rhinoScript.isDead);
        animator.SetBool("isAttack1", rhinoScript.isAttacking && !rhinoScript.isWalking && !rhinoScript.isChasing && rhinoScript.isAttack1 && !rhinoScript.isAttack2);
        animator.SetBool("isAttack2", rhinoScript.isAttacking && !rhinoScript.isWalking && !rhinoScript.isChasing && !rhinoScript.isAttack1 && rhinoScript.isAttack2);
    }
}
