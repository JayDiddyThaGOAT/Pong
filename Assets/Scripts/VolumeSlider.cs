using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class VolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;
    private Text volumeText;

    float previousVolume;

    private AudioSource [] sounds;
    private AudioSource confirmSound;

    public float PlayClip(AudioClip clip)
    {
        sounds[0].clip = clip;
        sounds[0].pitch = 1f;
        sounds[0].Play();

        return clip.length;
    }

    public void Toggle(bool toggled)
    {
        volumeSlider.interactable = toggled;
        for (int i = 0; i < volumeSlider.transform.childCount; ++i)
        {
            GameObject child = volumeSlider.transform.GetChild(i).gameObject;
            if (!(child.name == "Volume Title" || child.name == "Volume Value"))
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

    // Use this for initialization
    void Start()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = 100f * PlayerPrefs.GetFloat("Volume");

        volumeText = GetComponentsInChildren<Text>()[1];

        EventTrigger volumeConfirm = GetComponent<EventTrigger>();
        EventTrigger.Entry confirm = new EventTrigger.Entry
        {
            eventID = EventTriggerType.EndDrag
        };
        confirm.callback.AddListener((data) => { OnEndDragDelegate((PointerEventData)data); });
        volumeConfirm.triggers.Add(confirm);

        sounds = FindObjectsOfType<AudioSource>();
        foreach (AudioSource sound in sounds)
        {
            sound.mute = PlayerPrefs.GetInt("Muted") == 1 ? true : false;
            sound.volume = volumeSlider.normalizedValue;
        }
        confirmSound = GetComponent<AudioSource>();
    }

    public void OnEndDragDelegate(PointerEventData data)
    {
        foreach(AudioSource sound in sounds)
            sound.volume = volumeSlider.normalizedValue;

        PlayerPrefs.SetFloat("Volume", sounds[0].volume);
        confirmSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            foreach (AudioSource sound in sounds)
            {
                if (!sound.mute)
                    sound.volume += Input.GetAxis("Mouse ScrollWheel") / 2f;
            }

            volumeSlider.value = 100f * sounds[0].volume;

            if (volumeSlider.interactable && (volumeSlider.value > volumeSlider.minValue && volumeSlider.value < volumeSlider.maxValue))
                confirmSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach (AudioSource sound in sounds)
                sound.mute = !sound.mute;
            PlayerPrefs.SetInt("Muted", sounds[0].mute ? 1 : 0);
        }

        if (confirmSound.mute)
        {
            volumeSlider.interactable = false;
            volumeText.text = 0f.ToString();
        }
        else
        {
            volumeSlider.interactable = true;
            volumeText.text = volumeSlider.value.ToString();
        }
    }
}
