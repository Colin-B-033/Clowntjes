
using System.Collections.Generic;
using UnityEngine;

public class TruckMovementnonav : MonoBehaviour
{
    public float MovementSpeed;
    private float finalSpeed;
    public Rigidbody rb;

    void Start()
    {
        finalSpeed = Random.Range(MovementSpeed, MovementSpeed + 2f);

        // Optionally randomize drag if Rigidbody still exists
        Rigidbody rb = transform.GetChild(0).GetComponent<Rigidbody>();
        if (rb != null) rb.drag = Random.Range(0.1f, 1f);
    }

    void Update()
    {
        // Move using transform.position and local rotation
        transform.position += rb.transform.rotation * Vector3.forward * finalSpeed * Time.deltaTime;
    }
}