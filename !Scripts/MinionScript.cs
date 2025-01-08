using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionScript : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public BossScript boss;
    public Transform player;
    public PlayerHealth playerHealth;
    public LayerMask whatIsGround, whatIsPlayer;
    public float damage;
    public float health;
    public TextDamage textDamage;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float currentHealth;
    public float lerpSpeed = 0.05f;
    public Slider healthSlider;
    public Slider easeSlider;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerHealth = player.GetComponent<PlayerHealth>();
        boss = boss.GetComponent<BossScript>();
        currentHealth = health;

    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(10);
            Debug.Log("enemy Damage");
        }

        if (healthSlider.value != easeSlider.value)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, currentHealth, lerpSpeed);
        }

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

        transform.LookAt(player);

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
        currentHealth -= damage;
        currentHealth = Mathf.Max(0f, currentHealth);
        healthSlider.value = currentHealth;
        textDamage.CreatePopUp(transform.position, damage);
        if (currentHealth <= 0)
        {
            DestroyEnemy();
        };
    }
    public void DestroyEnemy()
    {
        boss.MinionDestroyed();
        Destroy(gameObject);
        Debug.Log("minion died");
    }
}
