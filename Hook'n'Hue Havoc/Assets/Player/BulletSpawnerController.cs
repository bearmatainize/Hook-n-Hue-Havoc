using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnerController : MonoBehaviour
{
    public BulletSpawner bulletSpawner;

    void Update()
    {
        // Check for input to spawn a bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        // Check if the bullet spawner is assigned
        if (bulletSpawner != null)
        {
            bulletSpawner.SpawnBullet();
        }
        else
        {
            Debug.LogWarning("Bullet spawner is not assigned.");
        }
    }
}
