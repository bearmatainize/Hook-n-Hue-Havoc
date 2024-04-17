using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public Transform cameraTransform;

    public float sensitivity = 12f;
    public float moveSpeed = 6f;
    public float gravity = 9.81f; // Gravity acceleration

    private float xRotation = 0f;
    private CharacterController controller;
    private GrappleHook grappleHook;

    public float xInput;
    public float zInput;
    private float yVelocity = 0f; // Vertical velocity

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controller = GetComponent<CharacterController>(); // Get reference to CharacterController component
        grappleHook = GetComponent<GrappleHook>();
    }

    public void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

         // Check if the player is grappling
        if (grappleHook.isGrappling)
        {
            return; // Exit early to prevent normal movement
        }

        // Movement
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xInput + transform.forward * zInput;
        move.Normalize();

        // Apply gravity
        yVelocity -= gravity * Time.deltaTime;

        // Combine vertical movement with gravity
        Vector3 verticalMovement = Vector3.up * yVelocity * Time.deltaTime;

        // Move the player using CharacterController
        controller.Move(move * moveSpeed * Time.deltaTime + verticalMovement);
    }
}

