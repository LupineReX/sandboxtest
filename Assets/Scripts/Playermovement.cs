using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
   [Header("References")]
   public Climbing climbingScript;
   float playerHeight = 2f;
  
   [SerializeField] Transform orientation;
   [Header("Movement")]
   public float moveSpeed = 6f;
   float movementMultiplier = 10f;

   [Header("Sprinting")]
   [SerializeField] float walkSpeed = 4f;
   [SerializeField] float sprintSpeed = 6f;
   [SerializeField] float acceleration = 10f;


   [Header("Crouching")]
   public float crouchSpeed;
   public float crouchYScale;
   private float startYScale;
   bool isCrouched;
   [SerializeField] float airMultiplier = 0.4f;
   [Header("Jumping")]
   public float jumpForce = 5f;

   [Header("keybinds")]
   [SerializeField] KeyCode jumpKey = KeyCode.Space;
   [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
   [SerializeField] KeyCode crouchKey = KeyCode.C;
   [Header("Drag")]
   [SerializeField] float groundDrag = 6f;
   [SerializeField] float airDrag = 2f;

   float horziontalMovement;
   float verticalMovement;

   [Header("Ground Detection")]
   [SerializeField] Transform groundCheck;
   [SerializeField] LayerMask groundMask;
   public bool isGrounded;
   float groundDistance = 0.4f;
   Vector3 moveDirection;
   Vector3 slopeMoveDirection;

   Rigidbody rb;
   
   public bool sliding;
   RaycastHit slopeHit;
   private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

   
   private void Start()
   {
       rb = GetComponent<Rigidbody>();
       rb.freezeRotation = true;

       startYScale = transform.localScale.y;
   }

   private void Update()
   {
       isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
       MyInput();
       ControlDrag();
       ControlSpeed();
       if (Input.GetKeyDown(jumpKey) && isGrounded)
       {
            Jump();
       }

       slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
   }

   void Jump()
   {
        rb.velocity = new Vector3(rb.velocity.x , 0 , rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
   }

   void MyInput()
   {
        horziontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horziontalMovement;
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down*5f, ForceMode.Impulse);
            isCrouched = true;
        }
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            isCrouched = false;
        }
        if (isCrouched)
        {
            moveSpeed = crouchSpeed;
        }
   }
   void ControlSpeed()
   {
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration* Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
        
   }
   void ControlDrag()
   {
      if (isGrounded)
      {  
         rb.drag = groundDrag;
      }
      else
      {
        rb.drag = airDrag;
      }
   }

   private void FixedUpdate()
   {
       MovePlayer();
   }

   void MovePlayer()
   {
        if (climbingScript.exitingWall) return;
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
   }

}

