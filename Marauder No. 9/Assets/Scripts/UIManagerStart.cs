using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManagerStart : MonoBehaviour
{
    // Fields
    [SerializeField]
    private Canvas canvasStart, canvasMain, canvasSettings, canvasCredits;
    [SerializeField]
    private EventSystem es;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private Slider sfxSlider, musicSlider;

    // Start is called before the first frame update
    void Start()
    {
        sfxSlider.wholeNumbers = true;
        musicSlider.wholeNumbers = true;
        sfxSlider.maxValue = 10;
        musicSlider.maxValue = 10;
        StartMenu();
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
    // StartMenu method will display the start menu
    public void StartMenu()
    {
        canvasStart.enabled = true;
        SetEnabledOfChildren(canvasStart, true);
        canvasMain.enabled = false;
        SetEnabledOfChildren(canvasMain, false);
        canvasSettings.enabled = false;
        SetEnabledOfChildren(canvasSettings, false);
        canvasCredits.enabled = false;
        SetEnabledOfChildren(canvasCredits, false);
        es.SetSelectedGameObject(canvasStart.transform.Find("Start").gameObject);
    }
    // MainMenu method will display the main menu
    public void MainMenu()
    {
        canvasStart.enabled = false;
        SetEnabledOfChildren(canvasStart, false);
        canvasMain.enabled = true;
        SetEnabledOfChildren(canvasMain, true);
        canvasSettings.enabled = false;
        SetEnabledOfChildren(canvasSettings, false);
        canvasCredits.enabled = false;
        SetEnabledOfChildren(canvasCredits, false);
        es.SetSelectedGameObject(canvasMain.transform.Find("Play").gameObject);
    }
    // SettingsMenu method will display the settings menu
    public void SettingsMenu()
    {
        canvasStart.enabled = false;
        SetEnabledOfChildren(canvasStart, false);
        canvasMain.enabled = false;
        SetEnabledOfChildren(canvasMain, false);
        canvasSettings.enabled = true;
        SetEnabledOfChildren(canvasSettings, true);
        canvasCredits.enabled = false;
        SetEnabledOfChildren(canvasCredits, false);
        es.SetSelectedGameObject(canvasSettings.transform.Find("SFXSlider").gameObject);
    }
    // CreditsMenu method will display the credits menu
    public void CreditsMenu()
    {
        canvasStart.enabled = false;
        SetEnabledOfChildren(canvasStart, false);
        canvasMain.enabled = false;
        SetEnabledOfChildren(canvasMain, false);
        canvasSettings.enabled = false;
        SetEnabledOfChildren(canvasSettings, false);
        canvasCredits.enabled = true;
        SetEnabledOfChildren(canvasCredits, true);
        es.SetSelectedGameObject(canvasCredits.transform.Find("Back").gameObject);
    }
    // StartGame method will start the game
    public void StartGame()
    {
        gm.StartGame();
    }
    // SetEnabledOfChildren is a helper method that will enable the
    // gui elements of a particular Canvas
    // @param c is the Canvas
    // @param b is a boolean that will enable or disable the elements
    private void SetEnabledOfChildren(Canvas c, bool b)
    {
        foreach (Transform child in c.transform)
        {
            Button button = child.gameObject.GetComponent<Button>();
            Slider slider = child.gameObject.GetComponent<Slider>();
            if (button != null)
            {
                button.enabled = b;
            }
            if (slider != null)
            {
                slider.enabled = b;
                if(slider.gameObject.name == "MusicSlider")
                {
                    slider.value = gm.GetMusicVolume();
                }
                if (slider.gameObject.name == "SFXSlider")
                {
                    slider.value = gm.GetSFXVolume();
                }
            }   
        }
    }
}

