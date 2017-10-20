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

    public Vector3 startPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 move = Input.mousePosition;
        transform.position = new Vector3(move.x, move.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
