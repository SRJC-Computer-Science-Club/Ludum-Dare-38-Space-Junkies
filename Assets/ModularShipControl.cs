using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularShipControl : MonoBehaviour {

    public float rotationSpeed = 100;

    public Sprite[] shipParts;

    public float thrustForce;

    [SerializeField]
    private GameObject Body;
    [SerializeField]
    private GameObject LeftWing;
    [SerializeField]
    private GameObject RightWing;
    [SerializeField]
    private GameObject Thruster;

    public Sprite shipPeices
    {
        get
        {
            return shipPeices;
        }

        set
        {
            shipPeices = value;
        }
    }

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        GetComponent<Rigidbody2D>().AddForce(Input.GetAxis("Vertical") * transform.up * thrustForce);
    }
}
