using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public string Enemyname;
    public Animator animator;
    public EnemySpawner spawner;
    public Transform player;
    public PlayerHealth playerHealth;
    public PlayerData playerData;
    public LevelScript levelScript;
    public LayerMask whatIsGround, whatIsPlayer;
    public TextDamage textDamage;

    public RhinoData bossData;

    public Vector3 walkPoint;
    bool walkPointSet;

    bool alreadyAttacked;
    bool canTakeDamage = true;

    public bool isChasing = false;
    public bool isWalking = false;
    public bool isFlying = false;
    public bool isDead = false;
    public bool isNormalState = false;
    public bool isFireAttackState = false;
    public bool isSpawnMinionsState = false;


    private float nextAttackTime = 0f;
    private float nextChaseTime = 0f;
    private float nextTakeDamageTime = 0f;
    private float nextWalkTime = 0f;
    private float flyStartTime = 2f;
    public int level;
    public int maxHealth;
    public int damage;
    public int experienceDrop;
    public int goldDrop;
    public int currentHealth;
    private float fireDuration = 6.0f; 
    public bool playerInSightRange, playerInAttackRange, playerInFlyingRange;


    public enum BossState { Normal, FireAttack, SpawnMinions };
    public BossState currentState = BossState.Normal;
    public GameObject fireEffect;
    public float stateDuration = 6f; 
    private float nextStateTime;
    private int spawnedMinionsCount = 0;
    public GameObject minionPrefab;
    public int maxMinionsCount = 4;


    private bool canChangeState = true;
    private float stateChangeCooldown = 3f;
    private float nextStateChangeTime;
    private float nextNormalStateTime; 
    private float nextFireAttackStateTime; 
    private float nextSpawnMinionsStateTime;
    public GameObject bossCanvas;


    public void Start()
    {
        Enemyname = "CF";
        level = Random.Range(bossData.minLevel, bossData.maxLevel + 1);
        maxHealth = Mathf.RoundToInt(bossData.minMaxHealth * (1 + bossData.healthModifier * level));
        currentHealth = maxHealth;
        damage = Mathf.RoundToInt(bossData.minAttack * (1 + bossData.attackModifier * level));
        experienceDrop = Mathf.RoundToInt(bossData.minExperienceDrop * (1 + bossData.experienceDropModifier * level));

        goldDrop = Mathf.RoundToInt(bossData.minGoldDrop * (1 + bossData.goldDropModifier * level));

    }
    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        agent.speed = bossData.BaseSpeed;

        nextStateTime = Time.time + stateDuration;
        nextAttackTime = Time.time + bossData.GeneralAttackCooldown;
        nextChaseTime = Time.time + bossData.ChaseCooldown;
        nextTakeDamageTime = Time.time + bossData.TakeDamageCooldown;
        nextWalkTime = Time.time + bossData.WalkCooldown;
        nextNormalStateTime = Time.time; 
        nextFireAttackStateTime = Time.time + stateChangeCooldown; 
        nextSpawnMinionsStateTime = Time.time + stateChangeCooldown;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, bossData.AggroRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, bossData.AttackRange, whatIsPlayer);
        playerInFlyingRange = Physics.CheckSphere(transform.position, 40f, whatIsPlayer);

        if (canChangeState && Time.time >= nextStateChangeTime)
        {
            if (playerInFlyingRange && !playerInAttackRange && !playerInSightRange)
            {
                agent.speed = (bossData.BaseSpeed * 2) + bossData.BaseSpeed;
                isChasing = false;
                isWalking = false;
                isFlying = true;
                isNormalState = false;
                isFireAttackState = false;
                isSpawnMinionsState = false;
                fly();
            }
            else if (!playerInSightRange && !playerInAttackRange && !playerInFlyingRange)
            {
                agent.speed = (bossData.BaseSpeed * bossData.WalkSpeedModifier) + bossData.BaseSpeed;
                Patroling();
            }
            else if (playerInSightRange && !playerInAttackRange)
            {
                agent.speed = (bossData.BaseSpeed * bossData.ChaseSpeedModifier) + bossData.BaseSpeed;
                if (Time.time >= nextChaseTime)
                {
                    isChasing = true;
                    isWalking = false;
                    isFlying = false;
                    isNormalState = false;
                    isFireAttackState = false;
                    isSpawnMinionsState = false;
                    ChasePlayer();
                    nextChaseTime = Time.time + bossData.ChaseCooldown;
                }
            }
            else if (playerInAttackRange && playerInSightRange)
            {
                isChasing = false;
                isWalking = false;
                isFlying = false;
                Debug.Log("transitions");
                CheckStateTransitions();
            }

            if (!isChasing && !isFlying)
            {
                isWalking = agent.velocity.magnitude > 0;
            }
        }

    }
    private void CheckStateTransitions()
    {
        if (Time.time >= nextStateTime)
        {
            int nextStateIndex = Random.Range(0, 3);
            currentState = (BossState)nextStateIndex;

            nextStateTime = Time.time + stateDuration;
            ChangeState(currentState);
        }
    }


    public void ChangeState(BossState newState)
    {
        isNormalState = false;
        isFireAttackState = false;
        isSpawnMinionsState = false;
        isChasing = false;
        isWalking = false;
        isFlying = false;
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * bossData.RotationSpeed);
        if (canChangeState && Time.time >= nextStateChangeTime)
        {
            Debug.Log("changing state");

           
            switch (newState)
            {
                case BossState.Normal:
                    if (Time.time >= nextNormalStateTime)
                    {
                      
                        nextStateChangeTime = Time.time + stateChangeCooldown;
                        nextNormalStateTime = Time.time + 10f; 
                        isNormalState = true;
                        NormalBehavior();
                    }
                    break;
                case BossState.FireAttack:
                    if (Time.time >= nextFireAttackStateTime)
                    {
                        
                        nextStateChangeTime = Time.time + stateChangeCooldown;
                        nextFireAttackStateTime = Time.time + 15f; 
                        isFireAttackState = true;
                        FireAttackBehavior();
                    }
                    break;
                case BossState.SpawnMinions:
                    if (Time.time >= nextSpawnMinionsStateTime)
                    {
                       
                        nextStateChangeTime = Time.time + stateChangeCooldown;
                        nextSpawnMinionsStateTime = Time.time + 20f; 
                        isSpawnMinionsState = true;
                        SpawnMinionsBehavior();
                    }
                    break;
            }

            
            canChangeState = false;
            StartCoroutine(StateChangeCooldown());
        }
    }
    private IEnumerator StateChangeCooldown()
    {
  
        yield return new WaitForSeconds(stateChangeCooldown);

        canChangeState = true;
    }
    private void Patroling()
    {
        isChasing = false;
        isWalking = false;
        isFlying = false;
        isNormalState = false;
        isFireAttackState = false;
        isSpawnMinionsState = false;
        if (Time.time >= nextWalkTime)
        {
            isChasing = false;
            isWalking = true;
            isFlying = false;
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

            nextWalkTime = Time.time + bossData.WalkCooldown;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-bossData.WalkRange, bossData.WalkRange);
        float randomX = Random.Range(-bossData.WalkRange, bossData.WalkRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void fly()
    {
        isChasing = false;
        isWalking = false;
        isFlying = true;
        isNormalState = false;
        isFireAttackState = false;
        isSpawnMinionsState = false;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > 20f && Time.time - flyStartTime > 2f)
        {
            agent.SetDestination(player.position);
        }
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * bossData.RotationSpeed);
        if (distanceToPlayer < bossData.AggroRange)
        {
            agent.SetDestination(player.position);

        }
    }


    private void NormalBehavior()
    {
        isNormalState = true;
        isFireAttackState = false;
        isSpawnMinionsState = false;
        isChasing = false;
        isWalking = false;
        isFlying = false;
        Debug.Log("normal");
        agent.SetDestination(transform.position);

        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * bossData.RotationSpeed);

        if (Vector3.Distance(transform.position, player.position) <= 10f)
        {
            if (!alreadyAttacked)
            {
                Debug.Log("damage player");
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), bossData.GeneralAttackCooldown);
            }
        }

    }

    private void FireAttackBehavior()
    {
            isFireAttackState = true;
            isNormalState = false;
            isSpawnMinionsState = false;
            isChasing = false;
            isWalking = false;
            isFlying = false;
            Debug.Log("fire");
            agent.SetDestination(transform.position);

            Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * bossData.RotationSpeed);
            if (Vector3.Distance(transform.position, player.position) <= 15f)
            {
                if (!alreadyAttacked)
                {
                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), bossData.GeneralAttackCooldown);
                }
            }

    }

    private void SpawnMinionsBehavior()
    {
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * bossData.RotationSpeed);
        isSpawnMinionsState = true;
        isNormalState = false;
        isFireAttackState = false;
        isChasing = false;
        isWalking = false;
        isFlying = false;
        Debug.Log("spawn");
        if (Vector3.Distance(transform.position, player.position) <= 20f)
        {
            while (spawnedMinionsCount < maxMinionsCount)
            {

                Debug.Log("spawn minion");
                Instantiate(minionPrefab, GetRandomSpawnPosition(), Quaternion.identity);
                spawnedMinionsCount++;
            }
        }

    }

    public void StartFly()
    {
        agent.isStopped = true;
        agent.updateRotation = false;
    }

    public void StopFly()
    {
        agent.isStopped = false;
        agent.updateRotation = true;
    }
    public void MinionDestroyed()
    {
        spawnedMinionsCount--;
    }

    public void fireEffectOn()
    {
        fireEffect.SetActive(true);
    }
    public void fireEffectOff()
    {
        fireEffect.SetActive(false);
    }
    public void FinishAttackAnimation()
    {
        isNormalState = false;
    }
    public void FinishFireAnimation()
    {
        isFireAttackState = false;
    }
    public void FinishSpawnAnimation()
    {
        isSpawnMinionsState = false;
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
            Invoke(nameof(ResetTakeDamage), bossData.TakeDamageCooldown);
        }

        if (currentHealth <= 0 & isDead == false)
        {
            isDead = true;
            agent.isStopped = true;
            agent.updateRotation = false;
            levelScript.GainXP(experienceDrop);
            levelScript.GainGold(goldDrop);
            StartCoroutine(ShowCanvasForDuration(5f));
            Invoke("LoadOverworld", 5f);

        }
    }

    IEnumerator ShowCanvasForDuration(float duration)
    {
        bossCanvas.SetActive(true);
        yield return new WaitForSeconds(duration);
        bossCanvas.SetActive(false);
    }


    private void LoadOverworld()
    {
        LoadingScript.Instance.LoadScene("OverWorld");
    }
    private void ResetTakeDamage()
    {
        canTakeDamage = true;
    }

    Vector3 GetRandomSpawnPosition()
    {
        float xPos = Random.Range(152, 250);
        float zPos = Random.Range(156, 236);
        return new Vector3(xPos, 53, zPos);
    }

}
