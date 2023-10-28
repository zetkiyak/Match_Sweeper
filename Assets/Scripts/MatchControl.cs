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
    public void FindClosestTile(RectTransform rectTransform)
    {
        //if (GetComponent<Tile>().isActive == true)
        //{
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
                //Debug.Log("En yakýn obje: " + closestTile.name + ", Mesafe: " + minDistance);
            }
        }

        //}
    }
    public void Match(bool endDrag)
    {
        // CORRECT MATCH
        Debug.Log(closestTile);
        if (closestTile != null && minDistance < 50/* && GetComponent<Tile>().isActive == true*/)
        {
            if (closestTile.GetComponent<Tile>().id == this.GetComponent<Tile>().id)
            {
                //Debug.Log("en yakin obje : " + closestTile.name);
                GridSystem.instance.tiles.Remove(closestTile.gameObject.GetComponent<Tile>());
                Destroy(closestTile.gameObject);
                GridSystem.instance.tiles.Remove(this.GetComponent<Tile>());
                Destroy(this.gameObject);
                var a = GetNeighbors(closestTile.GetComponent<Tile>().Mypos);
                for (int i = 0; i < a.Count; i++)
                {
                    GridSystem.instance.tiles.Remove(a[i].gameObject.GetComponent<Tile>());
                    Destroy(a[i]);
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

                // Koordinat sýnýrlarýný kontrol etmek önemlidir.
                if (IsValidTilePosition(neighborX, neighborY))
                {
                    GameObject neighborPos = GridSystem.instance.GetTileMyPos(neighborX, neighborY);
                    if (neighborPos != null)
                    {
                        neighbors.Add(neighborPos);
                    }
                }
            }
        }

        return neighbors;
    }

    // Verilen koordinatlar geçerli bir tile pozisyonu mu kontrol eder
    private bool IsValidTilePosition(int x, int y)//grid sistemi gönderilecek
    {
        return x >= 0 && x < 6 && y >= 0 && y < 10;
    }


}
