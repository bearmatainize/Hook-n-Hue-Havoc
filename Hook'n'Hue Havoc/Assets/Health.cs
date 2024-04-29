using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (gameObject.CompareTag("Player"))
        {
            HUDManager.instance.UpdateHealthBar(health, 100f);
        }
        
        if (health <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                EndGame.instance.PlayerKilled();
            }
            else
            {
                EndGame.instance.EnemyKilled();
                Destroy(gameObject);
            }
        }
    }
}
