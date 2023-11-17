using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Sound[] soundClips;
    public Sound[] musicClips;
    public AudioSource source;
    [SerializeField] private GameManager gm;

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

    private void Start()
    {
        SoundManager.Instance.playMusic("Menu");
    }

    private void Update()
    {
        source.volume = gm.GetMusicVolume() / 10;
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
            source.PlayOneShot(s.audio, gm.GetSFXVolume()/10);
        }
    }

    public void playMusic(string musicName)
    {
        Sound s = Array.Find(musicClips, x => x.name == musicName);

        source.Stop();
        if (s == null)
        {
            Debug.Log("No Music with name: " + musicName);
        }
        else
        {
            source.clip = s.audio;
            source.Play();
        }
    }
}

