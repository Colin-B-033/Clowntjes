using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TruckMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    private float finalSpeed;

    private NavMeshAgent agent;
    public Transform destination;

    void Start()
    {
        // Get and configure NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        finalSpeed = Random.Range(movementSpeed, movementSpeed + 10f);
        agent.speed = finalSpeed;

        if (destination != null)
            agent.SetDestination(destination.position);
    }

    void Update()
    {
        if (agent == null) return;

        // Smoothly rotate the truck to match its movement direction
        Vector3 velocity = agent.velocity;
        if (velocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            agent.avoidancePriority = 70; // Set avoidance priority
        }
    }
}
