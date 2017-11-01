/*
    Check UnityPackages/README if confused about the json/Newtonsoft stuff.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class ItemDataBase : MonoBehaviour {

    private string filePath = "";
    private string fileData = "";

    private int listSize = 0;
    
    public GameObject[] shipPartList;
    private void Start()
    {
        //filePath = Application.streamingAssetsPath + "/ShipParts.json";
        //fileData = File.ReadAllText(filePath);

        
        //ShipPart body = JsonUtility.FromJson<ShipPart>(fileData);
    }
}