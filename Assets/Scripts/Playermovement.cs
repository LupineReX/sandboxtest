using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
   [Header("Movement")]
   public float moveSpeed = 6f;
   float movementMultiplier = 10f;
   float rbDrag = 6f;

   float horziontalMovement;
   float verticalMovement;

   Vector3 moveDirection;

   Rigidbody rb;

   private void Start()
   {
       rb = GetComponent<Rigidbody>();
       rb.freezeRotation = true;
   }

   private void Update()
   {
       MyInput();
       ControlDrag();
   }

   void MyInput()
   {
        horziontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horziontalMovement;
   }

   void ControlDrag()
   {
      rb.drag = rbDrag;
   }

   private void FixedUpdate()
   {
       MovePlayer();
   }

   void MovePlayer()
   {
        rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
   }

}
