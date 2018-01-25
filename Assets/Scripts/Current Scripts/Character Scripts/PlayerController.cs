using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 20f;
    public float thrustForce = 20f;
    public float rotationSpeed = 125f;
    public static bool onGround;
    //public bool testingMan; //Work on later
    public GameObject armRotationPoint;
    public GameObject laserBlastPrefab;
    public GameObject laserBeamPrefab;

    
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
    private int weaponMode;
    private bool laserStarted;
    private GameObject laserBeam;
    private GameObject laserBeamInitiated;
    private GameObject laserBeamSegment;
    private float blasterHeatLevel;


	void Start ()
    {
        boxMaster = this.GetComponent<BoxCollider2D>();
        tomRigidbody = this.GetComponent<Rigidbody2D>();
        canJet = false;
        onGround = true;
        inSpace = false;
        laserStarted = false;
        count = 0;
        weaponMode = 1;
    }
	
	
	void Update ()
    {
        if (ShipController.moveMan)
        {
            planet = ShipController.landingSite;

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
            ChangeWeapon();

            // Fires weapon on mouse button push
            if (Input.GetMouseButtonDown(0) && blasterHeatLevel < 100.0f)
            {
                Fire();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Destroy(laserBeam);
                laserBeam = null;
            }
            else if (laserBeam)
            {
                laserBeamUpdate();
            }
            else
            {
                blasterHeatLevel -= 5.0f;
            }

            Debug.Log("Blaster Heat Level: " + blasterHeatLevel);
        }
    }

    void GetInShip()
    {
        ShipController.moveMan = false;
        ShipController.liftOff = 1;
        ShipController.isLanded = false;
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
        GameObject projectile = null;
        Debug.Log("Tom has fired his weapon!");
        
        switch (weaponMode)
        {

            case 1:
                projectile = Instantiate(laserBlastPrefab, armRotationPoint.transform.position, Quaternion.Euler(0.0f, 0.0f, armTheta));
                blasterHeatLevel += 10.0f;
                break;
            case 2:
                projectile = Instantiate(laserBlastPrefab, armRotationPoint.transform.position, Quaternion.Euler(0.0f, 0.0f, armTheta + 5.0f));
                projectile = Instantiate(laserBlastPrefab, armRotationPoint.transform.position, Quaternion.Euler(0.0f, 0.0f, armTheta));
                projectile = Instantiate(laserBlastPrefab, armRotationPoint.transform.position, Quaternion.Euler(0.0f, 0.0f, armTheta - 5.0f));
                blasterHeatLevel += 20.0f;
                break;
            case 3:
                laserBeam = Instantiate(laserBeamPrefab, armRotationPoint.transform.position, Quaternion.Euler(0.0f, 0.0f, armTheta));
                laserBeamInitiated = Instantiate(laserBeamPrefab, armRotationPoint.transform.position, Quaternion.Euler(0.0f, 0.0f, armTheta));
                break; 
        }
    }


    private void ChangeWeapon()
    {
        Vector2 scrollWheeleDelta = Input.mouseScrollDelta;
        Debug.Log("Scroll Wheele Delta: " + Input.mouseScrollDelta);

        if (scrollWheeleDelta.y == -1.0f || Input.GetKeyDown(KeyCode.DownArrow))
        {
            weaponMode--;

            if (weaponMode <= 0)
            {
                weaponMode = 3;
            }
        }
        else if (scrollWheeleDelta.y == 1.0f || Input.GetKeyDown(KeyCode.UpArrow))
        {
            weaponMode++;

            if (weaponMode >= 4)
            {
                weaponMode = 1;
            }
        }

        Debug.Log("Weapon mode: " + weaponMode);
    }


    private void laserBeamUpdate ()
    {
        //laserBeamSegment = Instantiate()
        laserBeam.transform.rotation = Quaternion.Euler(0, 0, armTheta);
        laserBeam.transform.position = armRotationPoint.transform.position;
        blasterHeatLevel += 3.0f * Time.deltaTime;
        Debug.Log("Hey This laser beam is long");
    }


    private void findMousePosition ()
    {

    }



    public void OnCollisionEnter2D (Collision2D col)
    {
        tomRigidbody.velocity = new Vector3 (0, 0, 0);
        onGround = true;
    }
}