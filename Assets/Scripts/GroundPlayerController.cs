using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlayerController : MonoBehaviour
{
    public float playerSpeed = 20;


    private RaycastHit2D castMaster;
    private BoxCollider2D boxMaster;
    private GameObject planet;
    private HingeJoint2D worldConnector;
    private Ray2D rayMaster;
    private int count;
    //private Rigidbody2D planet;

	void Start ()
    {
        boxMaster = this.GetComponent<BoxCollider2D>();
        worldConnector = this.gameObject.GetComponent<HingeJoint2D>();
        count = 0;
    }
	
	
	void Update ()
    {
        if (PlayerControls.moveMan == true)
        {
            planet = PlayerControls.landingSite;

            if (count == 0)
            {
                float xPlanet = planet.transform.position.x;
                float yPlanet = planet.transform.position.y;

                float distance = Mathf.Sqrt(Mathf.Pow(xPlanet - this.transform.position.x, 2) + Mathf.Pow(yPlanet - this.transform.position.y, 2));

                float angle = Mathf.Atan(this.transform.localPosition.y / this.transform.localPosition.x);
                float xCorr = Mathf.Abs (distance * Mathf.Cos(angle));
                float yCorr = Mathf.Abs (distance * Mathf.Sin(angle));

                this.transform.Rotate(0.0f, 0.0f, 0.0f);
                worldConnector.anchor = new Vector2(0.0f, -(yCorr));
                // original = new Vector2 (0.0f, -(distance));
                count++;
            }

            if (Input.GetKey(KeyCode.E))
            {
                Destroy(this.gameObject);
                PlayerControls.moveMan = false;
                PlayerControls.liftOff = 1;
                PlayerControls.isLanded = false;
                PlanetaryPull.crashed = false;
                count = 0;
                //PlayerControls.playerStop = false;
                //PlayerControls.stopSpawn = false;
            }

            characterMovement();   
        }
    }



    void characterMovement ()
    {
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();

        if (Input.GetKey (KeyCode.A))
        {
            rb.AddRelativeForce (new Vector2(-playerSpeed, 0));
            Debug.Log("Right");
        }
        else if (Input.GetKey (KeyCode.D))
        {
            rb.AddRelativeForce(new Vector2(playerSpeed, 0));
            Debug.Log("Left");
        }
    }
}
