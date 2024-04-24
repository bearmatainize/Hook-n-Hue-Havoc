using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject paintSplatterPrefab;
    public float damage = 50f;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collided with anything
        if (collision.gameObject != null)
        {
            // Check if the collided object is not the player or bullet or enemy or target dummy
            if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Bullet") && !collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Dummy"))
            {
                // Instantiate paint splatter at the collision point
                ContactPoint contact = collision.contacts[0];
                Vector3 offset = contact.normal * 0.05f; // Adjust the offset distance as needed
                Quaternion rotation = Quaternion.LookRotation(contact.normal, Vector3.up);
                // Manually rotate by 180 degrees around the up axis
                rotation *= Quaternion.Euler(0, 180, 0);

                // Instantiate paint splatter with adjusted position and rotation
                Instantiate(paintSplatterPrefab, contact.point + offset, rotation);

                // Destroy the bullet
                Destroy(gameObject);
            }
            
            if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
            {
                Health health = collision.gameObject.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
                
                // Destroy the bullet
                Destroy(gameObject);
            }
        }
    }
}
