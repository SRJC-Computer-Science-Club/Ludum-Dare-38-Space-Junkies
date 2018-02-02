using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalBehavior : MonoBehaviour
{
    private float fuelAmount = 20;
    private ShipController shipController;


    public void Start ()
    {
        shipController = GameObject.FindGameObjectWithTag("ship").GetComponent<ShipController>();
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("You got " + fuelAmount + " fuel. You now have: " + shipController.getFuel());

        if (shipController.getFuel() + 5.0f <= 100)
        {
            shipController.setFuel(shipController.getFuel() + fuelAmount);
        }

        Destroy (this.gameObject);
    }

	
	
	
}
