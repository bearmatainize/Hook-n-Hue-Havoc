using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public static EndGame instance;
    
    public GameObject EndGameMenu;
    public GameObject endGameText;
    public GameObject pressEscText;
    public GameObject sourcesText;
    private TextMeshProUGUI textComponent;

    public int trainingDummiesCount = 5;
    public int enemiesCount = 10;
    public bool isPlayerAlive = true;
    
    void Start()
    {
        textComponent = endGameText.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (EndGameMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Start Screen");
        }
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

    public void EnemyKilled()
    {
        if (SceneManager.GetActiveScene().name == "Training")
        {
            trainingDummiesCount--;
            HUDManager.instance.UpdateEnemiesRemaining(trainingDummiesCount);
            if (trainingDummiesCount <= 0)
            {
                // Player wins in the training scene
                textComponent.text = "You completed the training!";
                Debug.Log("You won in the training scene!");
                ShowText();
            }
        }
        else if (SceneManager.GetActiveScene().name == "Mall")
        {
            enemiesCount--;
            HUDManager.instance.UpdateEnemiesRemaining(enemiesCount);
            if (enemiesCount <= 0)
            {
                // Player wins in the mall scene
                textComponent.text = "You won in the mall!";
                Debug.Log("You won in the mall scene!");
                ShowText();
            }
        }
    }

    public void PlayerKilled()
    {
        isPlayerAlive = false;
        if (SceneManager.GetActiveScene().name == "Mall")
        {
            // Player loses in the mall scene
            textComponent.text = "You lost in the mall!";
            Debug.Log("You lost in the mall scene!");
            ShowText();
        }
    }

    public void ShowText()
    {
        EndGameMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
