using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // Handle the character's death here
            Debug.Log(gameObject.name + " has died.");
        }
    }
}
