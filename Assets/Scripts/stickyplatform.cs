using UnityEngine;

public class stickyplatform : MonoBehaviour
{
    private Vector3 lastPlatformPosition;
    private Transform currentPlatform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Truck"))
        {
            currentPlatform = collision.transform;
            lastPlatformPosition = currentPlatform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform == currentPlatform)
        {
            currentPlatform = null;
        }
    }

    private void FixedUpdate()
    {
        if (currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.position - lastPlatformPosition;
            transform.position += platformMovement;
            lastPlatformPosition = currentPlatform.position;
        }
    }
}
