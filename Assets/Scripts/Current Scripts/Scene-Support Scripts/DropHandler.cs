/*
  The Drop Handler determines whether or not the item being dragged should
  be dropped to into the specified drop zone.
  This script should be added to your drop zone.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler{

    [SerializeField]
    private DragHandler.SlotType targetItemType;

    public void OnDrop(PointerEventData eventData)
    {
        //Grabs the DragHandler object of the current item being dragged.
        DragHandler drag = eventData.pointerDrag.GetComponent<DragHandler>();

        
        if(drag != null && drag.itemType == targetItemType)
        {
            drag.startPosition = this.transform.position;
            drag.transform.SetParent(this.transform);
        }
    }
}
