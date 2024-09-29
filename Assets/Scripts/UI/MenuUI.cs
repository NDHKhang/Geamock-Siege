using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    LoadManager loadManager;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject pauseUI;

    void Start()
    {
        loadManager = LoadManager.instance;
    }

    public void ToggleState()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        menuUI.SetActive(!menuUI.activeSelf);

        if(menuUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Restart()
    {
        if(loadManager != null)
        {
            WaveSpawner.EnemiesAlive = 0;
            Time.timeScale = 1f;
            loadManager.ReloadScene();
        }
    }

    public void Quit()
    {
        loadManager.Quit();
    }
}
