using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public float grappleSpeed = 10f;
    public float maxDistance = 20f;
    public LayerMask grappleableLayers;

    private Transform playerTransform;
    private bool isGrappling = false;
    private Vector3 grapplePoint;

    /*public LineRenderer grappleLineRenderer;

    // Call this method to update the Line Renderer's positions
    public void UpdateGrappleVisual(Vector3 startPoint, Vector3 endPoint)
    {
        grappleLineRenderer.positionCount = 2; // Set the number of positions to 2 (start and end points)
        grappleLineRenderer.SetPosition(0, startPoint); // Set the start point
        grappleLineRenderer.SetPosition(1, endPoint); // Set the end point
    }*/

    public void Initialize(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    public void FireGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, playerTransform.forward, out hit, maxDistance, grappleableLayers))
        {
            StartGrapple(hit.point);
        }
    }

    private void StartGrapple(Vector3 targetPoint)
    {
        grapplePoint = targetPoint;
        isGrappling = true;
    }

    public void ReleaseGrapple()
    {
        isGrappling = false;
    }

    /*public void Reset()
    {
        isGrappling = false;
    }*/

    private void Update()
    {
        if (isGrappling)
        {
            // Calculate direction towards grapple point
            Vector3 grappleDir = (grapplePoint - playerTransform.position).normalized;
            float distanceToGrapplePoint = Vector3.Distance(playerTransform.position, grapplePoint);

            //UpdateGrappleVisual(playerTransform.position, grapplePoint);

            if (distanceToGrapplePoint > .1f)
            {
                // Move towards the grapple point
                playerTransform.position += grappleDir * grappleSpeed * Time.deltaTime;
            }
            else
            {
                // If close to the grapple point, stop movement
                playerTransform.position = grapplePoint;
            }
        }
    }
}
