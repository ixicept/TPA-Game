using System.Collections;
using UnityEngine;

public class MovementDash : MonoBehaviour
{
    public PlayerData data;
    public MovementScript movementScript;
    public Animator animator;
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    public Rigidbody rb;

    private bool useCameraForward = true;
    private bool allowAllDirections = true;


    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftShift;

    private bool canDash = true;
    private float lastDashTime; 

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementScript = GetComponent<MovementScript>();
        data.dashCount = 0;
    }

    public void Update()
    {
        if (Input.GetKeyDown(dashKey) && canDash)
        {
            Dash();
        }

        if (canDash && Time.time - lastDashTime > 1f)
        {
            data.dashCount = 0;
            canDash = true;
        }
    }

    private void Dash()
    {
        if (data.dashCount < 2)
        {
            Vector3 direction = GetDirection(useCameraForward ? playerCam : orientation);
            Vector3 forceToApply = direction *data.baseSpeed * data.dashForce * data.dashSpeedModifier;
            Debug.Log("dashforce"+  forceToApply);
            rb.AddForce(forceToApply, ForceMode.Impulse);
            animator.SetTrigger("isDash");
            data.dashCount++;

            if (data.dashCount == 2)
            {
                StartCooldown();
            }

            lastDashTime = Time.time;
        }
    }


    private void StartCooldown()
    {
        canDash = false; 
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(data.dashCd);
        data.dashCount = 0;
        canDash = true;
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            direction = forwardT.forward;

        if (verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;
    }
}
