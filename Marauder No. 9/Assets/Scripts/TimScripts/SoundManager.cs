using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Sound[] soundClips;
    public AudioSource source;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playSound(string soundName)
    {
        Sound s = Array.Find(soundClips, x => x.name == soundName);

        if (s == null)
        {
            Debug.Log("No Sound with name: " + soundName);
        }
        else
        {
            source.clip = soundClips[0].audio;
            source.Play();
        }
    }
}

