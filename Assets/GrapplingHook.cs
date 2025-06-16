using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 GrapplePoint;
    public LayerMask grappleableLayer;
    public Transform Guntip, playercam, player;
    private float maxDistance = 15f; // Maximum distance for grappling
    private SpringJoint joint;

    private GameObject aimIndicator; // Blue sphere indicator
    private bool isGrappling = false;

    private void Awake()
    {
        Destroy(joint);
        lr = GetComponent<LineRenderer>();
        Debug.Log("Grappling Hook Initialized");
        if (lr == null)
        {
            Debug.LogError("LineRenderer component not found on the GameObject.");
        }

        // Create the blue sphere indicator
        aimIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        aimIndicator.transform.localScale = Vector3.one * 0.3f;
        aimIndicator.GetComponent<Renderer>().material.color = Color.blue;
        aimIndicator.SetActive(false); // Hide by default
        Destroy(aimIndicator.GetComponent<Collider>()); // Remove collider
    }

    void Update()
    {
        if (!isGrappling)
            UpdateAimIndicator();
        DrawRope();
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void UpdateAimIndicator()
    {
        RaycastHit hit;
        if (Physics.Raycast(playercam.position, playercam.forward, out hit, maxDistance, grappleableLayer))
        {
            aimIndicator.SetActive(true);
            aimIndicator.transform.position = hit.point;
        }
        else
        {
            aimIndicator.SetActive(false);
        }
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(playercam.position, playercam.forward, out hit, maxDistance, grappleableLayer))
        {
            GrapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = GrapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, GrapplePoint);

            //The distance grapple will try to keep from grapple point.
            joint.maxDistance = distanceFromPoint * 0.8f; // Adjust as needed
            joint.minDistance = distanceFromPoint * 0.25f; // Adjust as needed

            //change these values if needed
            joint.spring = 4.5f; // Adjust spring strength
            joint.damper = 7f; // Adjust damper for smoother movement
            joint.massScale = 4.5f; // Adjust mass scale for the joint

            lr.positionCount = 2;

            isGrappling = true;
            aimIndicator.SetActive(true);
            aimIndicator.transform.position = GrapplePoint; // Freeze at grapple point
        }
    }
    void DrawRope()
    {
        if (!joint) return;

        lr.SetPosition(0, Guntip.position);
        lr.SetPosition(1, GrapplePoint);
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        isGrappling = false;
        aimIndicator.SetActive(false);
    }
}