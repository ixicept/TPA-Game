using Unity.VisualScripting;
using UnityEngine;

public class DungeonNPC : NPC
{
    public MovementScript playerMovement;
    public GameObject freeCamGameObject;
    public GameObject canvas;

    public override void InteractWithPlayer()
    {
        base.InteractWithPlayer();
        playerMovement.enabled = false;
        freeCamGameObject.SetActive(false);
        canvas.SetActive(true);
    }
}
