using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager gm = null;
    [SerializeField]
    private float sfxVolume, musicVolume;

    private void Awake()
    {
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");    
    }

    public void SetMusicVolume(float mv)
    {
        musicVolume = mv;
    }

    public void SetSFXVolume(float sfxv)
    {
        sfxVolume = sfxv;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }
}
