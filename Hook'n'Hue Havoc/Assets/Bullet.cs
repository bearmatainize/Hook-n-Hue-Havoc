using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject paintSplatterPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collided with anything
        if (collision.gameObject != null)
        {
            // Instantiate paint splatter at the collision point
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.LookRotation(contact.normal, Vector3.up);
            // Manually rotate by 180 degrees around the up axis
            rotation *= Quaternion.Euler(0, 180, 0);

            // Instantiate paint splatter with adjusted rotation
            Instantiate(paintSplatterPrefab, contact.point, rotation);

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
