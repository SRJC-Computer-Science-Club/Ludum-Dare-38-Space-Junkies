using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    [SerializeField]
    private GameObject shipMenu;
    private bool setShipMenu = true;
	// Use this for initialization
	void Start () {
        //setShipMenu = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M) && setShipMenu)
        {
            shipMenu.SetActive(setShipMenu);
            Time.timeScale = 0;
            setShipMenu = !setShipMenu;
        }
        else if (Input.GetKeyDown(KeyCode.M) && !setShipMenu)
        {
            shipMenu.SetActive(setShipMenu);
            Time.timeScale = 1;
            setShipMenu = !setShipMenu;
        }
	}
}
