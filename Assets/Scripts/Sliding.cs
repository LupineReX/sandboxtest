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

   [Header("sliding")]
   public float maxSlideTime;
   public float slideForce;
   private float slideTimer;
   
   public float slideYScale;
   private float startYScale;

   [Header("Input")]
   public KeyCode slideKey = KeyCode.V;
   private float horziontalInput;
   private float verticalInput;

   private bool sliding;

   private void start()
   {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<Playermovement>();

        startYScale = playerObj.localScale.y;
   }

   private void Update()
   {
        horziontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input .GetAxisRaw("Vertical");

        if(Input.GetKeyDown(slideKey) && (horziontalInput != 0 || verticalInput != 0))
        {
            StartSlide();
        }
        if(Input.GetKeyUp(slideKey) && sliding)
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
          Vector3 inputDirection = orientation.forward * verticalInput + orientation.right *horziontalInput;

          rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force); 

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
