using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public float grappleSpeed = 10f;
    public float maxDistance = 20f;
    public LayerMask grappleableLayers;

    private Transform playerTransform;
    public bool isGrappling = false;
    private Vector3 grapplePoint;

    public LineRenderer grappleLineRenderer;

    private Camera playerCamera;
    private CharacterController controller;

    public void Initialize(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        playerCamera = playerTransform.GetComponentInChildren<Camera>();
        controller = playerTransform.GetComponent<CharacterController>(); // Get reference to CharacterController component
    }

    public void FireGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, maxDistance, grappleableLayers))
        {
            StartGrapple(hit.point);
        }
    }

    private void StartGrapple(Vector3 targetPoint)
    {
        grapplePoint = targetPoint;
        isGrappling = true;

        // Update the Line Renderer's positions
        UpdateGrappleVisual(playerTransform.position, grapplePoint);
    }

    public void ReleaseGrapple()
    {
        isGrappling = false;
        // Disable the Line Renderer
        grappleLineRenderer.enabled = false;
    }

    private void Update()
    {
        if (isGrappling)
        {
            // Calculate direction towards grapple point
            Vector3 grappleDir = (grapplePoint - playerTransform.position).normalized;
            float distanceToGrapplePoint = Vector3.Distance(playerTransform.position, grapplePoint);

            if (distanceToGrapplePoint > .1f)
            {
                // Move towards the grapple point
                Vector3 newPosition = playerTransform.position + grappleDir * grappleSpeed * Time.deltaTime;

                // Perform a movement check to ensure the new position is valid
                if (!Physics.Raycast(playerTransform.position, grappleDir, grappleSpeed * Time.deltaTime, grappleableLayers))
                {
                    controller.Move(grappleDir * grappleSpeed * Time.deltaTime);
                }

                // Update the Line Renderer's positions
                UpdateGrappleVisual(playerTransform.position, grapplePoint);
            }
            else
            {
                // If close to the grapple point, stop movement
                Vector3 newPosition = grapplePoint + grappleDir * 0.1f; // Move the player slightly away from the surface

                // Perform a movement check to ensure the new position is valid
                if (!Physics.Raycast(playerTransform.position, grappleDir, 0.1f, grappleableLayers))
                {
                    controller.Move(grappleDir * 0.1f);
                }

                // Update the Line Renderer's positions
                UpdateGrappleVisual(playerTransform.position, grapplePoint);
            }
        }
    }

    // Update the Line Renderer's positions
    private void UpdateGrappleVisual(Vector3 startPoint, Vector3 endPoint)
    {
        grappleLineRenderer.enabled = true;
        grappleLineRenderer.positionCount = 2; // Set the number of positions to 2 (start and end points)
        grappleLineRenderer.SetPosition(0, startPoint); // Set the start point
        grappleLineRenderer.SetPosition(1, endPoint); // Set the end point
    }
}
