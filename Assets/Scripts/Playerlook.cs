using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlook : MonoBehaviour
{  
   [Header("References")]
   [SerializeField] WallRunning wallRun;
   [SerializeField] private float sensX = 2;
   [SerializeField] private float sensY = 2;

   [SerializeField] Transform cam;
   [SerializeField] Transform orientation;

   float mouseX;
   float mouseY;

   float mutiplier = 0.01f;

   float xRotation;
   float yRotation;
   
   private void Start()
   {

        Cursor.lockState = CursorLockMode.Locked;
   }

   private void Update()
   {
      MyInput();

      cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, wallRun.tilt);
      orientation.transform.rotation = Quaternion.Euler(0,yRotation, 0);
   }

   void MyInput()
   {
        mouseX = Input.GetAxisRaw("Mouse X"); 
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * mutiplier;
        xRotation -= mouseY * sensY * mutiplier;

        xRotation  = Mathf.Clamp(xRotation, -90f , 90f);
   }

}
