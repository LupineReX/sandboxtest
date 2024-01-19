using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlook : MonoBehaviour
{
   [SerializeField] private float sensX;
   [SerializeField] private float sensY;

   Camera cam;

   float mouseX;
   float mousey;

   float mutiplier = 0.01f;

   float xRotation;
   float yRotation;

}
