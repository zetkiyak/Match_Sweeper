using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownSystem : MonoBehaviour
{
    GridSystem gridSystem;
    public List<Tile> nonEmptyTiles = new List<Tile>();

    void Start()
    {
        gridSystem = GridSystem.instance;

    }

   
    public void DropDown()
    {
        for (int i = 0; i < LevelManager.instance.currentLevelSettings._gridSize.x; i++)//i
        {
            for (int j = 0; j < LevelManager.instance.currentLevelSettings._gridSize.y; j++)
            {
                GameObject currentTile = gridSystem.GetTileByPos(i, j);
                if (currentTile != null && !nonEmptyTiles.Contains(currentTile.GetComponent<Tile>()))
                {
                    nonEmptyTiles.Add(currentTile.GetComponent<Tile>());
                }

            }
        }
    }
}
