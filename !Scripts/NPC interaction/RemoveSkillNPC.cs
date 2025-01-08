using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveSkillNPC : NPC
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
