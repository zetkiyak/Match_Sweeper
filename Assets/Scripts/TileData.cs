using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Data/TileData")]
public class TileData : ScriptableObject
{
    public Item[] items;
}
[Serializable]
public class Item
{
    public string id;
    public Sprite sprite;
}