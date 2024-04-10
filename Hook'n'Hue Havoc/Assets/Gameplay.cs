using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gameplay : MonoBehaviour
{

    public GameObject timerText;
    public GameObject endGameText;
    public GameObject sourcesText;
    private TextMeshProUGUI textComponentTimer;
    private TextMeshProUGUI textComponentEndGame;
    public bool lost;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        textComponentTimer = timerText.GetComponent<TextMeshProUGUI>();
        textComponentEndGame = endGameText.GetComponent<TextMeshProUGUI>();
        timer = 20f;
        textComponentTimer.text = "Timer: " + timer.ToString();
        endGameText.SetActive(false);
        sourcesText.SetActive(false);
        lost = false;
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;
        textComponentTimer.text = "Timer: " + timer.ToString("N2");

        if(timer <= 0) {
            EndGame(true);
        }

        if(lost) {
            EndGame(false);
        }
    }

    void EndGame(bool win) {
        sourcesText.SetActive(true);
        Time.timeScale = 0;
        if(win) {
            endGameText.SetActive(true);
        } else {
            textComponentEndGame.text = "You Lose!";
            endGameText.SetActive(true);
        }
    }
}
