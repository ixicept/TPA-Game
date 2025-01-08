using System;
using System.Collections;
using UnityEngine;

public class Skill1 : Skill
{
    public PlayerData playerData;
    public GameObject objectPrefab;
    public float spawnDistance;
    public float objectSpeed;
    public float objectLifetime;
    public Animator animator;
    public float damageMultiplier = 3f;
    private float damage;
    public float cooldownAfterActivation = 2.0f;
    public int maxHitCount = 4;
    private bool isOnCooldown = false;
    private Vector3 initialPosition; 

    public Skill1(string skillName, float skillCooldown) : base(skillName, skillCooldown)
    {
        name = "Slash";
        Cooldown = 5f;
    }

    public override void Activate()
    {
        base.Activate();
        animator.SetTrigger("slash");
        Debug.Log("Performing Skill 1 action...");
        StartCoroutine(SpawnPrefabsWithDelay());
  

    }
    private IEnumerator SpawnPrefabsWithDelay()
    {
        yield return new WaitForSeconds(1.2f);
        Vector3 characterPosition = transform.position;

        Vector3 spawnPosition = characterPosition + transform.forward * spawnDistance;
        spawnPosition.y = characterPosition.y + 1.5f;
        Vector3 directionToPlayer = (transform.position - spawnPosition).normalized;
        Quaternion spawnRotation = Quaternion.LookRotation(directionToPlayer);
        spawnRotation *= Quaternion.Euler(0, 180, 0);
        GameObject launchedObject = Instantiate(objectPrefab, spawnPosition, spawnRotation);
        launchedObject.transform.eulerAngles = new Vector3(0, spawnRotation.eulerAngles.y, 0);
        initialPosition = launchedObject.transform.position;
        Vector3 moveDirection = transform.forward;
        Rigidbody rb = launchedObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = launchedObject.AddComponent<Rigidbody>();
        }
        rb.velocity = moveDirection * objectSpeed;
  
        StartCoroutine(StartCooldown());
       
        damage = (playerData.attack * damageMultiplier);
        launchedObject.AddComponent<SkillHandler>().Setup(Convert.ToInt32(damage));

        StartCoroutine(DestroyAfterDistance(launchedObject)); 
    }

    private IEnumerator DestroyAfterDistance(GameObject obj)
    {
        while (Vector3.Distance(initialPosition, obj.transform.position) < objectLifetime)
        {
            yield return null;
        }
        Destroy(obj);
    }

    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownAfterActivation);
        isOnCooldown = false;
    }

}
