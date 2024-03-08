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
   public float maxSlidetime;
   public float slideForce;
   private float slideTimer;
   
   public float slideYScale;
   private float startYScale;

   [Header("Input")]
   public KeyCode slideKey = KeyCode.C;
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
        if(Input.GetKeyUp(slideKey) && Sliding)
        {
            StopSlide();
        }
   }

   private void StartSlide()
   {
        sliding = true; 

        
   }
   private void SlidingMovement()
   {

   }

   private void StopSlide()
   {

   }
}
