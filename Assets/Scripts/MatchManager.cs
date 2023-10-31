using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    LevelSettings levelSettings;
    GridSystem gridSystem;
    public static MatchManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    IEnumerator Start()
    {
        yield return null;
        yield return null;

        levelSettings = LevelManager.instance.currentLevelSettings;
        gridSystem = GridSystem.instance;

        GetStartItem();
    }

    public void GetStartItem()
    {
        for (int i = 0; i < levelSettings.tilesToOpen.Count; i++)
        {
            GameObject obj = gridSystem.GetTileByPos(levelSettings.tilesToOpen[i].x, levelSettings.tilesToOpen[i].y);
            obj.GetComponent<Tile>().ActivateTile();
        }

    }
    public void AllItemNotActive()
    {
        for (int i = 0; i < gridSystem.tiles.Count; i++)
        {
            gridSystem.tiles[i].GetComponent<TileMovement>().resortActive = true;
        }
    }
    public async void AllItemMatch()
    {
        if (TileManager.Instance.IsTileOver())
        {
            AllItemNotActive();

            for (int i = 0; i < gridSystem.tiles.Count; i++)
            {
                Tile tile1 = gridSystem.tiles[i];
                if (!gridSystem.tiles[i].gameObject.activeInHierarchy)
                    continue;


                for (int j = i + 1; j < gridSystem.tiles.Count; j++)
                {

                    if (!gridSystem.tiles[j].gameObject.activeInHierarchy)
                        continue;

                    if (tile1.id == gridSystem.tiles[j].id)
                    {
                        Tile tile2 = gridSystem.tiles[j];

                        tile1.gameObject.GetComponent<TileMovement>().resortActive = true;
                        tile1.gameObject.transform.SetAsLastSibling();
                        await tile1.gameObject.transform.DOMove(tile2.transform.position, 0.2f).AsyncWaitForCompletion();


                        tile1.gameObject.GetComponent<TileMovement>().resortActive = false;
                        tile1.gameObject.SetActive(false);
                        tile2.gameObject.SetActive(false);

                        break;
                    }
                }
            }
            LevelManager.instance.LevelCompleted();
        }
    }

}
