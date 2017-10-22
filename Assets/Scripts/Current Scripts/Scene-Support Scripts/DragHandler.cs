/*
 The Drag Handler handles the dragging and placement of an inventory item.
 This script should be placed on the object you wish to be dragged.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

    public enum SlotType { BODY, LEFTWING, RIGHTWING };
    public SlotType itemType = SlotType.BODY;

    public Vector3 startPosition;

    private Transform startParent;
    private Transform canvas;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        transform.SetParent(canvas);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 move = Input.mousePosition;
        transform.position = new Vector3(move.x, move.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.position = startPosition;
        if (transform.parent == canvas)
        {
            transform.SetParent(startParent);
        }
    }
}
