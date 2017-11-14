using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class ShipPart : MonoBehaviour{

    public string nameOfPart = "";
    public string typeOfPart = "";
    public int durability = 0;

    public void destroyShipPart()
    {
        Object.Destroy(gameObject);
    }

    //This is something we might want to implement later, it's a more complex but effecent way of
    //doing what is above.
    /*
    private string name { get; set; }
    private string type { get; set; }
    private int durability { get; set; }

    public ShipPart()
    {
    }

    public ShipPart(string _Name, string _Type, int _Durability)
    {
        name = _Name;
        type = _Type;
        durability = _Durability;
    }
    */
}
