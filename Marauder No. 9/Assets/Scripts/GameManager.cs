using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Fields
    private static GameManager gm = null;
    [SerializeField] private float sfxVolume, musicVolume;
    
    // Awake is called when an enabled script instance is being loaded
    private void Awake()
    {
        // Create Singleton
        if(gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // LoadLevel method will load a specific scene
    // @param levelName is a string of the level name
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    // NextLevel method will load the next scene in the build index
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // QuitGame method will quit the application
    public void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
    // StartGame method will load the Main Scene and play the Game Music
    public void StartGame()
    {
        SoundManager.Instance.playMusic("Game");
        SceneManager.LoadScene("Main");    
    }
    // SetMusicVolume method will set the musicVolume field
    // @param mv is a float value
    public void SetMusicVolume(float mv)
    {
        musicVolume = mv;
    }
    // SetSFXVolume method will set the sfxVolume field
    // @param sfxv is a float value
    public void SetSFXVolume(float sfxv)
    {
        sfxVolume = sfxv;
    }
    // GetMusicVolume method will @return the musicVolume field 
    public float GetMusicVolume()
    {
        return musicVolume;
    }
    // GetSFXVolume method will @return the sfxVolume field  
    public float GetSFXVolume()
    {
        return sfxVolume;
    }
}
