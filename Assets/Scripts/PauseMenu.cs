using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused;
    public GameObject pauseMenuUI;
    public AudioSource sound;
    public AudioClip buttonPressedClip;

    private Ball ball;

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                if (!ball.IsInvoking())
                    Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void Retry()
    {
        SceneManager.LoadScene("GameMenu");
    }

    private void Menu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    private void Exit()
    {
        Application.Quit();
    }

    public void RunFunction(string functionName)
    {
        Time.timeScale = 1f;
        sound.PlayOneShot(buttonPressedClip);
        Invoke(functionName, buttonPressedClip.length);
    }

}