using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class ShipPart : MonoBehaviour{

    public string nameOfPart = "";
    public string typeOfPart = "";
    public float durability = 0;
    public float thrustForse = 0;
    public bool hasCollided = false;

    public void destroyShipPart()
    {
        Object.Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "planet")
        {
            hasCollided = !hasCollided;
        }
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
