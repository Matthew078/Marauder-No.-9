using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerStart : MonoBehaviour
{
    [SerializeField]
    private Canvas canvasStart, canvasMain, canvasSettings, canvasCredits;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private Slider sfxSlider, musicSlider;

    // Start is called before the first frame update
    void Start()
    {
        StartMenu();
        sfxSlider.value = gm.GetSFXVolume();
        musicSlider.value = gm.GetMusicVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasSettings.enabled)
        {
            gm.SetSFXVolume(sfxSlider.value);
            gm.SetMusicVolume(musicSlider.value);
        }
    }

    public void StartMenu()
    {
        canvasStart.enabled = true;
        canvasMain.enabled = false;
        canvasSettings.enabled = false;
        canvasCredits.enabled = false;
    }

    public void MainMenu()
    {
        canvasStart.enabled = false;
        canvasMain.enabled = true;
        canvasSettings.enabled = false;
        canvasCredits.enabled = false;
    }
    public void SettingsMenu()
    {
        canvasStart.enabled = false;
        canvasMain.enabled = false;
        canvasSettings.enabled = true;
        canvasCredits.enabled = false;
    }
    public void CreditsMenu()
    {
        canvasStart.enabled = false;
        canvasMain.enabled = false;
        canvasSettings.enabled = false;
        canvasCredits.enabled = true;
    }

    public void StartGame()
    {
        gm.StartGame();
    }

    
}

