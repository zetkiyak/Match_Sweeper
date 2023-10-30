using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettings : MonoBehaviour
{
    public Vector2Int _gridSize;
    public TileData tileData;
    public GridSystem gridSystem;
    public LevelManager levelManager;
    public Sprite closedTileBackground, openTileBackground;
    public List<Vector2Int> tilesToOpen = new List<Vector2Int>();
    private void Awake()
    {
        gridSystem.GenerateGridSystem(_gridSize.x, _gridSize.y);
        levelManager.currentLevelSettings = this;
    }
}
