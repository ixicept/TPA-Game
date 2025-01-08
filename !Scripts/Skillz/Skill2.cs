using System;
using System.Collections;
using UnityEngine;

public class Skill2 : Skill
{
    public GameObject objectPrefab;
    public Animator animator;
    public PlayerData playerData;
    public float damageMultiplier = 3f;
    private float damage;
    public float cooldownAfterActivation = 2.0f;
    public int maxHitCount = 4;
    public float sphereRadius = 5f;

    private bool isOnCooldown = false;

    public Skill2(string skillName, float skillCooldown) : base(skillName, skillCooldown)
    {
        name = "ROCK SMASH";
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
        animator.SetTrigger("meteor");
        Debug.Log("Performing Skill 2 action...");

        StartCoroutine(SpawnPrefabsWithDelay());
    }

    private IEnumerator SpawnPrefabsWithDelay()
    {
        yield return new WaitForSeconds(1.0f);
        Vector3 spawnPosition = transform.position + transform.forward * 5f;

   
        GameObject summonedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(0.8f);

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

        StartCoroutine(StartCooldown());
        Destroy(summonedObject, 1f);
    }

    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownAfterActivation);
        isOnCooldown = false;
    }

    private void DealDamage(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy with damage: " + playerData.attack * damageMultiplier);
            enemy.GetComponent<RhinoScript>().TakeDamage(playerData.attack * damageMultiplier);
        }
        else if (enemy.CompareTag("Boss"))
        {
            enemy.GetComponent<BossScript>().TakeDamage(playerData.attack * damageMultiplier);
        }
        else if (enemy.CompareTag("Minion"))
        {
            enemy.GetComponent<MinionScript>().TakeDamage(playerData.attack * damageMultiplier);
        }
        else if (enemy.CompareTag("Minotaur"))
        {
            enemy.GetComponent<MinoScript>().TakeDamage(playerData.attack * damageMultiplier);
        }
    }
}
