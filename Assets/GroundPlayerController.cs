using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlayerController : MonoBehaviour
{
    private HingeJoint2D worldConnector;
    private RaycastHit2D castMaster;
    private Ray2D rayMaster;
    private Rigidbody2D planet;

	void Start ()
    {
        worldConnector = this.gameObject.GetComponent<HingeJoint2D>();
        worldConnector.anchor = new Vector2(this.transform.position.x, -this.transform.position.y);
    }
	
	
	void Update ()
    {
        characterMovement();
    }



    void characterMovement ()
    {
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();

        if (Input.GetKey (KeyCode.A))
        {
            rb.AddRelativeForce (new Vector2(-5, 0));
            Debug.Log("Right");
        }
        else if (Input.GetKey (KeyCode.D))
        {
            rb.AddRelativeForce(new Vector2(5, 0));
            Debug.Log("Left");
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(new Vector2(0, 10));
            Debug.Log("Jump");

            //findAnchorPoint();
        }
    }



    void findAnchorPoint ()
    {
        rayMaster.direction = new Vector2(0, -5);

        castMaster = Physics2D.Raycast(rayMaster.origin, rayMaster.direction);
        Transform planet = castMaster.transform;
        worldConnector.connectedAnchor = planet.position;

        worldConnector.anchor = new Vector2(0.0f, 1.0f);
    }
}
