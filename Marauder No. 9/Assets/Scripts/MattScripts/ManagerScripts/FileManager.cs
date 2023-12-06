using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    // Fields
    [SerializeField]
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // SaveData will save the fields SFXVolume and MusicVolume in PlayerPrefs
    public void SaveDate()
    {
        PlayerPrefs.SetFloat("SFXVolume", gm.GetSFXVolume());
        PlayerPrefs.SetFloat("MusicVolume", gm.GetMusicVolume());
    }
    // LoadData will load the the data from the file and store it
    // the fields SFXVolume and MusicVolume
    public void LoadData()
    {
        if (PlayerPrefs.HasKey("SFXVolume") && PlayerPrefs.HasKey("MusicVolume"))
        {
            gm.SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
            gm.SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        }
        else
        {
            gm.SetSFXVolume(5f);
            gm.SetMusicVolume(5f);
        }
    }
}
