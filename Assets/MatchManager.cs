using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{

    LevelSettings levelSettings;
    GridSystem gridSystem;

    void Start()
    {

        levelSettings = LevelManager.instance.currentLevelSettings;
        gridSystem = GridSystem.instance;
        //GetObject();
    }

    //public void GetObject()
    //{
    //    for (int i = 0; i < levelSettings.tiles.Count; i++)
    //    {
    //        GameObject obj = gridSystem.GetTileByPos(levelSettings.tiles[i].x, levelSettings.tiles[i].y);
    //        obj.GetComponent<Tile>().ActivateTile();
    //    }
    //}
}
