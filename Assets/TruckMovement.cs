using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    public float MovementSpeed;
    void Start()
    {
        MovementSpeed = Random.Range(MovementSpeed, MovementSpeed + 30f);
        this.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().drag = Random.Range(0.1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += this.transform.forward * Time.deltaTime * MovementSpeed;
    }
}
