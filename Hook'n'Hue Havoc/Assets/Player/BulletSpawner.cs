using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float bulletSpeed = 20f;

    public void SpawnBullet(Vector3 spawnDirection, bool isPlayerBullet)
    {
        // Instantiate bullet at the spawn point with the specified direction
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);

        // Get the rigidbody component of the bullet
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        
        // Set the velocity of the bullet to make it move forward
        bulletRigidbody.velocity = spawnDirection * bulletSpeed;

        // Disable gravity for the bullet
        bulletRigidbody.useGravity = false;
        
        // Set the isPlayerBullet variable of the Bullet script
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.isPlayerBullet = isPlayerBullet;
    }
}
