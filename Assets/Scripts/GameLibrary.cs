using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;
using UnityEngine.SceneManagement;

public enum ShotSources
{
    Player, Enemy
}
public enum ShotTypes
{
    Target, Direction
}

public class GameLibrary : MonoBehaviour
{
    public List<SplineContainer> randomSplines;
    public Transform playerPos;
    public int totalEnemies;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    //Singleton for getter
    private static GameLibrary instance;
    public static GameLibrary Instance { get { return instance; } }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void EnemyKilled()
    {
        totalEnemies -= 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }


    private void PauseGame()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
