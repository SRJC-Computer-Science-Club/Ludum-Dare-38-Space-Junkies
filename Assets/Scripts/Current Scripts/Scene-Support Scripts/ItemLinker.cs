using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLinker : MonoBehaviour {

    private Transform currentChild;
    public GameObject ship;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.childCount > 0 && this.transform.GetChild(0) != currentChild)
        {
            ship.GetComponent<PlayerControls>().changeShipPiece(transform.GetChild(0).GetComponent<DragHandler>().isLinkedTo);
        }
        updateChild();
	}


    private void updateChild()
    {
        currentChild = this.transform.GetChild(0);
    }
}
