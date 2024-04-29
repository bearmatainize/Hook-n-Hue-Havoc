using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawnerController : MonoBehaviour
{
    public BulletSpawner bulletSpawner;
    public PlayerInput playerInput;
    public Camera playerCamera; // Reference to the player's camera

    public float fireCooldown = 0.5f; // Cooldown time in seconds
    private float lastFireTime; // Time when the last bullet was fired

    void Update()
    {
        // Check if enough time has passed since the last bullet was fired
        if (Time.time - lastFireTime >= fireCooldown)
        {
            // Check for input to spawn a bullet
            if (playerInput.actions["Fire"].IsPressed())
            {
                SpawnBullet();
                lastFireTime = Time.time; // Update the last fire time
            }
        }
    }

    void SpawnBullet()
    {
        // Calculate the direction of the bullet based on the camera's forward direction
        Vector3 spawnDirection = playerCamera.transform.forward;

        bulletSpawner.SpawnBullet(spawnDirection, true);
    }
}
