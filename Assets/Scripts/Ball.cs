using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;

    Rigidbody2D body;
    float[] directions = new float[2];

    public bool countDownDone = false;

    private Text redScoreUI, blueScoreUI;
    private int redScore, blueScore;
    [HideInInspector]
    public bool noScore = true;

    private AudioSource sound;
    public AudioClip scoredClip, bounceClip, whistleClip, launchLeftClip, launchRightClip;

    private float launchX, launchY;

    private Animator timer;

    void Awake()
    {
        redScoreUI = GameObject.Find("Red Score").GetComponent<Text>();
        blueScoreUI = GameObject.Find("Blue Score").GetComponent<Text>();
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();

        timer = GetComponentInChildren<Animator>();

        Invoke("Launch", 4f);

    }

    public void Launch()
    {
        directions[0] = -1f;
        directions[1] = 1f;

        launchX = directions[Random.Range(0, directions.Length)];
        if (launchX == 1f)
            sound.clip = launchRightClip;
        else if (launchX == -1f)
            sound.clip = launchLeftClip;
        sound.Play();

        launchY = directions[Random.Range(0, directions.Length)];

        body.velocity = new Vector2(launchX, launchY) * speed;
    }

    public int GetWinningScore()
    {
        return Mathf.Max(redScore, blueScore);
    }

    public void ResetBall()
    {
        transform.position = Vector2.zero;
        body.velocity = Vector2.zero;
    }

    void OnBecameInvisible()
    {
        if (Application.isPlaying)
        {
            ResetBall();
            timer.PlayInFixedTime(Animator.StringToHash("Countdown"), 0, 0f);
            Invoke("Launch", 4f);
        }
    }

    void Update()
    {
        sound.volume = PlayerPrefs.GetFloat("Volume");

        redScoreUI.text = redScore.ToString();
        blueScoreUI.text = blueScore.ToString();

        if (redScore == PlayerPrefs.GetInt("Max Score"))
            redScoreUI.color = Color.white;

        if (blueScore == PlayerPrefs.GetInt("Max Score"))
            blueScoreUI.color = Color.white;
    }

    private void FixedUpdate()
    {
        body.constraints = PauseMenu.isPaused ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.None;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        sound.clip = bounceClip;
        if (col.gameObject.tag == "Paddle")
        {
            float y = ((transform.position.y - col.transform.position.y) / col.collider.bounds.size.y) + col.collider.attachedRigidbody.velocity.normalized.y;
            Vector2 dir;
            if (col.transform.position.x < 0f)
                dir = new Vector2(1f, y);
            else
                dir = new Vector2(-1f, y);

            body.velocity = dir * speed;

            sound.pitch = Random.Range(0.95f, 1.25f);
        }
        else
        {
            sound.pitch -= 0.125f;
            sound.pitch = Mathf.Clamp(sound.pitch, 0.95f - 0.125f, 1.25f - 0.125f);
        }

        sound.Play();
    }

    void RedWon()
    {
        PlayerPrefs.SetString("Previous Scene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("RedWins");
    }

    void BlueWon()
    {
        PlayerPrefs.SetString("Previous Scene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("BlueWins");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Red Goal")
            redScore++;
        else if (col.gameObject.name == "Blue Goal")
            blueScore++;

        noScore = false;
        ResetBall();

        sound.pitch = 1f;
        if (redScore == PlayerPrefs.GetInt("Max Score"))
        {
            sound.PlayOneShot(whistleClip);
            Invoke("RedWon", whistleClip.length);
        }
        else if (blueScore == PlayerPrefs.GetInt("Max Score"))
        {
            sound.PlayOneShot(whistleClip);
            Invoke("BlueWon", whistleClip.length);
        }
        else
        {
            sound.PlayOneShot(scoredClip); ;
            timer.PlayInFixedTime(Animator.StringToHash("Countdown"), 0, 0f);
            Invoke("Launch", 4f);
        }
    }
}
