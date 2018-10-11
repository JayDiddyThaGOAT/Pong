using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    
    private Image pauseBG;
    private Text pauseText;

    private Button retryButton, menuButton, exitButton;

    private Ball ball;
    private Vector2 ballSavedVelocity;
    private Animator timer;

    private MaxSlider maxScoreSlider;
    private VolumeSlider volumeSlider;

    private AudioSource sound;

    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();

        pauseBG = GetComponent<Image>();
        pauseText = transform.GetChild(0).GetComponent<Text>();

        retryButton = transform.GetChild(3).GetComponent<Button>();
        menuButton = transform.GetChild(4).GetComponent<Button>();
        exitButton = transform.GetChild(5).GetComponent<Button>();

        retryButton.GetComponent<EventTrigger>().enabled = false;
        menuButton.GetComponent<EventTrigger>().enabled = false;
        exitButton.GetComponent<EventTrigger>().enabled = false;

        ball = FindObjectOfType<Ball>();
        timer = ball.GetComponentInChildren<Animator>();

        maxScoreSlider = FindObjectOfType<MaxSlider>();
        maxScoreSlider.Toggle(false);

        volumeSlider = FindObjectOfType<VolumeSlider>();
        volumeSlider.Toggle(false);
    }

    void ReLaunchBall()
    {
        ball.GetComponent<Rigidbody2D>().velocity = ballSavedVelocity;
    }

    IEnumerator Retry(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RetryGame(AudioClip clip)
    {
        sound.PlayOneShot(clip);
        Time.timeScale = 1f;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        foreach (Paddle paddle in FindObjectsOfType<Paddle>())
            paddle.speed = 0;

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
        Time.timeScale = 1f;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        foreach (Paddle paddle in FindObjectsOfType<Paddle>())
            paddle.speed = 0;

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
        Time.timeScale = 1f;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        foreach (Paddle paddle in FindObjectsOfType<Paddle>())
            paddle.speed = 0;

        StartCoroutine(Exit(clip.length));
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale == 0f)
            {
                pauseBG.color = new Color(pauseBG.color.r, pauseBG.color.g, pauseBG.color.b, 0f);
                pauseText.color = new Color(pauseText.color.r, pauseText.color.g, pauseText.color.b, 0f);

                retryButton.interactable = false;
                menuButton.interactable = false;
                exitButton.interactable = false;
                retryButton.GetComponent<EventTrigger>().enabled = false;
                menuButton.GetComponent<EventTrigger>().enabled = false;
                exitButton.GetComponent<EventTrigger>().enabled = false;
                
                maxScoreSlider.Toggle(false);
                volumeSlider.Toggle(false);

                ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                timer.PlayInFixedTime(Animator.StringToHash("Countdown"), 0, 0f);
                Time.timeScale = 1f;
                Invoke("ReLaunchBall", 4f);
            }
            else
            {
                if (!(ball.IsInvoking() || IsInvoking()))
                {
                    pauseBG.color = new Color(pauseBG.color.r, pauseBG.color.g, pauseBG.color.b, 0.5f);
                    pauseText.color = new Color(pauseText.color.r, pauseText.color.g, pauseText.color.b, 1f);

                    retryButton.interactable = true;
                    menuButton.interactable = true;
                    exitButton.interactable = true;
                    retryButton.GetComponent<EventTrigger>().enabled = true;
                    menuButton.GetComponent<EventTrigger>().enabled = true;
                    exitButton.GetComponent<EventTrigger>().enabled = true;

                    maxScoreSlider.Toggle(true);
                    maxScoreSlider.SetMinimumScore(ball.GetWinningScore() + 1);

                    volumeSlider.Toggle(true);

                    ballSavedVelocity = ball.GetComponent<Rigidbody2D>().velocity;
                    Time.timeScale = 0f;
                }
            }
        }
    }
}