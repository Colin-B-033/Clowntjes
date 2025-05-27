using UnityEngine;



public class TruckController : MonoBehaviour
{

    public float speed = 10f;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        // Increase speed over time
        speed = 10f + Time.timeSinceLevelLoad; // 10f is the base speed
        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
    }
}