using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private Playermovement pm;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;
    public float maxSlideForce;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.V;
    private float horizontalInput;
    private float verticalInput;

    private bool sliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<Playermovement>();

        startYScale = playerObj.localScale.y;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Check if the player is sprinting in addition to sliding input
        if (Input.GetKeyDown(slideKey) && pm.isSprinting && (horizontalInput != 0 || verticalInput != 0))
        {
            StartSlide();
        }
        if (Input.GetKeyUp(slideKey) && sliding)
        {
            StopSlide();
        }
    }

    private void FixedUpdate()
    {
        if (sliding)
        {
            SlidingMovement();
        }
    }

    private void StartSlide()
    {
        sliding = true;
        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {
        // Calculate input direction based on player orientation
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Add force to the player in the input direction
        rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

        // Check if the player is on a slope
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.0f))
        {
            // Calculate the angle of the slope
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            // If the angle is greater than 45 degrees, increase the slide force
            if (slopeAngle > 45f)
            {
                // Gradually increase slideForce based on slope angle
                slideForce += slopeAngle * Time.deltaTime;

                // Cap the maximum slide force to prevent excessive acceleration
                slideForce = Mathf.Min(slideForce, maxSlideForce);
            }
        }

        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0)
            StopSlide();
    }

    private void StopSlide()
    {
        sliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }
}

