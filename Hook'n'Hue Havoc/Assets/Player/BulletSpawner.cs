using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float bulletSpeed = 10f;

    public void SpawnBullet()
    {
        // Instantiate a bullet at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

        // Get the rigidbody component of the bullet
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Check if the bullet has a rigidbody component
        if (bulletRigidbody != null)
        {
            // Set the velocity of the bullet to make it move forward
            bulletRigidbody.velocity = spawnPoint.forward * bulletSpeed;

            // Disable gravity for the bullet
            bulletRigidbody.useGravity = false;
        }
        else
        {
            Debug.LogWarning("Bullet prefab does not have a Rigidbody component.");
        }
    }

    private void OnCollision(Collision collision)
    {
        // Check if the bullet collided with anything
        if (collision.gameObject != null)
        {
            // Instantiate paint effect at the collision point
            //Instantiate(paintPrefab, collision.contacts[0].point, Quaternion.identity);

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
