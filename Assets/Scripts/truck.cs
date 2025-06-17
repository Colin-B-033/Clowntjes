using UnityEngine;

public class TruckController : MonoBehaviour
{
    public float speed = 10f;

    void FixedUpdate()
    {
        // Increase speed over time
        speed = 10f + Time.timeSinceLevelLoad; // 10f is the base speed
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }
}
