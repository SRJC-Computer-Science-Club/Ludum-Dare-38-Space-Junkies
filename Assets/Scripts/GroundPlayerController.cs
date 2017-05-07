using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlayerController : MonoBehaviour
{
    public float playerSpeed = 20f;
    public float thrustForce = 20f;
    public float rotationSpeed = 100f;
    public static bool grounded;


    private RaycastHit2D castMaster;
    private BoxCollider2D boxMaster;
    private GameObject planet;
    private Rigidbody2D rb;
    private Ray2D rayMaster;
    private int count;
    private float timeToJetPack;
    private bool canJet;
    //private Rigidbody2D planet;

	void Start ()
    {
        boxMaster = this.GetComponent<BoxCollider2D>();
        rb = this.GetComponent<Rigidbody2D>();
        canJet = false;
        grounded = true;
        count = 0;
    }
	
	
	void Update ()
    {
        if (PlayerControls.moveMan == true)
        {
            planet = PlayerControls.landingSite;


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

            if (grounded)
            {
                characterMovementGrounded();
            }
            else
            {
                characterMovementSpaced();
            }   
        }
    }



    void characterMovementGrounded ()
    {
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();

        if (Input.GetKeyDown(KeyCode.W) && canJet && timeToJetPack - Time.time < 0.50f)
        {
            grounded = false;
            rb.drag = 0;
        }
        else if (timeToJetPack - Time.time > 0.50f)
        {
            canJet = false;
        }

        if (Input.GetKey (KeyCode.D))
        {
            transform.Translate (Vector2.right * playerSpeed * Time.deltaTime);
        }
        else if (Input.GetKey (KeyCode.A))
        {
             transform.Translate (-Vector2.right * playerSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown (KeyCode.W) && !canJet)
        {
            this.transform.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 200);
            timeToJetPack = Time.time;
            canJet = true;
        }
    }



    private void characterMovementSpaced ()
    {
        this.transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        this.GetComponent<Rigidbody2D>().AddForce(Input.GetAxis("Vertical") * transform.up * thrustForce);
    }



    public void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "planet" && !grounded)
        {
            rb.drag = 5;
            grounded = true;
        }
    }
}







// Old parts

//if (count == 0)
//            {
//                float xPlanet = planet.transform.position.x;
//float yPlanet = planet.transform.position.y;

//float distance = Mathf.Sqrt(Mathf.Pow(xPlanet - this.transform.position.x, 2) + Mathf.Pow(yPlanet - this.transform.position.y, 2));

//float angle = Mathf.Atan(this.transform.localPosition.y / this.transform.localPosition.x);
//float xCorr = Mathf.Abs(distance * Mathf.Cos(angle));
//float yCorr = Mathf.Abs(distance * Mathf.Sin(angle));

//                this.transform.Rotate(0.0f, 0.0f, 0.0f);
//worldConnector.anchor = new Vector2(0.0f, -(yCorr));
//                // original = new Vector2 (0.0f, -(distance));
//                count++;
//            }
