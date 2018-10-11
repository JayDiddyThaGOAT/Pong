using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    private AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        sound.clip = clip;
        sound.PlayOneShot(clip);
    }
}
