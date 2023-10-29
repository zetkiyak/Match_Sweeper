using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Tile : MonoBehaviour
{
    public Sprite item;
    public string id;
    public Vector2Int Mypos;
    public bool isActive = false;

    public void SetItem(string itemID, Sprite itemSprite)
    {
        id = itemID;
        item = itemSprite;
    }
    public void open()
    {
        isActive = true;
        GetRandomIDAndSprite();
    }

    private void GetRandomIDAndSprite()
    {
        string id = ItemGenerator.Instance.getRandomItem();
        Sprite sprite = ItemGenerator.Instance.SearchSprite(id);
        SetItem(id, sprite);
        OpenSprite(sprite);
    }
    private void OpenSprite(Sprite sprite)
    {
        this.GetComponent<Image>().sprite = LevelManager.instance.currentLevelSettings.openTileBackground;
        this.transform.GetChild(0).GetComponent<Image>().enabled = true;
        this.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
    }
}
