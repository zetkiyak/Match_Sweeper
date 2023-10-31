using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform LevelParent;
    //public TextMeshProUGUI LevelCountText;
    public int currentLevelCount;
    public LevelSettings currentLevelSettings;

    private void Awake()
    {
        if (!instance)
            instance = this;

    }

    private void Start()
    {
        for (int i = 0; i < LevelParent.childCount; i++)
        {
            LevelParent.GetChild(i).gameObject.SetActive(false);
        }

        OpenLevel();
    }
    public void LevelTextControl()
    {
        int levelTextCount = PlayerPrefs.GetInt("LevelCount") + 1;
        //LevelCountText.text = "LEVEL " + levelTextCount;

    }
    public void OpenLevel()
    {
        if (LevelParent.childCount <= PlayerPrefs.GetInt("nextLevel", 0))
            currentLevelCount = PlayerPrefs.GetInt("nextLevel", 0);
        else
            currentLevelCount = PlayerPrefs.GetInt("nextLevel", 0);

        LevelParent.GetChild(currentLevelCount).gameObject.SetActive(true);
    }

    [Button]
    public void LevelCompleted()
    {

        UIManager.instance.OpenWinPanel();

        if (LevelParent.childCount - 1 > currentLevelCount)
            PlayerPrefs.SetInt("nextLevel", currentLevelCount + 1);

        else
            PlayerPrefs.SetInt("nextLevel", 0);
    }

    public void GameOver()
    {
        UIManager.instance.OpenGameOverPanel();
    }

}
