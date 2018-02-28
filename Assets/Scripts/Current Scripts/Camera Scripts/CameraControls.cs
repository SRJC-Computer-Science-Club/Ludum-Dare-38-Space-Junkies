using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public GameObject targetShip;
    public GameObject targetTom;
    public bool testingTom;


    private Vector3 offset;
    private ShipController shipController;
    private PlayerController playerController;

    
    void Start()
    {
        // Sets camera position to player ship position at start of game
        transform.position = new Vector3(targetShip.transform.position.x, targetShip.transform.position.y, transform.position.z);
        offset = transform.position - targetShip.transform.position;
    }

   
    void LateUpdate()
    {
        // Sets camera to follow Tom or the Ship. Update is called once per frame but the last in line compared to void Update()
        if (!ShipController.moveMan && !testingTom)
        {
            transform.position = targetShip.transform.position + offset;
        }
        else if (ShipController.moveMan || testingTom)
        {
            //Debug.Log("player is cameras target player")    
            targetTom = GameObject.FindGameObjectWithTag("Player");
            transform.position = new Vector3(targetTom.transform.position.x, targetTom.transform.position.y, -10.0f);
        }
    }
}
