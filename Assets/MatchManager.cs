using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    LevelSettings levelSettings;
    GridSystem gridSystem;
    IEnumerator Start()
    {
        yield return null;
        yield return null;

        levelSettings = LevelManager.instance.currentLevelSettings;
        gridSystem = GridSystem.instance;

        GetStartItem();
    }

    public void GetStartItem()
    {
        for (int i = 0; i < levelSettings.tilesToOpen.Count; i++)
        {
            GameObject obj = gridSystem.GetTileByPos(levelSettings.tilesToOpen[i].x, levelSettings.tilesToOpen[i].y);
            obj.GetComponent<Tile>().ActivateTile();
        }
    }
}
