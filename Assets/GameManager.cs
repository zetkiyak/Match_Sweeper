using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void win()
    {
        PlayerPrefs.SetInt("LevelCount", PlayerPrefs.GetInt("LevelCount") + 1);
        PlayerPrefs.SetInt("nextLevel", PlayerPrefs.GetInt("nextLevel") + 1);

        UIManager.instance.OpenWinPanel();
    }

    public void GameOver()
    {

        PlayerPrefs.SetInt("LevelCount", PlayerPrefs.GetInt("LevelCount"));
        PlayerPrefs.SetInt("nextLevel", PlayerPrefs.GetInt("nextLevel"));

        UIManager.instance.OpenGameOverPanel();


    }
}