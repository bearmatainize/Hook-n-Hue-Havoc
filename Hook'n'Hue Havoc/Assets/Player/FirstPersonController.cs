using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public Transform cameraTransform;

    public float sensitivity = 12f;
    public float moveSpeed = 3f;
    public float gravity = 1.0f; // Gravity acceleration

    private float xRotation = 0f;
    private CharacterController controller;
    private GrappleHook grappleHook;

    public float xInput;
    public float zInput;
    private float yVelocity = 0f; // Vertical velocity

    public float jumpForce = 1.5f;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    private bool isJumping;

    public float sprintSpeed = 6f;
    private bool isSprinting = false;
    private bool isSprintReleased = false;
    private float sprintCooldown = 5.0f;
    private float sprintTimer = 0.0f;
    private float sprintDuration = 0.0f;
    private float maxSprintDuration = 3.0f;

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
        
        /*// print the sprint timer
        Debug.Log(sprintTimer);
        
        // Update the sprint timer
        if (!isSprinting)
        {
            sprintTimer += Time.deltaTime;
        }
        
        // Update the sprint duration
        if (isSprinting)
        {
            sprintDuration += Time.deltaTime;
            if (sprintDuration >= maxSprintDuration)
            {
                isSprinting = false;
                isSprintReleased = true;
            }
        }

        // Check if the player is sprinting
        if (Input.GetKey(KeyCode.LeftShift) && sprintTimer >= sprintCooldown)
        {
            isSprinting = true;
            isSprintReleased = false;
        }
        else
        {
            isSprinting = false;
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isSprintReleased = true;
            }
        }

        if (isSprintReleased)
        {
            sprintTimer = 0.0f; // Reset the timer
            sprintDuration = 0.0f; // Reset the duration
            isSprintReleased = false;
        }*/
        
        // Check if the player is sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
        
        // Movement
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xInput + transform.forward * zInput;
        move.Normalize();
        
        // Use sprintSpeed if the player is sprinting, otherwise use moveSpeed
        float speed = isSprinting ? sprintSpeed : moveSpeed;
        
        // Apply gravity
        if (yVelocity < 0)
        {
            // Apply higher gravity when falling
            yVelocity -= gravity * 2.5f * Time.deltaTime;
        } else if (yVelocity > 0 && !Input.GetButton("Jump"))
        {
            // Apply lower gravity when jump button is released
            yVelocity -= gravity * 1.5f * Time.deltaTime;
        } else
        {
            yVelocity -= gravity * Time.deltaTime;
        }

        // Combine vertical movement with gravity
        Vector3 verticalMovement = Vector3.up * yVelocity * Time.deltaTime;

        // Move the player using CharacterController
        controller.Move(move * speed * Time.deltaTime + verticalMovement);
        
        // Check for jump input
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            yVelocity = jumpForce;
        }
        
        // Apply jump force
        if (isJumping)
        {
            yVelocity = jumpForce;
            isJumping = false;
        }
        
    }
    
    public void Jump()
    {
        isJumping = true;
    }
    
    public bool IsGrounded()
    {
        return controller.isGrounded;
    }
}



