using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject winPanel, gameOverPanel;
    private void Awake()
    {
        instance = this;
    }
    public void OpenWinPanel()
    {
        winPanel.SetActive(true);
    }
    public void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
