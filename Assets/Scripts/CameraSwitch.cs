﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {
    public Camera RocketCamera;
    public Camera SpaceManCamera;
	
    public void Start ()
    {
        RocketCamera.enabled = false;
        SpaceManCamera.enabled = false;
    }

	// Update is called once per frame
	void Update ()
    {
        if (PlayerControls.moveMan == false) 
        {
            RocketCamera.enabled = true;
            SpaceManCamera.enabled = false;
        }
        if (PlayerControls.moveMan == true)
        {
            RocketCamera.enabled = false;
            SpaceManCamera.enabled = true;
        } 
	}
}
