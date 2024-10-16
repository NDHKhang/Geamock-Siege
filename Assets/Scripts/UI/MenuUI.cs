using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    LoadManager loadManager;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject pauseUI;
    private MusicPlayer musicPlayer;

    void Start()
    {
        loadManager = LoadManager.instance;
        musicPlayer = MusicPlayer.instance;

        WaveSpawner.EnemiesAlive = 0;
        Time.timeScale = 1f;
        musicPlayer.PlayMusic();
    }

    public void ToggleState()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        menuUI.SetActive(!menuUI.activeSelf);

        if(menuUI.activeSelf)
        {
            Time.timeScale = 0f;
            musicPlayer.PauseMusic();
        }
        else
        {
            Time.timeScale = 1f;
            musicPlayer.PlayMusic();
        }
    }

    public void NextLevel()
    {
        loadManager.NextScene();
    }

    public void MainMenu()
    {
        loadManager.MenuScene();
    }

    public void Restart()
    {
        loadManager.ReloadScene();
    }

    public void Quit()
    {
        loadManager.Quit();
    }
}
