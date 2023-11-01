using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;
    List<TileItem> currentTileIdentities = new List<TileItem>();
    GridSystem gridSystem;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gridSystem = GridSystem.instance;
    }
    public bool IsTileOver()
    {
        for (int j = 0; j < gridSystem.tiles.Count; j++)
        {
            Tile tile = gridSystem.tiles[j];
            if (tile.isActive == false)
            {
                return false;
            }
        }
        return true;
    }



    public void CheckGameOver()
    {
        currentTileIdentities.Clear();
        foreach (Tile item in gridSystem.tiles)
        {
            if (item.isActive)
            {
                string id = item.id;
                SearchTileIdentities(id);
            }
        }

        if(!IsPairMatch())
        {
            MatchManager.Instance.AllItemNotActive();
            LevelManager.instance.GameOver();
        }
    }

    public void SearchTileIdentities(string id) 
    {
        bool isThere = false;
        foreach (TileItem item in currentTileIdentities) 
        {
            if (item.id == id) 
            {
                item.count++;
                isThere = true;
                break;
            }
        }

        if(!isThere) 
        {
            TileItem tileItem = new TileItem();
            tileItem.id = id;
            tileItem.count = 1;
            currentTileIdentities.Add(tileItem);
        }
    }

    public bool IsPairMatch() 
    {
        foreach (var item in currentTileIdentities)
        {
            if(item.count>1)
            {
                return true;
            }
        }

        return false;
    }

}
