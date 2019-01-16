using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    private AudioSource timerSound;

    void Start()
    {
        timerSound = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        timerSound.volume = PlayerPrefs.GetFloat("Volume");
        timerSound.clip = clip;
        timerSound.PlayOneShot(clip);
    }
}
