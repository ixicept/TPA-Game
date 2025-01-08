using System.Collections;
using UnityEngine;

public class Skill3 : Skill
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

    public Skill3(string skillName, float skillCooldown) : base(skillName, skillCooldown)
    {
        name = "METEOR SHOWER";
        Cooldown = 20f;
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
        animator.SetTrigger("mshower");
        Debug.Log("Performing Skill 3 action...");

        StartCoroutine(SpawnPrefabsWithDelay());


    }

    private IEnumerator SpawnPrefabsWithDelay()
    {
        yield return new WaitForSeconds(1.2f);
        Vector3 spawnPosition = transform.position + transform.forward * 5f;

        GameObject summonedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

        StartCoroutine(StartCooldown());
        Collider[] colliders = Physics.OverlapSphere(spawnPosition, sphereRadius);

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
        Destroy(summonedObject, 2f);
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
