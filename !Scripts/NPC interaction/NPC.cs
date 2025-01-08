using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCData npcData;
    public Transform playerTransform;
    public MovementScript movementScript;
    private bool isCombat;

    private void Update()
    {
        isCombat = movementScript.isCombat;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= npcData.lookDistanceThreshold)
        {
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * npcData.rotationSpeed);
        }


        if (distanceToPlayer <= npcData.interactionDistance && !isCombat)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWithPlayer();
            }
        }
        else
        {
            Debug.Log("In Combat");
        }
    }

    public virtual void InteractWithPlayer()
    {
        Debug.Log("NPC is interacting with the player.");
    }
}
