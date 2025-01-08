using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RhinoScript : MonoBehaviour
{
    public string Enemyname;
    public Animator animator;
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public PlayerHealth playerHealth;
    public PlayerData playerData;
    public LevelScript levelScript;
    public LayerMask whatIsGround, whatIsPlayer;
    public EnemySpawner enemySpawner;
    public TextDamage textDamage;

    public RhinoData rhinoData;

    public Vector3 walkPoint;
    bool walkPointSet;

    bool alreadyAttacked;
    bool canTakeDamage = true;

    public bool isChasing = false;
    public bool isAttacking = false;
    public bool isWalking = false;
    public bool isDead = false;

    private float nextAttackTime = 0f;
    private float nextChaseTime = 0f;
    private float nextTakeDamageTime = 0f;
    private float nextWalkTime = 0f;
    public int level;
    public int maxHealth;
    public int damage;
    public int experienceDrop;
    public int goldDrop;
    public int currentHealth;
    public GameObject deadExplosion;
    public void Start()
    {
        Enemyname = gameObject.name.Replace("(Clone)", "");
        level = Random.Range(rhinoData.minLevel, rhinoData.maxLevel + 1);
        maxHealth = Mathf.RoundToInt(rhinoData.minMaxHealth * (1 + rhinoData.healthModifier * level));
        currentHealth = maxHealth;
        damage = Mathf.RoundToInt(rhinoData.minAttack * (1 + rhinoData.attackModifier * level));
        experienceDrop = Mathf.RoundToInt(rhinoData.minExperienceDrop * (1 + rhinoData.experienceDropModifier * level));

        goldDrop = Mathf.RoundToInt(rhinoData.minGoldDrop * (1 + rhinoData.goldDropModifier * level));

    }
    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        agent.speed = rhinoData.BaseSpeed;
    
        nextAttackTime = Time.time + rhinoData.GeneralAttackCooldown;
        nextChaseTime = Time.time + rhinoData.ChaseCooldown;
        nextTakeDamageTime = Time.time + rhinoData.TakeDamageCooldown;
        nextWalkTime = Time.time + rhinoData.WalkCooldown;
    }

    private void Update()
    {
        bool playerInSightRange = Physics.CheckSphere(transform.position, rhinoData.AggroRange, whatIsPlayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, rhinoData.AttackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            agent.speed = (rhinoData.BaseSpeed * rhinoData.WalkSpeedModifier) + rhinoData.BaseSpeed;
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            agent.speed = (rhinoData.BaseSpeed * rhinoData.ChaseSpeedModifier) + rhinoData.BaseSpeed;
            if (Time.time >= nextChaseTime)
            {
                isAttacking = false;
                isChasing = true;
                isWalking = false;
                ChasePlayer();
                nextChaseTime = Time.time + rhinoData.ChaseCooldown;
            }
        }
        if (playerInAttackRange && playerInSightRange)
        {
            isChasing = false;
            isWalking = false;
            if (!alreadyAttacked)
            {
                AttackPlayer();
            }

               
        }
        if (!isChasing)
        {
            isWalking = agent.velocity.magnitude > 0;
        }

    }

    private void Patroling()
    {
        isAttacking = false;
        isChasing = false;
        isWalking = false;
        if (Time.time >= nextWalkTime)
        {
            isAttacking = false;
            isChasing = false;
            isWalking = true;
            if (!walkPointSet)
            {
                SearchWalkPoint();
            }

            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);
            }

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
            }

            nextWalkTime = Time.time + rhinoData.WalkCooldown;
        }
    }


    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-rhinoData.WalkRange, rhinoData.WalkRange);
        float randomX = Random.Range(-rhinoData.WalkRange, rhinoData.WalkRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rhinoData.RotationSpeed);
        if (distanceToPlayer < rhinoData.AggroRange)
        {
            agent.SetDestination(player.position);
            
        }
        else
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        isAttacking = true;
        agent.SetDestination(transform.position);

        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rhinoData.RotationSpeed);
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            isAttacking = true;
            Invoke(nameof(ResetAttack), rhinoData.GeneralAttackCooldown);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("bruh");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision kena player");
            playerHealth.TakeDamage(damage);
        }
    }
  

    public void FinishAttackAnimation()
    {
        isAttacking = false;
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        if (canTakeDamage & isDead == false)
        {
            animator.SetTrigger("isHit");
            damage = Mathf.RoundToInt(damage);
            currentHealth -= (int)damage;
            textDamage.CreatePopUp(transform.position, damage);
            canTakeDamage = false;
            Invoke(nameof(ResetTakeDamage), rhinoData.TakeDamageCooldown);
        }

        if (currentHealth <= 0 & isDead == false)
        {
            isDead = true;
            DestroyEnemy();
            if (deadExplosion != null)
            {
                deadExplosion.SetActive(true);
            }
            agent.isStopped = true;
            agent.updateRotation = false;

        }
    }

    private void ResetTakeDamage()
    {
        canTakeDamage = true;
    }

    public void DestroyEnemy()
    {
        Debug.Log("Bro is Dead");
        enemySpawner.EnemyDied();
        Debug.Log("gold bruh " + goldDrop);
        Debug.Log(experienceDrop);
        levelScript.GainXP(experienceDrop);
        levelScript.GainGold(goldDrop);
        Invoke(nameof(DestroyObject), 3f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
