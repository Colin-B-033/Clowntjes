using UnityEngine;

public class Parent : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] Transform platform;
    [SerializeField] float platformMoveSpeed = 2f; // Speed for platform movement

    private void Update()
    {
        if (platform != null)
        {
            // Move the platform forward in its local Z direction
            platform.position += platform.right * platformMoveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTag))
        {
            other.gameObject.transform.parent = platform;
            Debug.Log("Player parented to platform: " + platform.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTag))
        {
            other.gameObject.transform.parent = null;
        }
    }
}
