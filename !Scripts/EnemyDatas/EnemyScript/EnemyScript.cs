using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;

    public Transform player;
    public PlayerHealth playerHealth;
    public LayerMask whatIsGround, whatIsPlayer;
    public float damage;
    public float health;
    public EnemySpawner enemySpawner;
    public TextDamage textDamage;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;


    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.SetDestination(transform.position);
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            playerHealth.TakeDamage(damage);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        textDamage.CreatePopUp(transform.position, damage);
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    public void DestroyEnemy()
    {
        enemySpawner.EnemyDied();
        Destroy(gameObject);
    }
}