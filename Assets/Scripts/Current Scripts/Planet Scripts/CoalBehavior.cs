using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalBehavior : MonoBehaviour
{
    private float fuelAmount = 20;



    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("You got " + fuelAmount + " fuel. You now have: " + ShipController.fuel);

        if (ShipController.fuel + 5.0f <= 100)
        {
            ShipController.fuel += fuelAmount;
        }
        Destroy (this.gameObject);
    }

	
	
	
}
