using System.Collections;
using System.Collections.Generic;
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
    private void Start()
    {
        levelManager = LevelManager.instance;
        tile = GetComponent<Tile>();
        gridSystem = GridSystem.instance;
    }
    public void FindClosestTile(RectTransform rectTransform)
    {
        if (tile.isActive == true)
        {
            Vector2 dragPos = rectTransform.position;
            minDistance = float.MaxValue;

            foreach (Transform tileTransform in GridSystem.instance.transform)                         //list ile degissstirr
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
    public void Match(bool endDrag)//pair match
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
                    gridSystem.tiles.Remove(neighbors[i].gameObject.GetComponent<Tile>());
                    neighbors[i].GetComponent<Tile>().open();
                }
            }
        }
        // WRONG MATCH
        if (minDistance > 50)
            endDrag = true;

    }

    public List<GameObject> GetNeighbors(Vector2Int position)
    {
        List<GameObject> neighbors = new List<GameObject>();

        for (int xOffset = -1; xOffset <= 1; xOffset++)
        {
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                if (xOffset == 0 && yOffset == 0)
                    continue; // Kendi koordinatýný dahil etme

                int neighborX = position.x + xOffset;
                int neighborY = position.y + yOffset;


                if (IsValidTilePosition(neighborX, neighborY))
                {
                    GameObject neighborPos = gridSystem.GetTileMyPos(neighborX, neighborY);
                    Debug.Log(neighborPos);
                    if (neighborPos != null && neighborPos.GetComponent<Tile>().isActive == false)
                    {
                        neighbors.Add(neighborPos);

                    }
                }
            }
        }

        return neighbors;
    }

    //Verilen koordinatlar geçerli bir tile pozisyonu mu kontrol eder
    private bool IsValidTilePosition(int x, int y)//grid sistemi gönderilecek
    {
        return x >= 0 && x < levelManager.currentLevelSettings._gridSize.x && y >= 0 && y < levelManager.currentLevelSettings._gridSize.y;
    }
}
