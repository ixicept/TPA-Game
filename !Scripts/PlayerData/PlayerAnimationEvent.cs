using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public MovementScript movementScript;
    public MovementDash playerDash;
    public GameObject freeCamGameObject;
    public PlayerData playerData;
    private float originalBaseSpeed;
    public void DisableMovement()
    {
        originalBaseSpeed = playerData.baseSpeed;
        playerData.baseSpeed = 0;
        playerDash.enabled = false;
        freeCamGameObject.SetActive(false);
    }

    public void EnableMovement()
    {
        playerData.baseSpeed = originalBaseSpeed;
        playerDash.enabled = true;
        freeCamGameObject.SetActive(true);
    }

    public void startroll()
    {
        originalBaseSpeed = playerData.baseSpeed;
        playerData.baseSpeed = playerData.baseSpeed * playerData.rollSpeed;
    }

    public void endroll()
    {
        playerData.baseSpeed = originalBaseSpeed;
    }


    public void hardLandFalse()
    {
        movementScript.isHardLanding = false;
    }


}
