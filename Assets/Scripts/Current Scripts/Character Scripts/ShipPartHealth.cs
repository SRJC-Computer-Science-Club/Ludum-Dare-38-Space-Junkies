//Must be placed on a valid ship part that has
//a ShipPart class attacked to it.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartHealth : MonoBehaviour {

    private float currentHealth = 0;
    private bool madeContact = false;

	void Awake()
    {
        currentHealth = transform.GetComponent<ShipPart>().durability;
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!madeContact)
        {
            currentHealth -= 0;
            madeContact = true;
            if (currentHealth <= 0)
            {
                transform.GetComponent<ShipPart>().thrustForse = 0;
                transform.GetComponent<ShipPart>().destroyShipPart();
            }
        }
    }
    // Update is called once per frame
    void Update ()
    {
		
	}
}
