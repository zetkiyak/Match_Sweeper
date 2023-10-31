using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemGenerator : MonoBehaviour
{
    public List<TileItem> allItem = new List<TileItem>();
    public int itemCount;
    public int gridSizeX, gridSizeY;
    public TileData tileData;

    int tilesToOpenCount;
    int randomTile;
    int pairMatchControl = 0;
    int itemLenght;
    #region singleton
    public static ItemGenerator Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    private IEnumerator Start()//Start Fonksiyonu 2 frame geciktirildi. Bu sayede diðer startlar ile cakismasi onlendi.
    {
        yield return null;
        yield return null;

        gridSizeX = LevelManager.instance.currentLevelSettings._gridSize.x;
        gridSizeY = LevelManager.instance.currentLevelSettings._gridSize.y;
        tileData = LevelManager.instance.currentLevelSettings.tileData;
        tilesToOpenCount = LevelManager.instance.currentLevelSettings.tilesToOpen.Count;


        randomTile = ChooseRandomStartItem();
        Generate();

    }
    public void Generate()//6*10=60 , 60/4 =15 , 60%4=0
    {
        itemCount = gridSizeX * gridSizeY;
        int mod = itemCount % 4;
        int result = itemCount / 4;
        if (mod == 0)
        {
            AddTileItem(result, 4, 0);
        }
        else if (mod == 2)
        {
            AddTileItem(result, 4, 0);
            AddTileItem(result + 1, 2, result);
        }
    }

    public int ChooseRandomStartItem()
    {
        int rand = UnityEngine.Random.Range(1, tilesToOpenCount - 1);//3  1
        return rand;
    }
    public string GiveItemsToMatch()//pair match ihtimali kontrolu  
    {
        if (allItem.Count > 0)
        {
            if (pairMatchControl != randomTile)//3
                return GetRandomItem(true);
            else
                return GetRandomItem(false);
        }
        return null;
    }
    int index;
    private string GetRandomItem(bool control)
    {
        if (control)
            index = UnityEngine.Random.Range(0, allItem.Count);

        itemLenght = allItem[index].count;
        allItem[index].count--;
        pairMatchControl++;

        if (CheckAllItem(index, itemLenght))
            return allItem[index].id;
        else
            return GiveItemsToMatch();
    }

    public bool CheckAllItem(int index, int itemCount)
    {
        if (itemCount > 0)
            return true;
        else if (itemCount == 0)
            allItem.RemoveAt(index);

        return false;
    }
    public Sprite SearchSprite(string index)
    {
        foreach (var item in tileData.items)
        {
            if (item.id == index)
            {
                return item.sprite;
            }
        }
        return null;
    }
    private void AddTileItem(int result, int count, int startIndex)
    {
        for (int i = startIndex; i < result; i++)
        {
            TileItem newItem = new TileItem();
            newItem.id = tileData.items[i].id;
            newItem.count = count;
            allItem.Add(newItem);
        }
    }
}
[Serializable]
public class TileItem
{
    public string id;
    public int count;
}