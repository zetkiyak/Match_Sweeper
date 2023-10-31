using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private Tile _tile;
    [SerializeField] private float _scale;
    public float offset;
    public Canvas canvas;
    public List<Tile> tiles = new List<Tile>();
    LevelSettings levelSettings;
    TileMovement tileMovement;
    #region Instance
    public static GridSystem instance;
    private void Awake()
    {
        if (!instance)
            instance = this;
    }
    #endregion
    private void Start()
    {
        levelSettings = LevelManager.instance.currentLevelSettings;
    }
    public void GenerateGridSystem(int _width, int _height)
    {
        Vector3 tileScale;
        float startX, startY;
        CalculateGridParameters(out tileScale, out startX, out startY, _width, _height);

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Vector2 pos = new Vector2(startX + i * offset, startY - j * offset);
                var tile = Instantiate(_tile, this.transform);
                tile.transform.localPosition = pos;

                tile.name = "(" + i + "," + j + ")";
                tile.Mypos = new Vector2Int(i, j);
                tile.transform.localScale = tileScale;
                tiles.Add(tile);

            }
        }
    }
    [Button]
    public void ResortOpr()
    {
        int startedYValue = levelSettings._gridSize.y - 1;
        int startedXValue = levelSettings._gridSize.x - 1;
        int loopCount = 0;//54321

        for (int i = tiles.Count - 1; i >= 0; i--)
        {
            if (tiles[i].Mypos.x != startedXValue)
            {
                loopCount = 0;
                startedXValue = tiles[i].Mypos.x;
                startedYValue = levelSettings._gridSize.y - 1;
            }

            Vector2Int currentPosValue = tiles[i].Mypos;

            currentPosValue.y = (startedYValue - loopCount);
            loopCount++;
            tiles[i].MyStartPos = tiles[i].Mypos;
            tiles[i].Mypos = currentPosValue;
            tileMovement = tiles[i].GetComponent<TileMovement>();
            tileMovement.ResortMovement();
        }
    }

    private void CalculateGridParameters(out Vector3 tileScale, out float startX, out float startY, int _width, int _height)
    {
        offset = _scale * 100;

        float gridWidth = _width * offset;
        float gridHeight = _height * offset;

        tileScale = new Vector3(_scale, _scale, 1f);

        RectTransform panelRect = this.transform as RectTransform;
        Vector2 panelSize = panelRect.rect.size;

        startX = -gridWidth / 2 + offset / 2;
        startY = gridHeight / 2 - offset / 2;
    }

    public GameObject GetTileByPos(int x, int y)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.Mypos.x == x && tile.Mypos.y == y)
            {
                return tile.gameObject;
            }
        }
        return null;
    }

}
