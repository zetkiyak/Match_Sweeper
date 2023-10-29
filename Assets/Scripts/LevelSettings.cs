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
    public List<Vector2Int> tiles = new List<Vector2Int>();//1-1,1-2,3-2
    private void Awake()
    {
        gridSystem.GenerateGridSystem(_gridSize.x, _gridSize.y);
        levelManager.currentLevelSettings = this;

    }

    IEnumerator Start()
    {
        yield return null;
        yield return null;
        GetObject();

    }
    public void GetObject()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            GameObject obj = gridSystem.GetTileMyPos(tiles[i].x, tiles[i].y);
            obj.GetComponent<Tile>().open();
          

        }
    }
}
