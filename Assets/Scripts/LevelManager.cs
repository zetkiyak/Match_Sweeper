using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    //public Transform LevelParent;
    //public TextMeshProUGUI LevelCountText;
    int nextLevelCount;
    public LevelSettings currentLevelSettings;
    private void Awake()
    {
        if (!instance)
            instance = this;

    }

    //private void Start()
    //{
    //    for (int i = 0; i < LevelParent.childCount; i++)
    //    {
    //        LevelParent.GetChild(i).gameObject.SetActive(false);
    //    }
    //}
    //public void LevelTextControl()
    //{
    //    int levelTextCount = PlayerPrefs.GetInt("LevelCount") + 1;
    //    //LevelCountText.text = "LEVEL " + levelTextCount;

    //}
    //public void NextLevel()
    //{
    //    if (LevelParent.childCount <= PlayerPrefs.GetInt("nextLevel"))
    //    {
    //        PlayerPrefs.SetInt("nextLevel", 0);
    //        nextLevelCount = PlayerPrefs.GetInt("nextLevel");
    //    }
    //    else
    //    {
    //        nextLevelCount = PlayerPrefs.GetInt("nextLevel");

    //    }
    //    LevelParent.GetChild(nextLevelCount).gameObject.SetActive(true);
    //}

}
