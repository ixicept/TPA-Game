using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallMovement : MonoBehaviour
{
    public float hardLandingThreshold = 3f;
    public KeyCode inputKey = KeyCode.Space; 
    public Transform playerTransform;
    private Vector3 lastPosition;
    private bool isGrounded = true; 
    private bool hasHardLanded = false; 

    void Start()
    {
        lastPosition = playerTransform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("ground");
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("in air");
            isGrounded = false;
            lastPosition = playerTransform.position;
            hasHardLanded = false;
        }
    }

    void Update()
    {
        float verticalVelocity = (playerTransform.position.y - lastPosition.y) / Time.deltaTime;

        if (!isGrounded && verticalVelocity < -0.1f)
        {
            Debug.Log("Player fell from a height!");
        }
        else if (!hasHardLanded && !isGrounded && verticalVelocity < -0.1f)
        {
            Debug.Log("Player experienced a hard landing!");
            hasHardLanded = true;
        }
        else if (hasHardLanded && Input.GetKeyDown(inputKey))
        {
            Debug.Log("Player performed an action after a hard landing!");
        }

        lastPosition = playerTransform.position;
    }
}
