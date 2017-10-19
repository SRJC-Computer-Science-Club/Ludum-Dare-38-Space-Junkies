/*
 The Drag Handler handles the dragging and placement of an inventory item.
 This script should be placed on the image you wish to be dragged.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

    private Vector3 startPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 move = Input.mousePosition;
        transform.position = new Vector3(move.x, move.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
