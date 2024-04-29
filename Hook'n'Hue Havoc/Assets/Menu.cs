using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    public GameObject PauseMenu;
    public GameObject resumeButton, settingsButton, quitButton;

    public EventSystem eventSystem;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if(!PauseMenu.activeInHierarchy)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            eventSystem.SetSelectedGameObject(null);
            
            eventSystem.SetSelectedGameObject(resumeButton);
        }
        else
        {
            Resume();
        }
    }

    public void Train()
    {
        // Load the train scene 
        SceneManager.LoadScene("Training");
        Time.timeScale = 1;
    }
    
    public void Play()
    {
        // Load the mall scene 
        SceneManager.LoadScene("Mall");
        Time.timeScale = 1;
    }
    
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMenu()
    {
        // Load the start screen scene 
        SceneManager.LoadScene("Start Screen");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
