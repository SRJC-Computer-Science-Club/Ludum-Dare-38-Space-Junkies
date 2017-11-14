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

        // itemList.shipPartList[i].transform.parent =


        for (int i = 0; i < itemList.shipPartList.Length; i++)
        {
            listOfPanels[i] = Instantiate(panel, this.transform);    
            Instantiate(itemList.shipPartList[i], listOfPanels[i].transform);
            listOfPanels[i].GetComponent<DropHandler>().targetItemType 
                = itemList.shipPartList[i].GetComponent<DragHandler>().itemType;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
