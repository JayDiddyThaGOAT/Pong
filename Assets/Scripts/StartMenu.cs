using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public Text title;
    public float titleStrobeDuration;

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
        float t = Mathf.PingPong(Time.time / titleStrobeDuration, 1f);
        title.color = Color.Lerp(Color.red, Color.blue, t);

        float redOn, blueOn;
        if (redPaddle.isOn)
            redOn = 1f;
        else
            redOn = 0f;

        if (bluePaddle.isOn)
            blueOn = 1f;
        else
            blueOn = 0f;

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
        if (redPaddle.isOn && bluePaddle.isOn)
            SceneManager.LoadScene("TwoPlayer");
        else if (redPaddle.isOn && !bluePaddle.isOn)
            SceneManager.LoadScene("RedPlayer");
        else if (!redPaddle.isOn && bluePaddle.isOn)
            SceneManager.LoadScene("BluePlayer");
        else
            SceneManager.LoadScene("NoPlayer");
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
