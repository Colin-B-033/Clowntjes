using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Softrotate : MonoBehaviour
{
    public Transform frontTransform;  // Assign the front part's transform
    public Transform rearTransform;   // Assign the trailer (rear)
    public float rotationSmoothSpeed = 2f;

    void LateUpdate()
    {
        // Match rotation Y (yaw) only
        Vector3 forwardDir = frontTransform.forward;
        forwardDir.y = 0; // Ensure flat rotation

        if (forwardDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(forwardDir);
            rearTransform.rotation = Quaternion.Slerp(rearTransform.rotation, targetRot, Time.deltaTime * rotationSmoothSpeed);
        }
    }
}
