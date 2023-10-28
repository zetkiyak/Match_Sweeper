using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private Tile _tile;
    [SerializeField] private float _offset;
    [SerializeField] private float _scale;
    public Canvas canvas;

    #region Instance
    public static GridSystem instance;
    private void Awake()
    {
        if (!instance)
            instance = this;
    }
    #endregion

    public void GenerateGridSystem(int _width, int _height)
    {
        Vector3 tileScale;
        float startX, startY;
        CalculateGridParameters(out tileScale, out startX, out startY, _width, _height);

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Vector2 pos = new Vector2(startX + i * _offset, startY - j * _offset);
                var tile = Instantiate(_tile, this.transform);
                tile.transform.localPosition = pos;

                tile.name = "(" + i + "," + j + ")";
                tile.Mypos = new Vector2Int(i, j);                
                tile.transform.localScale = tileScale;
            }
        }
    }

    private void CalculateGridParameters(out Vector3 tileScale, out float startX, out float startY, int _width, int _height)
    {
        float gridWidth = _width * _offset;
        float gridHeight = _height * _offset;

        _offset = _scale * 100;

        tileScale = new Vector3(_scale, _scale, 1f);

        RectTransform panelRect = this.transform as RectTransform;
        Vector2 panelSize = panelRect.rect.size;

        startX = -gridWidth / 2 + _offset / 2;
        startY = gridHeight / 2 - _offset / 2;
    }

}

