using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;
using DG.Tweening;
public class TileMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rectTransform;
    private Vector2 startDragPos;
    public bool resortActive;
    public bool endDrag;
    MatchControl matchControl;
    public bool clicked;
    private void Awake() => rectTransform = GetComponent<RectTransform>();
    private void Start()
    {
        startDragPos = this.transform.position;
        matchControl = GetComponent<MatchControl>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GetComponent<Tile>().isActive == true && !resortActive)
        {
            rectTransform.SetAsLastSibling();
            endDrag = false;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (GetComponent<Tile>().isActive == true && !resortActive)
        {
            rectTransform.anchoredPosition += eventData.delta / GridSystem.instance.canvas.scaleFactor;
            matchControl.FindClosestTile(rectTransform);

        }
    }
    public void OnDrop(PointerEventData eventData) => matchControl.CheckMatch(endDrag);
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            clicked = true;

        if (Input.GetMouseButtonUp(0))
            clicked = false;

    }
    private void FixedUpdate()
    {
        if (GetComponent<Tile>().isActive == true && !resortActive)
            MoveStartPos();


        if (!clicked && !resortActive)
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
    [Button]
    public void ResortMovement()//2.-4.10
    {
        if (GetComponent<Tile>().Mypos != GetComponent<Tile>().MyStartPos)
        {
            resortActive = true;
            Tile tile = this.GetComponent<Tile>();
            int diff = tile.MyStartPos.y - tile.Mypos.y;
            this.transform.DOLocalMoveY((this.transform.localPosition.y + (GridSystem.instance.offset * diff)), 0.25f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                startDragPos = this.transform.position;
                resortActive = false;
            });
        }

    }


}