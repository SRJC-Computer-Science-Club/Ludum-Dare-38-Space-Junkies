using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {
    public Camera RocketCamera;
    public Camera SpaceManCamera;


    private ShipController shipController;

	
    public void Start ()
    {
        shipController = GameObject.FindGameObjectWithTag("ship").GetComponent<ShipController>();
        RocketCamera.enabled = false;
        SpaceManCamera.enabled = false;
    }

	// Update is called once per frame
	void Update ()
    {
        if (PlayerControls.playerLeavesShip == false)
        {
            if (shipController.getMoveMan() == false)
            {
                RocketCamera.enabled = true;
                SpaceManCamera.enabled = false;
            }
            if (PlayerControls.playerLeavesShip == true)
            {
                if (shipController.getMoveMan() == true)
                {
                    RocketCamera.enabled = false;
                    SpaceManCamera.enabled = true;
                }
            }
        }
	}
}
