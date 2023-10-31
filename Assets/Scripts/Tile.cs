using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Sprite item;
    public string id;
    public Vector2Int Mypos;
    public Vector2Int MyStartPos;
    public bool isActive = false;


    public void ActivateTile()
    {
        isActive = true;
        SetItem();
        SetSprite(item);
    }
    private void SetItem()
    {
        id = ItemGenerator.Instance.GiveItemsToMatch();
        item = ItemGenerator.Instance.SearchSprite(id);
    }
    private void SetSprite(Sprite sprite)
    {
        this.GetComponent<Image>().sprite = LevelManager.instance.currentLevelSettings.openTileBackground;
        this.transform.GetChild(0).GetComponent<Image>().enabled = true;
        this.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
    }
}
