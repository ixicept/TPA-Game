using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public EnemySpawner spawner;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public EnemyData enemyData; 
    public TextDamage textDamage;
    public Vector3 walkPoint;

    private bool walkPointSet;
    private float timeBetweenAttacks;
    private bool alreadyAttacked;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timeBetweenAttacks = enemyData.attackCooldown;
    }

    private void Update()
    {
        bool playerInSightRange = Physics.CheckSphere(transform.position, enemyData.aggroRange, whatIsPlayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, enemyData.attackRange, whatIsPlayer);

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
        float randomZ = Random.Range(-enemyData.walkPointRange, enemyData.walkPointRange);
        float randomX = Random.Range(-enemyData.walkPointRange, enemyData.walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > enemyData.attackRange)
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
            player.GetComponent<PlayerHealth>().TakeDamage(enemyData.minimumAttack);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        enemyData.minimumMaxHealth -= damage;
        textDamage.CreatePopUp(transform.position, damage);
        if (enemyData.minimumMaxHealth <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        spawner.EnemyDied();
        Destroy(gameObject);
    }
}
