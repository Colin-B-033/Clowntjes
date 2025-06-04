using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunfollow : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 positionOffset = new Vector3(0.3f, -0.2f, 0.5f); // Adjust as needed

    private void LateUpdate()
    {
        // Set rotation to match camera
        transform.rotation = cameraTransform.rotation;
        // Set position to camera position plus offset in camera's local space
        transform.position = cameraTransform.position + cameraTransform.rotation * positionOffset;
    }
}
