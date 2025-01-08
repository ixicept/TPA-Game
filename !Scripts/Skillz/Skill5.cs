using System.Collections;
using UnityEngine;

public class Skill5 : Skill
{
    public PlayerData playerData;
    public GameObject objectPrefab;
    public Animator animator;
    public float damageMultiplier = 3f;
    private float damage;
    public float cooldownAfterActivation = 2.0f;
    public int maxHitCount = 4;
    public float sphereRadius = 5f;
    private bool isOnCooldown = false;
    public Skill5(string skillName, float skillCooldown) : base(skillName, skillCooldown)
    {
        name = "spawn something skill";
        Cooldown = 10f;
    }

    public override void Activate()
    {
        damage = (playerData.attack * damageMultiplier);
        if (isOnCooldown)
        {
            Debug.Log("Skill is on cooldown.");
            return;
        }

        base.Activate();
        animator.SetTrigger("dedishome");
        Debug.Log("Performing Skill 3 action...");

        StartCoroutine(SpawnPrefabsWithDelay());


    }

    private IEnumerator SpawnPrefabsWithDelay()
    {

        Vector3 spawnPosition = transform.position + transform.forward * 2f + transform.up * 1.5f;

    
        Vector3 directionToPlayer = (transform.position - spawnPosition).normalized;

        Quaternion spawnRotation = Quaternion.LookRotation(directionToPlayer);

        spawnRotation *= Quaternion.Euler(0, 180, 0);

     
        GameObject summonedObject = Instantiate(objectPrefab, spawnPosition, spawnRotation);
        summonedObject.transform.eulerAngles = new Vector3(0, spawnRotation.eulerAngles.y, 0);
        StartCoroutine(StartCooldown());
        yield return new WaitForSeconds(1.2f);
        Vector3 colliderSpawnPosition = spawnPosition + objectPrefab.transform.forward * 5.2f;
        Collider[] colliders = Physics.OverlapSphere(colliderSpawnPosition, sphereRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                for (int i = 0; i < maxHitCount; i++)
                {
                    DealDamage(collider.gameObject);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (collider.CompareTag("Boss"))
            {
                for (int i = 0; i < maxHitCount; i++)
                {
                    DealDamage(collider.gameObject);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (collider.CompareTag("Minion"))
            {
                for (int i = 0; i < maxHitCount; i++)
                {
                    DealDamage(collider.gameObject);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            else if (collider.CompareTag("Minotaur"))
            {
                for (int i = 0; i < maxHitCount; i++)
                {
                    DealDamage(collider.gameObject);
                    yield return new WaitForSeconds(0.2f);
                }
            }

        }

        Destroy(summonedObject, 10f);
    }

    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownAfterActivation);
        isOnCooldown = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Object collided with enemy: " + other.gameObject.name);
            DealDamage(other.gameObject);
        }
    }

    private void DealDamage(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy with damage: " + damage);
            enemy.GetComponent<RhinoScript>().TakeDamage(damage);
        }
        else if (enemy.CompareTag("Boss"))
        {
            Debug.Log("Hit boss with damage: " + damage);
            enemy.GetComponent<BossScript>().TakeDamage(damage);
        }
        else if (enemy.CompareTag("Minion"))
        {
            Debug.Log("Hit minion with damage: " + damage);
            enemy.GetComponent<MinionScript>().TakeDamage(damage);
        }
        else if (enemy.CompareTag("Minotaur"))
        {
            Debug.Log("Hit mino with damage: " + damage);
            enemy.GetComponent<MinoScript>().TakeDamage(damage);
        }
    }
}
