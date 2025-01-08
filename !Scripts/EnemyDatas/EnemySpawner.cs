using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 20;
    public int currentEnemies = 0;

    public float minXPos = 614f;
    public float maxXPos = 705f;
    public float minZPos = 535f;
    public float maxZPos = 678f;
    public float yPos;
    public float respawnDelay = 6f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            yield return null;
        }
    }

    void SpawnEnemy()
    {
        if (currentEnemies < maxEnemies)
        {
            currentEnemies++;
            Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        }
    }

    public void EnemyDied()
    {
        currentEnemies--;

        if (currentEnemies < maxEnemies)
        {
            StartCoroutine(RespawnAfterDelay());
        }
    }
    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnEnemy();
    }

    Vector3 GetRandomSpawnPosition()
    {
        float xPos = Random.Range(minXPos, maxXPos);
        float zPos = Random.Range(minZPos, maxZPos);
        return new Vector3(xPos, yPos, zPos);
    }
}
