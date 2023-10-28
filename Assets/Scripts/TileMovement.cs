using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileMovement : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rectTransform;
    private Vector2 startDragPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        startDragPos = this.transform.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //if (GetComponent<Tile>().isActive == true)
        rectTransform.SetAsLastSibling();

    }


    Transform closestTile = null;
    float minDistance;
    float distance;
    private void FindClosestTile()
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
            }
        }

        //  }



    }


    public void OnDrag(PointerEventData eventData)
    {
        //if (GetComponent<Tile>().isActive == true)
        // {
        rectTransform.anchoredPosition += eventData.delta / GridSystem.instance.canvas.scaleFactor;
        FindClosestTile();

        //}
    }

    #region End Drag
    public bool endDrag;
    public void OnEndDrag(PointerEventData eventData)
    {
        endDrag = true;
    }
    private void Update()
    {
        //if (GetComponent<Tile>().isActive == true)
        MoveStartPos();
    }

    private void MoveStartPos()
    {
        if (endDrag)
        {
            transform.position = Vector3.MoveTowards(transform.position, startDragPos, 3);
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);

            if (pos == startDragPos)
                endDrag = false;
        }
    }
    #endregion

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(distance);//distance da sýkýntý var
        if (closestTile != null && distance < 300) /*&& GetComponent<Tile>().isActive == true*/
        {
            if (closestTile.GetComponent<Tile>().id == this.GetComponent<Tile>().id)
            {
                // Burada en yakýn tile ile yapmak istediðiniz iþlemleri gerçekleþtirebilirsiniz.
                Debug.Log(closestTile.name);
                closestTile.gameObject.SetActive(false);
                this.gameObject.SetActive(false);

            }
        }
    }

}
