using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {

        PlayerPrefs.SetInt("LevelCount", PlayerPrefs.GetInt("LevelCount"));
        PlayerPrefs.SetInt("nextLevel", PlayerPrefs.GetInt("nextLevel"));
        UIManager.instance.OpenGameOverPanel();

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}