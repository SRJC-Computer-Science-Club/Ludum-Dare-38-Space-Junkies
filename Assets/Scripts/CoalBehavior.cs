using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalBehavior : MonoBehaviour
{
    private float fuelAmount = 5;



    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("You got " + fuelAmount + " fuel");
        Destroy (this.gameObject);
    }

	
	
	
}
