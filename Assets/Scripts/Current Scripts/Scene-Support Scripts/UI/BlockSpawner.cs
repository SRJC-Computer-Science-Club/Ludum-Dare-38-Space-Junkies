using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {

    private ItemDataBase itemListAll;
    private ItemDataBase itemListCurrent;

    [SerializeField]
    private  GameObject panel;

    private GameObject[] listOfPanels;

    // Use this for initialization
    void Start ()
    {
        itemListAll = GetComponent<ItemDataBase>();
        itemListCurrent = itemListAll;
        listOfPanels = new GameObject[itemListAll.shipPartList.Length];

        //Because the spawnAll function is setup to clear the list before spawning
        //the peices, a stand alone for loop is ran for initialization.
        for (int i = 0; i < itemListAll.shipPartList.Length; i++)
        {
            listOfPanels[i] = Instantiate(panel, this.transform);    
            Instantiate(itemListAll.shipPartList[i], listOfPanels[i].transform);
            listOfPanels[i].GetComponent<DropHandler>().targetItemType 
                = itemListAll.shipPartList[i].GetComponent<DragHandler>().itemType;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnAll()
    {
        clearList();

        for (int i = 0; i < itemListAll.shipPartList.Length; i++)
        {
            listOfPanels[i] = Instantiate(panel, this.transform);
            Instantiate(itemListAll.shipPartList[i], listOfPanels[i].transform);
            listOfPanels[i].GetComponent<DropHandler>().targetItemType
                = itemListAll.shipPartList[i].GetComponent<DragHandler>().itemType;
        }
    }


    public void spawnBodies()
    {
        clearList();

        for (int i = 0; i < itemListAll.shipPartList.Length; i++)
        {
            if (itemListAll.shipPartList[i].GetComponent<DragHandler>().itemType == DragHandler.SlotType.BODY) 
            {
                listOfPanels[i] = Instantiate(panel, this.transform);
                Instantiate(itemListAll.shipPartList[i], listOfPanels[i].transform);
                listOfPanels[i].GetComponent<DropHandler>().targetItemType
                    = itemListAll.shipPartList[i].GetComponent<DragHandler>().itemType;
            } 
        }
    }


    public void spawnWings()
    {
        clearList();

        for (int i = 0; i < itemListAll.shipPartList.Length; i++)
        {
            if (itemListAll.shipPartList[i].GetComponent<DragHandler>().itemType == DragHandler.SlotType.LEFTWING
                || itemListAll.shipPartList[i].GetComponent<DragHandler>().itemType == DragHandler.SlotType.RIGHTWING)
            {
                listOfPanels[i] = Instantiate(panel, this.transform);
                Instantiate(itemListAll.shipPartList[i], listOfPanels[i].transform);
                listOfPanels[i].GetComponent<DropHandler>().targetItemType
                    = itemListAll.shipPartList[i].GetComponent<DragHandler>().itemType;
            }
        }
    }


    private void clearList()
    {
        
        if(listOfPanels.Length > 0)
        {
            for (int i = 0; i < listOfPanels.Length; i++)
            {
                Destroy(listOfPanels[i].gameObject);
            }
        }
    }       
}
