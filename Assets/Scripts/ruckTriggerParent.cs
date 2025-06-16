using UnityEngine;

public class TruckTriggerParent : MonoBehaviour
{
    public Transform player;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter: {other.transform.name}");
        if (other.transform == player)
        {
            Debug.Log("Player entered truck trigger, setting parent.");
            other.transform.SetParent(transform.parent); // parent is the truck
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"OnTriggerExit: {other.transform.name}");
        if (other.transform == player)
        {
            Debug.Log("Player exited truck trigger, removing parent.");
            other.transform.SetParent(null);
        }
    }
}
