using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class MovementScript : MonoBehaviour
{
    public PlayerData data;
    public SwordState swordState;
    public SwordScript swordScript;
    Animator animator;
    private bool readyToJump;
    public bool isWalking = false;
    public bool isRunning = true;
    public bool isCombat = false;
    public bool isJumping = false;
    public bool isMoving = false;
    public bool isSword = false;
    public bool isHardLanding;
    public bool grounded;
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode toggleWalk = KeyCode.LeftControl;
    public KeyCode toggleCombat = KeyCode.R;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;

    private int jumpCount;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    PlayerInput input;
    InputAction action;
    public PlayerInput.NormalActions normalState;

    public void OnEnable()
    {
        input = new PlayerInput();
        normalState = input.Normal;
        normalState.Enable();
        normalState.WalkToggle.performed += ToggleWalking;
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        swordState = GetComponent<SwordState>();
        swordScript = GetComponent<SwordScript>();
        rb.freezeRotation = true;
        readyToJump = true;
        jumpCount = 0;
        data.baseSpeed = 5f;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight - 1f , whatIsGround);

        MyInput();
        SpeedControl();
        if (grounded)
            rb.drag = data.groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = normalState.Move.ReadValue<Vector2>().x;
        verticalInput = normalState.Jump.ReadValue<float>();

        if (normalState.CombatMode.triggered && grounded)
        {
            ToggleCombat();
            Debug.Log("combat state changed");
        }
        //double jump
        /*        if (Input.GetKeyDown(jumpKey) && !readyToJump && jumpCount < 1)
                {
                    Jump();
                    jumpCount++;
                }*/

        if (normalState.Jump.triggered && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), data.jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        float forwardInput = normalState.Move.ReadValue<Vector2>().y;
        float sidewaysInput = normalState.Move.ReadValue<Vector2>().x; 

        Vector3 forwardMovement = orientation.forward * forwardInput;
        Vector3 lateralMovement = orientation.right * sidewaysInput; 
        moveDirection = forwardMovement + lateralMovement;
        moveDirection *= isWalking ? data.baseSpeed * data.walkSpeed : data.baseSpeed * data.runSpeed;
        rb.AddForce(moveDirection.normalized * data.baseSpeed * 15f, ForceMode.Force);
        if (!isWalking)
        {
            if (isCombat)
            {
                rb.AddForce(data.baseSpeed * data.combatSpeed * data.walkSpeed * moveDirection.normalized, ForceMode.Force);
                Debug.Log("gacor");
            }
            else
            {
                rb.AddForce(data.baseSpeed * data.runSpeed * data.walkSpeed * moveDirection.normalized, ForceMode.Force);
                Debug.Log("gacor2");
            }
        }
        else
        {
            if (isCombat)
            {
                rb.AddForce(data.baseSpeed * data.combatSpeed * moveDirection.normalized, ForceMode.Force);
                Debug.Log("gacor3");
            }
            else
            {
                rb.AddForce(data.baseSpeed * data.runSpeed * moveDirection.normalized, ForceMode.Force);
                Debug.Log("gacor4");
            }
        }
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * data.baseSpeed * 15f, ForceMode.Force);
        }
        else if (!grounded)
        {
            Debug.Log("not ground");
            rb.AddForce(moveDirection.normalized * data.baseSpeed * 15f * data.airMultiplier, ForceMode.Force);
        }
        Vector3 finalForce = moveDirection.normalized * data.baseSpeed * 10f;
        rb.AddForce(grounded ? finalForce : finalForce * data.airMultiplier, ForceMode.Force);
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 7);
        }
        isMoving = moveDirection.magnitude > 0.1f;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        float speed = isWalking ? data.baseSpeed * data.walkSpeed : data.baseSpeed * data.runSpeed;

        if (isCombat)
        {
            speed *= data.combatSpeed;
        }

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        Vector3 currentVel = rb.velocity;

        float maxYVelocity = 20f; 
        currentVel.y = Mathf.Clamp(currentVel.y, -maxYVelocity, maxYVelocity);

        rb.velocity = currentVel;

    }

    private void Jump()
    {
/*        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);*/
        rb.AddForce(transform.up * data.jumpForce, ForceMode.Impulse);
        isJumping = !isJumping;
    }

    private void ResetJump()
    {
        readyToJump = true;
        jumpCount = 0;
        isJumping = !isJumping;
    }

    private void ToggleWalking(InputAction.CallbackContext ctx)
    {
        isWalking = !isWalking;
        isRunning = !isRunning;
    }

    private void ToggleCombat()
    {
        isCombat = !isCombat;
        animator.SetTrigger("isSword");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.y > data.hardLandTreshHold)
        {
            isHardLanding = true;
        }
    }
}
