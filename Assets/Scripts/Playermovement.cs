using System.Collections;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private float currentSpeed;

    [Header("Slow Motion")]
    public KeyCode slowMoKey = KeyCode.E;
    private float slowMoTimeScale = 0.3f;
    private float slowMoTransitionSpeed = 2f;
    private Coroutine slowMoCoroutine;
    private bool isSlowMo = false;
    private float maxSlowMoAmount = 5f;
    private float slowMoDepleteRate = 1f;
    private float regenRate = 0.5f;
    private float slowMoAmount;

    public SlowUIController slowMoUI;

    public Transform playerCamera;

    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintMultiplier = 2f;

    [Header("Ground Check")]
    public float playerHeight;
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    // Platform tracking
    private Vector3 platformVelocity = Vector3.zero;
    private Rigidbody currentPlatformRb = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        slowMoAmount = maxSlowMoAmount;
    }

    private void Update()
    {
        float rayLength = playerHeight * 0.5f + 0.2f;
        Vector3 checkPosition = transform.position + Vector3.down * (rayLength - groundCheckRadius);
        grounded = Physics.CheckSphere(checkPosition, groundCheckRadius, whatIsGround);

        MyInput();
        SpeedControl();

        rb.drag = grounded ? groundDrag : 0f;

        // Slow motion input and control
        if (Input.GetKeyDown(slowMoKey))
        {
            if (!isSlowMo && slowMoAmount > 0f)
            {
                isSlowMo = true;
                if (slowMoCoroutine != null) StopCoroutine(slowMoCoroutine);
                slowMoCoroutine = StartCoroutine(SmoothTimeScaleTransition(slowMoTimeScale));
            }
            else if (isSlowMo)
            {
                isSlowMo = false;
                if (slowMoCoroutine != null) StopCoroutine(slowMoCoroutine);
                slowMoCoroutine = StartCoroutine(SmoothTimeScaleTransition(1f));
            }
        }

        if (isSlowMo)
        {
            slowMoAmount -= slowMoDepleteRate * Time.unscaledDeltaTime;
            if (slowMoAmount <= 0f)
            {
                slowMoAmount = 0f;
                isSlowMo = false;
                if (slowMoCoroutine != null) StopCoroutine(slowMoCoroutine);
                slowMoCoroutine = StartCoroutine(SmoothTimeScaleTransition(1f));
            }
        }
        else if (slowMoAmount < maxSlowMoAmount)
        {
            slowMoAmount = Mathf.Min(slowMoAmount + regenRate * Time.unscaledDeltaTime, maxSlowMoAmount);
        }

        if (slowMoUI != null)
        {
            slowMoUI.UpdateSlider(slowMoAmount, maxSlowMoAmount, isSlowMo);
            slowMoUI.SetGradientTarget(isSlowMo);
        }
    }

    private void FixedUpdate()
    {
        UpdatePlatformVelocity();
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        currentSpeed = moveSpeed;
        if (Input.GetKey(sprintKey)) currentSpeed *= sprintMultiplier;

        float forceMultiplier = grounded ? 1f : airMultiplier;
        rb.AddForce(moveDirection.normalized * currentSpeed * 10f * forceMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Vector3 flatPlatformVel = new Vector3(platformVelocity.x, 0f, platformVelocity.z);

        // Player-only velocity (subtract platform's)
        Vector3 playerOnlyVel = flatVel - flatPlatformVel;

        if (playerOnlyVel.magnitude > currentSpeed)
        {
            Vector3 limitedPlayerVel = playerOnlyVel.normalized * currentSpeed;
            Vector3 newFlatVel = flatPlatformVel + limitedPlayerVel;
            rb.velocity = new Vector3(newFlatVel.x, rb.velocity.y, newFlatVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Add horizontal platform momentum
        if (platformVelocity != Vector3.zero)
        {
            Vector3 horizontalPlatformVel = new Vector3(platformVelocity.x, 0f, platformVelocity.z);
            rb.AddForce(horizontalPlatformVel, ForceMode.VelocityChange);
        }
    }

    private void ResetJump() => readyToJump = true;

    private void UpdatePlatformVelocity()
    {
        if (grounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight * 0.5f + 0.3f))
            {
                Rigidbody hitRb = hit.collider.attachedRigidbody;

                if (hitRb != null && !hitRb.isKinematic)
                {
                    currentPlatformRb = hitRb;
                    platformVelocity = hitRb.velocity;
                    return;
                }
            }
        }

        // If not grounded or platform has no rigidbody
        currentPlatformRb = null;
        platformVelocity = Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        float rayLength = playerHeight * 0.5f + 0.2f;
        Vector3 checkPosition = transform.position + Vector3.down * (rayLength - groundCheckRadius);
        Gizmos.color = grounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(checkPosition, groundCheckRadius);
    }

    private IEnumerator SmoothTimeScaleTransition(float targetScale)
    {
        float startScale = Time.timeScale;
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.unscaledDeltaTime * slowMoTransitionSpeed;
            Time.timeScale = Mathf.Lerp(startScale, targetScale, progress);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            yield return null;
        }

        Time.timeScale = targetScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
