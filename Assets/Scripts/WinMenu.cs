<<<<<<< refs/remotes/Pong/master
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public Transform winner;
    public float speed;

    private MaxSlider maxScoreSlider;
    private VolumeSlider volumeSlider;
    private Button retryButton, menuButton, exitButton;

    private AudioSource sound;

    public void PlayClip(AudioClip clip)
    {
        sound.clip = clip;
        sound.pitch = 1f;
        sound.Play();
    }

    IEnumerator Retry(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(PlayerPrefs.GetString("Previous Scene"));
    }

    public void RetryGame(AudioClip clip)
    {
        sound.PlayOneShot(clip);
        StartCoroutine(Retry(clip.length));
    }

    IEnumerator Menu(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("StartMenu");
    }

    public void GoBackToMainMenu(AudioClip clip)
    {
        sound.PlayOneShot(clip);
        StartCoroutine(Menu(clip.length));
    }

    IEnumerator Exit(float time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
    }

    public void ExitGame(AudioClip clip)
    {
        sound.PlayOneShot(clip);
        StartCoroutine(Exit(clip.length));
    }

    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.playOnAwake = true;

        maxScoreSlider = FindObjectOfType<MaxSlider>();
        volumeSlider = FindObjectOfType<VolumeSlider>();

        retryButton = FindObjectsOfType<Button>()[0];
        menuButton = FindObjectsOfType<Button>()[1];
        exitButton = FindObjectsOfType<Button>()[2];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        winner.position = Vector2.Lerp(winner.position, Vector2.zero, speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        sound.volume = PlayerPrefs.GetFloat("Volume");

        retryButton.interactable = winner.position == Vector3.zero;
        retryButton.GetComponent<EventTrigger>().enabled = retryButton.interactable;
        menuButton.interactable = winner.position == Vector3.zero;
        menuButton.GetComponent<EventTrigger>().enabled = menuButton.interactable;
        exitButton.interactable = winner.position == Vector3.zero;
        exitButton.GetComponent<EventTrigger>().enabled = exitButton.interactable;

        maxScoreSlider.Toggle(winner.position == Vector3.zero);
        volumeSlider.Toggle(winner.position == Vector3.zero);

        float retryAlpha, mainMenuAlpha, exitAlpha;

        if (winner.position == Vector3.zero)
        {
            retryAlpha = 1f;
            mainMenuAlpha = 1f;
            exitAlpha = 1f;
        }
        else
        {
            retryAlpha = 0f;
            mainMenuAlpha = 0f;
            exitAlpha = 0f;
        }

        retryButton.GetComponentInChildren<Text>().color = new Color(retryButton.GetComponentInChildren<Text>().color.r,
                                                                     retryButton.GetComponentInChildren<Text>().color.g,
                                                                     retryButton.GetComponentInChildren<Text>().color.b,
                                                                     retryAlpha);

        menuButton.GetComponentInChildren<Text>().color = new Color(menuButton.GetComponentInChildren<Text>().color.r,
                                                                        menuButton.GetComponentInChildren<Text>().color.g,
                                                                        menuButton.GetComponentInChildren<Text>().color.b,
                                                                        mainMenuAlpha);

        exitButton.GetComponentInChildren<Text>().color = new Color(exitButton.GetComponentInChildren<Text>().color.r,
                                                                    exitButton.GetComponentInChildren<Text>().color.g,
                                                                    exitButton.GetComponentInChildren<Text>().color.b,
                                                                    exitAlpha);
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public Transform winner;
    public float speed;

    private MaxSlider maxScoreSlider;
    private VolumeSlider volumeSlider;
    private Button retryButton, menuButton, exitButton;

    private AudioSource sound;

    public void PlayClip(AudioClip clip)
    {
        sound.clip = clip;
        sound.pitch = 1f;
        sound.Play();
    }

    IEnumerator Retry(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(PlayerPrefs.GetString("Previous Scene"));
    }

    public void RetryGame(AudioClip clip)
    {
        sound.PlayOneShot(clip);
        StartCoroutine(Retry(clip.length));
    }

    IEnumerator Menu(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("StartMenu");
    }

    public void GoBackToMainMenu(AudioClip clip)
    {
        sound.PlayOneShot(clip);
        StartCoroutine(Menu(clip.length));
    }

    IEnumerator Exit(float time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
    }

    public void ExitGame(AudioClip clip)
    {
        sound.PlayOneShot(clip);
        StartCoroutine(Exit(clip.length));
    }

    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.playOnAwake = true;

        maxScoreSlider = FindObjectOfType<MaxSlider>();
        volumeSlider = FindObjectOfType<VolumeSlider>();

        retryButton = FindObjectsOfType<Button>()[0];
        menuButton = FindObjectsOfType<Button>()[1];
        exitButton = FindObjectsOfType<Button>()[2];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        winner.position = Vector2.Lerp(winner.position, Vector2.zero, speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        sound.volume = PlayerPrefs.GetFloat("Volume");

        retryButton.interactable = winner.position == Vector3.zero;
        retryButton.GetComponent<EventTrigger>().enabled = retryButton.interactable;
        menuButton.interactable = winner.position == Vector3.zero;
        menuButton.GetComponent<EventTrigger>().enabled = menuButton.interactable;
        exitButton.interactable = winner.position == Vector3.zero;
        exitButton.GetComponent<EventTrigger>().enabled = exitButton.interactable;

        maxScoreSlider.Toggle(winner.position == Vector3.zero);
        volumeSlider.Toggle(winner.position == Vector3.zero);

        float retryAlpha, mainMenuAlpha, exitAlpha;

        if (winner.position == Vector3.zero)
        {
            retryAlpha = 1f;
            mainMenuAlpha = 1f;
            exitAlpha = 1f;
        }
        else
        {
            retryAlpha = 0f;
            mainMenuAlpha = 0f;
            exitAlpha = 0f;
        }

        retryButton.GetComponentInChildren<Text>().color = new Color(retryButton.GetComponentInChildren<Text>().color.r,
                                                                     retryButton.GetComponentInChildren<Text>().color.g,
                                                                     retryButton.GetComponentInChildren<Text>().color.b,
                                                                     retryAlpha);

        menuButton.GetComponentInChildren<Text>().color = new Color(menuButton.GetComponentInChildren<Text>().color.r,
                                                                        menuButton.GetComponentInChildren<Text>().color.g,
                                                                        menuButton.GetComponentInChildren<Text>().color.b,
                                                                        mainMenuAlpha);

        exitButton.GetComponentInChildren<Text>().color = new Color(exitButton.GetComponentInChildren<Text>().color.r,
                                                                    exitButton.GetComponentInChildren<Text>().color.g,
                                                                    exitButton.GetComponentInChildren<Text>().color.b,
                                                                    exitAlpha);
    }
}
>>>>>>> Fixed UI Bug Where Highest Max Score Displayed "10"
