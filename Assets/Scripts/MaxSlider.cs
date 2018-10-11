using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MaxSlider : MonoBehaviour {

    private Slider maxScoreSlider;
    private Text maxScore;

    private AudioSource sound;

    public void SetMinimumScore(int score)
    {
        maxScoreSlider.minValue = score;
    }

    public void Toggle(bool toggled)
    {
        maxScoreSlider.interactable = toggled;
        for (int i = 0; i < maxScoreSlider.transform.childCount; ++i)
        {
            GameObject child = maxScoreSlider.transform.GetChild(i).gameObject;
            if (child.name != "Max Score Value")
                child.SetActive(toggled);
            else
            {
                Color color = child.GetComponent<Text>().color;
                if (toggled)
                    child.GetComponent<Text>().color = new Color(color.r, color.g, color.b, 1f);
                else
                    child.GetComponent<Text>().color = new Color(color.r, color.g, color.b, 0f);
            }
        }
    }

    void UpdateMaxScore()
    {
        sound.Play();
        PlayerPrefs.SetInt("Max Score", Mathf.RoundToInt(maxScoreSlider.value));
        maxScore.text = PlayerPrefs.GetInt("Max Score").ToString();
    }

    // Use this for initialization
    void Start()
    {
        maxScoreSlider = GetComponent<Slider>();
        maxScoreSlider.value = PlayerPrefs.GetInt("Max Score");
        maxScore = GetComponentsInChildren<Text>()[1];

        sound = GetComponent<AudioSource>();
        maxScoreSlider.onValueChanged.AddListener(delegate { UpdateMaxScore(); });
    }
}
