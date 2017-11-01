using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {

    private ItemDataBase itemList;
    [SerializeField]
    private  GameObject panel;

    private GameObject[] listOfPanels;
	// Use this for initialization
	void Start ()
    {
        itemList = GetComponent<ItemDataBase>();
        listOfPanels = new GameObject[itemList.shipPartList.Length];

        for (int i = 0; i < itemList.shipPartList.Length; i++)
        {
            listOfPanels[i] = Instantiate(panel, panel.transform.parent = this.transform);    
            Instantiate(itemList.shipPartList[i], itemList.shipPartList[i].transform.parent = listOfPanels[i].transform);
            listOfPanels[i].GetComponent<DropHandler>().targetItemType 
                = itemList.shipPartList[i].GetComponent<DragHandler>().itemType;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
