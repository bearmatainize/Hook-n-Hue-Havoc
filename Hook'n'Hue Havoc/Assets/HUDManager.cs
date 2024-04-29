using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    public GameObject enemiesRemainingText;
    public Slider healthBar;
    private TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent = enemiesRemainingText.GetComponent<TextMeshProUGUI>();
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateEnemiesRemaining(int enemiesRemaining)
    {
        textComponent.text = "Enemies Remaining: " + enemiesRemaining;
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.value = currentHealth / maxHealth;
    }
}
