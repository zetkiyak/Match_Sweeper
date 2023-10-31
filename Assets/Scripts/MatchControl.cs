using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class MatchControl : MonoBehaviour
{
    Transform closestTile;
    float minDistance;
    float distance;
    LevelManager levelManager;
    Tile tile;
    GridSystem gridSystem;
    TileMovement tileMovement;
    private void Start()
    {
        levelManager = LevelManager.instance;
        tile = GetComponent<Tile>();
        gridSystem = GridSystem.instance;
        tileMovement = GetComponent<TileMovement>();
    }
    public void FindClosestTile(RectTransform rectTransform)
    {
        if (tile.isActive == true)
        {
            Vector2 dragPos = rectTransform.position;
            minDistance = float.MaxValue;

            foreach (Transform tileTransform in GridSystem.instance.transform)
            {
                if (tileTransform != transform)
                {
                    Vector2 tilePos = tileTransform.position;
                    distance = Vector2.Distance(dragPos, tilePos);
                    if (distance < minDistance)
                    {

                        minDistance = distance;
                        closestTile = tileTransform;

                    }
                }
            }

        }
    }
    public void CheckMatch(bool endDrag)
    {
        //  CORRECT MATCH
        if (closestTile != null && minDistance < 50 && tile.isActive == true)
        {
            if (closestTile.GetComponent<Tile>().id == tile.id)
            {
                gridSystem.tiles.Remove(closestTile.gameObject.GetComponent<Tile>());
                gridSystem.tiles.Remove(tile);

                Destroy(closestTile.gameObject);
                Destroy(this.gameObject);

                var neighbors = GetNeighbors(closestTile.GetComponent<Tile>().Mypos);
                for (int i = 0; i < neighbors.Count; i++)
                {
                    //gridSystem.tiles.Remove(neighbors[i].gameObject.GetComponent<Tile>());
                    neighbors[i].GetComponent<Tile>().ActivateTile();
                }
            }
        }
        // WRONG MATCH
        if (minDistance > 50)
            endDrag = true;


        gridSystem.ResortOpr();
        DOVirtual.DelayedCall(2, () =>
        {
            AllItemMatch();
        });

    }
    public async void AllItemMatch()
    {
        if (TileManager.Instance.IsTileOver())
        {
            for (int i = 0; i < gridSystem.tiles.Count; i++)
            {

                string id = gridSystem.tiles[i].id;
                if (!gridSystem.tiles[i].gameObject.activeInHierarchy)
                    continue;


                for (int j = i + 1; j < gridSystem.tiles.Count; j++)
                {

                    if (!gridSystem.tiles[j].gameObject.activeInHierarchy)
                        continue;

                    if (id == gridSystem.tiles[j].id)
                    {
                        Tile tile1 = gridSystem.tiles[i];
                        Tile tile2 = gridSystem.tiles[j];

                        tile1.gameObject.GetComponent<TileMovement>().resortActive = true;
                        tile1.gameObject.transform.SetAsLastSibling();
                        await tile1.gameObject.transform.DOMove(tile2.transform.position, 0.3f).AsyncWaitForCompletion();


                        tile1.gameObject.GetComponent<TileMovement>().resortActive = false;
                        tile1.gameObject.SetActive(false);
                        tile2.gameObject.SetActive(false);
                        break;
                    }
                }
            }

        }
    }

    public List<GameObject> GetNeighbors(Vector2Int position)
    {
        List<GameObject> neighbors = new List<GameObject>();

        for (int xOffset = -1; xOffset <= 1; xOffset++)
        {
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                if (xOffset == 0 && yOffset == 0)
                    continue;

                int neighborX = position.x + xOffset;
                int neighborY = position.y + yOffset;


                if (IsValidTilePosition(neighborX, neighborY))
                {
                    GameObject neighborPos = gridSystem.GetTileByPos(neighborX, neighborY);
                    if (neighborPos != null && neighborPos.GetComponent<Tile>().isActive == false)
                    {
                        neighbors.Add(neighborPos);

                    }
                }
            }
        }

        return neighbors;
    }

    private bool IsValidTilePosition(int x, int y)
    {
        return x >= 0 && x < levelManager.currentLevelSettings._gridSize.x && y >= 0 && y < levelManager.currentLevelSettings._gridSize.y;
    }
}
