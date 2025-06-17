using UnityEngine;

public class Parent : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] Transform platform;

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
