<<<<<<< refs/remotes/Pong/master
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
    public Toggle redPaddle, bluePaddle;
    public Text redControls, blueControls;
    public Button playButton, exitButton;

    private AudioSource sound;

    public void PlayClip(AudioClip clip)
    {
        sound.clip = clip;
        sound.pitch = 1f;
        sound.Play();
    }

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        sound.volume = PlayerPrefs.GetFloat("Volume");

        float redOn, blueOn;
        if (redPaddle.isOn)
            redOn = 1f;
        else
            redOn = 0f;

        if (bluePaddle.isOn)
            blueOn = 1f;
        else
            blueOn = 0f;

        playButton.interactable = PlayerPrefs.GetInt("Max Score") > 0;

        redControls.color = new Color(1f, 0f, 0f, redOn);
        blueControls.color = new Color(0f, 0f, 1f, blueOn);
	}

    public void PlayGame(AudioClip clip)
    {
        PlayClip(clip);
        Invoke("Play", clip.length);
    }

    void Play()
    {
        PlayerPrefs.SetInt("Red Player", redPaddle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Blue Player", bluePaddle.isOn ? 1 : 0);
        SceneManager.LoadScene("GameMenu");
    }

    public void ExitGame(AudioClip clip)
    {
        PlayClip(clip);
        Invoke("Exit", clip.length);
    }

    void Exit()
    {
        Application.Quit();
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
    public Toggle redPaddle, bluePaddle;
    public Text redControls, blueControls;
    public Button playButton, exitButton;

    private AudioSource sound;

    public void PlayClip(AudioClip clip)
    {
        sound.clip = clip;
        sound.pitch = 1f;
        sound.Play();
    }

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        sound.volume = PlayerPrefs.GetFloat("Volume");

        float redOn, blueOn;
        if (redPaddle.isOn)
            redOn = 1f;
        else
            redOn = 0f;

        if (bluePaddle.isOn)
            blueOn = 1f;
        else
            blueOn = 0f;

        playButton.interactable = PlayerPrefs.GetInt("Max Score") > 0;

        redControls.color = new Color(1f, 0f, 0f, redOn);
        blueControls.color = new Color(0f, 0f, 1f, blueOn);
	}

    public void PlayGame(AudioClip clip)
    {
        PlayClip(clip);
        Invoke("Play", clip.length);
    }

    void Play()
    {
        PlayerPrefs.SetInt("Red Player", redPaddle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Blue Player", bluePaddle.isOn ? 1 : 0);
        SceneManager.LoadScene("GameMenu");
    }

    public void ExitGame(AudioClip clip)
    {
        PlayClip(clip);
        Invoke("Exit", clip.length);
    }

    void Exit()
    {
        Application.Quit();
    }
}
>>>>>>> Fixed UI Bug Where Highest Max Score Displayed "10"
