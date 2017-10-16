using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlayerController : MonoBehaviour
{
    public float playerSpeed = 20f;
    public float thrustForce = 20f;
    public float rotationSpeed = 125f;
    public static bool grounded;
    public bool testingMan;
    public GameObject armRotationPoint;
    public GameObject projectilePrefab;
    


    private RaycastHit2D castMaster;
    private BoxCollider2D boxMaster;
    private GameObject planet;
    private Rigidbody2D rb;
    private Ray2D rayMaster;
    private int count;
    private bool canJet;
    private bool spaced;
    private float armTheta;
    private Quaternion setRotation;
    //private Rigidbody2D planet;

	void Start ()
    {
        boxMaster = this.GetComponent<BoxCollider2D>();
        rb = this.GetComponent<Rigidbody2D>();
        canJet = false;
        grounded = true;
        spaced = false;
        count = 0;
    }
	
	
	void Update ()
    {
        if (PlayerControls.moveMan || testingMan)
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

            if (!spaced)
            {
                characterMovementGrounded();
            }
            else
            {
                characterMovementSpaced();
            }

            Debug.Log("Magnitude: " + GravitationalForces.totalForceReferance.magnitude);
            armRotation();
            fire();
        }
    }



    void characterMovementGrounded ()
    {
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();

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
        
        if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) && !spaced)
        {
            grounded = false;

            float liftOffSpeed = 130;//GravitationalForces.totalForceReferance.magnitude + (GravitationalForces.totalForceReferance.magnitude / 2);

            this.transform.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * liftOffSpeed);
        }

        if (GravitationalForces.totalForceReferance.magnitude < 4.0f)
        {
            //rb.velocity = new Vector2(rb.velocity.x / 3, rb.velocity.y / 3);
            spaced = true;
            rb.velocity = new Vector3(0, 0, 0);
            //transform.rotation = setRotation;
            //Debug.Log("setRotation: " + setRotation + "\nRotation: " + transform.rotation);

        }

        setRotation = transform.rotation;
    }



    private void characterMovementSpaced ()
    {
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();

        if (GravitationalForces.totalForceReferance.magnitude > 4.0f)
        {
            spaced = false;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            this.GetComponent<Rigidbody2D>().AddForce(Input.GetAxis("Vertical") * transform.up * thrustForce);
        }

        //if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        //{
        //    this.transform.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 200);
        //}
        //Debug.Log("Rotation: " + transform.rotation);
    }


    private void armRotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y, mousePos.z);
        Debug.Log("MousePos: " + mousePos);

        armTheta = (Mathf.Rad2Deg * Mathf.Atan2(mousePos.y, mousePos.x)) - 90.0f;
        Debug.Log("Theta = " + armTheta);

        armRotationPoint.transform.rotation = Quaternion.Euler(0, 0, armTheta);
    }


    private void fire()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            GameObject projectile = Instantiate(projectilePrefab, armRotationPoint.transform.position, Quaternion.Euler(0.0f, 0.0f, armTheta));
            projectile.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 10);
        }
    }



    public void OnCollisionEnter2D (Collision2D col)
    {
        rb.velocity = new Vector3 (0, 0, 0);
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
