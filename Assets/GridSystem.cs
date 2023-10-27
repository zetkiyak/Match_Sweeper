using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private int _height, _width;
    [SerializeField] private Tile _tile;
    [SerializeField] private float _offset;

    private void Start()
    {
        GenerateGridSystem();
    }
    public void GenerateGridSystem()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Vector2 pos = new Vector2(i * _offset, j * _offset);
                var tile = Instantiate(_tile,this.transform);
                tile.transform.localPosition = pos;
                tile.name = "(" + i + "," + j + ")";
            }
        }
    }
}
