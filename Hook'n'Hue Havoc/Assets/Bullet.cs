using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Material bulletMaterial; // Material for bullet
    public GameObject paintSplatterPrefab;
    public Material[] paintMaterials; // Array of paint materials
    public float damage = 34f;
    public Color playerColor = Color.blue; // Color for player's bullet (blue)
    public Color enemyColor = new Color(1, 0.65f, 0); // Color for enemy's bullet (orange)
    public bool isPlayerBullet; // True if the player shot the bullet, false if the enemy shot the bullet

    private void Start()
    {
        // Set the color of the bullet based on who shot it
        bulletMaterial = GetComponent<Renderer>().material;
        bulletMaterial.color = isPlayerBullet ? playerColor : enemyColor;
    }
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
                // Manually rotate by 180 degrees around the up axis, and randomly rotate around the z axis
                rotation *= Quaternion.Euler(0, 180, Random.Range(0, 360));
                // Instantiate paint splatter with adjusted position and rotation
                GameObject paintSplatter = Instantiate(paintSplatterPrefab, contact.point + offset, rotation);

                // Get a random material from the array
                Material randomMaterial = paintMaterials[Random.Range(0, paintMaterials.Length)];

                // Assign the random material to the paint splatter
                Renderer renderer = paintSplatter.GetComponent<Renderer>();
                renderer.material = randomMaterial;
                
                // Set the color of the material based on who shot the bullet
                if (isPlayerBullet)
                {
                    renderer.material.SetColor("_Color", playerColor);
                }
                else
                {
                    renderer.material.SetColor("_Color", enemyColor);
                }

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