/*
  This script should be added to your drop zone.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler{

    [SerializeField]
    private String targetObject = "";

    public void OnDrop(PointerEventData eventData)
    {
        //Grabs the DragHandler object of the current item being dragged.
        DragHandler drag = eventData.pointerDrag.GetComponent<DragHandler>();

        if(drag != null && eventData.pointerDrag.gameObject.tag == targetObject)
        {
                drag.startPosition = this.transform.position;
                drag.transform.SetParent(this.transform);
        }
    }
}
