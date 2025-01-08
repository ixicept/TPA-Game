using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    Animator animator;
    public BossScript bossScript;
    void Start()
    {
        bossScript = GetComponent<BossScript>();
        animator = GetComponent<Animator>();
    }

    void Update()

    {
        animator.SetBool("isNormalState", bossScript.isNormalState && !bossScript.isFireAttackState && !bossScript.isSpawnMinionsState);
        animator.SetBool("isFireAttackState", bossScript.isFireAttackState && !bossScript.isNormalState && !bossScript.isSpawnMinionsState);
        animator.SetBool("isSpawnMinionsState", bossScript.isSpawnMinionsState && !bossScript.isNormalState && !bossScript.isFireAttackState);
        animator.SetBool("isDead", bossScript.isDead);
        animator.SetBool("isWalking", bossScript.isWalking);
        animator.SetBool("isChasing", bossScript.isChasing);
        animator.SetBool("isFlying", bossScript.isFlying);
    }
}
