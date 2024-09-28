using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameWinUI;

    public static GameManager instance;

    [HideInInspector]
    public bool isOver = false;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        isOver = true;
        gameOverUI.SetActive(true);
    }

    public void Win()
    {
        Time.timeScale = 0f;
        isOver = true;
        gameWinUI.SetActive(true);
    }
}
