using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSceneScript : MonoBehaviour
{
    public MovementScript playerMovement;
    public GameObject freeCamGameObject;
    public LayerMask whatIsPlayer;
    public Vector3 boxHalfExtents = new Vector3(1.5f, 8f, 1.5f);
    public GameObject canvas;

    private bool isInTriggerArea = false;

    private void Start()
    {
        canvas.SetActive(false);
    }

    private void Update()
    {
        if (IsInsideArea(transform.position))
        {
            if (!isInTriggerArea)
            {
                isInTriggerArea = true;
                Debug.Log("Player entered trigger area.");
                playerMovement.enabled = false;
                freeCamGameObject.SetActive(false);
                canvas.SetActive(true);
            }
        }
        else
        {
            if (isInTriggerArea)
            {
                isInTriggerArea = false;
                Debug.Log("Player exited trigger area.");
                playerMovement.enabled = true;
                freeCamGameObject.SetActive(true);
                canvas.SetActive(false);
            }
        }
    }

    private bool IsInsideArea(Vector3 point)
    {
        return Physics.CheckBox(transform.position, boxHalfExtents, Quaternion.identity, whatIsPlayer);
    }
}
