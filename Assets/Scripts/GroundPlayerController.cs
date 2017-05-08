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
        rb.drag = 5;

        if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow))
        {
            transform.Translate (Vector2.right * playerSpeed * Time.deltaTime);
            //this.transform.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * 10);
        }
        else if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate (-Vector2.right * playerSpeed * Time.deltaTime);
            //this.transform.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * 10);
        }
        
        if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) && grounded)
        {
            this.transform.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 170);
        }

        if (GravitationalForces.totalForceReferance.magnitude < 10.0f)
        {
            grounded = false;
        }
    }



    private void characterMovementSpaced ()
    {
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.drag = 0.2f;

        this.transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        this.GetComponent<Rigidbody2D>().AddForce(Input.GetAxis("Vertical") * transform.up * thrustForce);

        //if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        //{
        //    this.transform.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 200);
        //}
    }



    public void OnCollisionEnter2D (Collision2D col)
    {
        grounded = true;
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
