using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlayerController : MonoBehaviour
{
    private HingeJoint2D worldConnector;
    private RaycastHit2D castMaster;
    private Ray2D rayMaster;
    //private Rigidbody2D planet;

	void Start ()
    {
        worldConnector = this.gameObject.GetComponent<HingeJoint2D>();
    }
	
	
	void Update ()
    {
        characterMovement();
        findAnchorPoint();
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

            findAnchorPoint();
        }
    }



    void findAnchorPoint ()
    {
        int counter = 1;

        for (int pow = 1; /*worldConnector.anchor == new Vector2(0.0f, 0.0f)*/ worldConnector.connectedBody == null; pow++)
        {
            if (counter < 3)
            {
                Debug.Log("Checking Y-axis");
                rayMaster.direction = new Vector2(0, Mathf.Pow(-5, pow));
                //Debug.DrawRay(this.transform.position, new Vector2(0, Mathf.Pow(-5, pow)), new Color32 (200, 200, 200, 255), 1000);
            }
            else if (counter > 6)
            {
                counter = 1;
            }
            else
            {
                Debug.Log("Checking X-axis");
                rayMaster.direction = new Vector2(Mathf.Pow(-5, pow), 0);
               // Debug.DrawRay(this.transform.position, new Vector2(Mathf.Pow(-5, pow), 0), new Color32(200, 200, 200, 255), 1000);
            }
            

            castMaster = Physics2D.Raycast(rayMaster.origin, rayMaster.direction);
            GameObject planet = castMaster.transform.gameObject;

            if (castMaster.transform)
            {
                Debug.Log("What did I hit " + castMaster.ToString () + " Cordo - " + planet.transform.position.x 
                          + " " + planet.transform.position.y);
                CircleCollider2D radiusFinder = planet.GetComponent <CircleCollider2D> ();
                float radius = radiusFinder.radius;


                worldConnector.anchor = new Vector2(worldConnector.connectedAnchor.x, -(radius));
                worldConnector.connectedBody = planet.GetComponent<Rigidbody2D>();
                this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                this.transform.position = new Vector2(0.0f, radius + (this.gameObject.GetComponent <CapsuleCollider2D> ().size.y / 2));
                worldConnector.anchor = new Vector2(worldConnector.connectedAnchor.x, -(this.transform.position.y));
                //worldConnector.anchor = this.transform.position;
            }
        }
    }
}
