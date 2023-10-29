using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rectTransform;
    private Vector2 startDragPos;

    public bool endDrag;
    MatchControl matchControl;

    private void Awake() => rectTransform = GetComponent<RectTransform>();
    private void Start()
    {
        startDragPos = this.transform.position;
        matchControl = GetComponent<MatchControl>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GetComponent<Tile>().isActive == true)
        {
            rectTransform.SetAsLastSibling();
            endDrag = false;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (GetComponent<Tile>().isActive == true)
        {
            rectTransform.anchoredPosition += eventData.delta / GridSystem.instance.canvas.scaleFactor;
            matchControl.FindClosestTile(rectTransform);

        }
    }
    public void OnDrop(PointerEventData eventData) => matchControl.CheckMatch(endDrag);

    private void FixedUpdate()
    {
        if (GetComponent<Tile>().isActive == true)
            MoveStartPos();


        if (Input.touchCount == 0)
            endDrag = true;


    }
    private void MoveStartPos()
    {
        if (endDrag)
        {
            transform.position = Vector3.MoveTowards(transform.position, startDragPos, 50);
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            if (pos == startDragPos)
                endDrag = false;
        }
    }

}
