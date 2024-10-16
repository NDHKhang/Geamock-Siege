using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameWinUI;

    public static GameManager instance;
    private MusicPlayer musicPlayer;

    [HideInInspector]
    public bool isOver = false;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    void Start()
    {
        musicPlayer = MusicPlayer.instance;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        isOver = true;
        musicPlayer.PauseMusic();
        gameOverUI.SetActive(true);
    }

    public void Win()
    {
        Time.timeScale = 0f;
        isOver = true;
        musicPlayer.PauseMusic();
        gameWinUI.SetActive(true);
    }
}
