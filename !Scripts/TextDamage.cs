using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TextDamage : MonoBehaviour
{
    public static TextDamage current;
    public GameObject prefab;

    private void Awake()
    {
        current = this;
    }

    public void CreatePopUp(Vector3 position, float damageAmount)
    {
        GameObject popUp = Instantiate(prefab, position, Quaternion.identity);

        TextMeshProUGUI textMesh = popUp.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text = damageAmount.ToString();
        }

        float popUpDistance = 2f; 

        Vector3 directionToCamera = Camera.main.transform.position - position;
        directionToCamera.y = 0f; 

        Vector3 popUpPosition = position + directionToCamera.normalized * popUpDistance;
        float randomOffsetX = Random.Range(-1f, 1f);
        float randomOffsetZ = Random.Range(-1f, 1f);
        Vector3 randomOffset = new Vector3(randomOffsetX, 0f, randomOffsetZ);
        popUpPosition += randomOffset;

        popUp.transform.position = popUpPosition + Vector3.up * 2f;
        Destroy(popUp, 2f);
    }
    public void CreatePopUpString(Vector3 position, string text)
    {
        GameObject popUp = Instantiate(prefab, position, Quaternion.identity);

        TextMeshProUGUI textMesh = popUp.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text = text.ToString();
        }

        float popUpDistance = 2f;

        Vector3 directionToCamera = Camera.main.transform.position - position;
        directionToCamera.y = 0f;

        float randomOffsetX = Random.Range(-0.5f, 0.5f);
        float randomOffsetZ = Random.Range(-0.5f, 0.5f);
        Vector3 randomOffset = new Vector3(randomOffsetX, 0f, randomOffsetZ);

        Vector3 popUpPosition = position + directionToCamera.normalized * popUpDistance + randomOffset;

        popUpPosition.y += 2f;

        popUp.transform.position = popUpPosition;
        Destroy(popUp, 2f);
    }

}
