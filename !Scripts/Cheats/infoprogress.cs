using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoprogress : MonoBehaviour
{
    public GameObject canvasObject;
    public GameObject objectPrefab;
    public Transform playerTransform;
    public int numberOfObjects = 10;
    public float spawnInterval = 0.1f;
    public float duration = 4f;
    public float radius = 30f;
    public float objectLifetime = 10f; 

    private List<GameObject> spawnedObjects = new List<GameObject>(); 

    public void infoprogresstpa()
    {
        canvasObject.SetActive(false);
        StartCoroutine(ShowCanvasForDuration(3f));
        StartCoroutine(Ulti());
    }

    IEnumerator ShowCanvasForDuration(float duration)
    {
        canvasObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        canvasObject.SetActive(false);
    }

    IEnumerator Ulti()
    {
        float timer = 0f;

        while (timer < duration)
        {
            for (int i = 0; i < numberOfObjects; i++)
            {
                Vector3 spawnPosition = playerTransform.position + Random.insideUnitSphere * radius;
                GameObject summonedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
                spawnedObjects.Add(summonedObject);
                Destroy(summonedObject, objectLifetime);

                Rigidbody rb = summonedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
                }

            }

            timer += spawnInterval;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
