using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlayerController : MonoBehaviour
{
    public float playerSpeed = 20f;
    public float thrustForce = 20f;
    public float rotationSpeed = 125f;
    public static bool onGround;
    //public bool testingMan; //Work on later
    public GameObject armRotationPoint;
    public GameObject projectilePrefab;
    
    private RaycastHit2D castMaster;
    private BoxCollider2D boxMaster;
    private GameObject planet;
    private Rigidbody2D tomRigidbody;
    private Ray2D rayMaster;
    private int count;
    private bool canJet;
    private bool inSpace;
    private float armTheta;
    private Quaternion setRotation;

	void Start ()
    {
        boxMaster = this.GetComponent<BoxCollider2D>();
        tomRigidbody = this.GetComponent<Rigidbody2D>();
        canJet = false;
        onGround = true;
        inSpace = false;
        count = 0;
    }
	
	
	void Update ()
    {
        if (PlayerControls.moveMan)
        {
            planet = PlayerControls.landingSite;

            // Launch into space off planet
            if (Input.GetKey(KeyCode.E))
            {
                GetInShip();               
            }

            // If movement keys pressed, move Tom either by ground or space.
            if (!inSpace)
            {
                MovementOnGround();
            }
            else
            {
                MovementInSpace();
            }

            ArmRotation();

            // Fires weapon on mouse button push
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
    }

    void GetInShip()
    {
        PlayerControls.moveMan = false;
        PlayerControls.liftOff = 1;
        PlayerControls.isLanded = false;
        PlanetaryPull.crashed = false;
        // PlayerControls.playerStop = false;
        // PlayerControls.stopSpawn = false;
        count = 0;
        Destroy(this.gameObject);
    }

    void MovementOnGround ()
    {
        if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow))
        {
            transform.Translate (Vector2.right * playerSpeed * Time.deltaTime);
        }
        else if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate (-Vector2.right * playerSpeed * Time.deltaTime);
        }
        
        if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) && !inSpace)
        {
            onGround = false;

            // GravitationalForces.totalForceReferance.magnitude + (GravitationalForces.totalForceReferance.magnitude / 2);
            float liftOffSpeed = 130;

            tomRigidbody.AddRelativeForce(Vector2.up * liftOffSpeed);
        }

        if (GravitationalForces.totalForceReferance.magnitude < 4.0f)
        {
            //rb.velocity = new Vector2(rb.velocity.x / 3, rb.velocity.y / 3);
            inSpace = true;
            tomRigidbody.velocity = new Vector3(0, 0, 0);
            //transform.rotation = setRotation;
            //Debug.Log("setRotation: " + setRotation + "\nRotation: " + transform.rotation);

        }

        setRotation = transform.rotation;
    }



    private void MovementInSpace ()
    {
        if (GravitationalForces.totalForceReferance.magnitude > 4.0f)
        {
            inSpace = false;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            tomRigidbody.transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            tomRigidbody.AddForce(Input.GetAxis("Vertical") * transform.up * thrustForce);
        }
    }



    private void ArmRotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y, mousePos.z);
        //Debug.Log("MousePos: " + mousePos);

        armTheta = (Mathf.Rad2Deg * Mathf.Atan2(mousePos.y, mousePos.x)) - 90.0f;
        //Debug.Log("Theta = " + armTheta);

        armRotationPoint.transform.rotation = Quaternion.Euler(0, 0, armTheta);
    }



    private void Fire()
    {
        Debug.Log("Tom has fired his weapon!");
        GameObject projectile = Instantiate(projectilePrefab, armRotationPoint.transform.position, Quaternion.Euler(0.0f, 0.0f, armTheta));
    }



    public void OnCollisionEnter2D (Collision2D col)
    {
        tomRigidbody.velocity = new Vector3 (0, 0, 0);
        onGround = true;
    }
}