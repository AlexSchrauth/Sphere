using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    [Header("Dash Settings")]
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public KeyCode dashKey = KeyCode.E;

    private bool isDashing = false;
    private float dashTimer = 0f;
    private Vector3 dashDirection;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Dash input needs to be checked in Update so it never misses a keypress
        if (Input.GetKeyDown(dashKey) && dashTimer <= 0 && !isDashing)
        {
            StartCoroutine(PerformDash());
        }

        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // If we are dashing, let the dash coroutine control the physics velocity completely
        if (isDashing)
        {
            rigidbody.linearVelocity = dashDirection * dashSpeed;
            return; 
        }

        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.y);
    }

    IEnumerator PerformDash()
    {
        isDashing = true;
        dashTimer = dashCooldown;

        // Determine dash direction based on player input. 
        // If holding keys, dash that way. If stationary, dash forward.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        Vector3 localDir = new Vector3(h, 0, v).normalized;
        
        if (localDir == Vector3.zero)
        {
            dashDirection = transform.forward; // Dash forward if not pressing WASD
        }
        else
        {
            dashDirection = transform.rotation * localDir; // Dash in the direction of WASD keys
        }

        // Keep the dash moving cleanly over the duration
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            yield return new WaitForFixedUpdate();
        }

        // Reset velocity back to 0 at the end of the dash so you don't drift forever
        rigidbody.linearVelocity = new Vector3(0, rigidbody.linearVelocity.y, 0);
        isDashing = false;
    }
}