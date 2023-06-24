using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
     public Vector3 rotation;

     public float MaxRot = 20f;
     public float MinRot = -20f;

   public void RotateUpDown(float angle)
   {
     
    //   Vector3 rotationPre = transform.rotation.eulerAngles;

     Vector3 newRotation = new Vector3(

          transform.rotation.eulerAngles.x + angle,
          transform.rotation.eulerAngles.y,
          transform.rotation.eulerAngles.z
     );

     newRotation.x = newRotation.x > 180f ? newRotation.x - 360f : newRotation.x;

     newRotation.x = Mathf.Clamp(newRotation.x , MinRot , MaxRot);

     transform.rotation =  Quaternion.Euler(newRotation);

    // rotation = transform.rotation.eulerAngles;
     
 

   }

}
