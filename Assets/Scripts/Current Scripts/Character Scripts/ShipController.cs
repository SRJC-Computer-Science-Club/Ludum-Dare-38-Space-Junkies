using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public static GameObject landingSite;
    public static bool isLanded;
    public static bool playerLeavesShip;
    public static int liftOff;
    public static float fuel = 100f;
    public GameObject playerPrefab;
    public GameObject playerActual;
    public GameObject shipLaserPrefab;
    public float thrustForce;
    public float xSpawn;
    public float ySpawn;
    public float forceX;
    public float forceY;
    public Texture2D bgImage;
    public Texture2D fgImage;
    public Vector3 playerPosition;
    public static bool moveMan;
    public GameObject fireTubeOne;
    public GameObject fireTubeTwo;


    private Rigidbody2D ShipRigidbody;
    private const float halfPlayer = 0.84f;
    private float maxFuel = 100f;
    public float rotationSpeed;
    private float timeStart;
    private float timeToSpawn;
    private float lastDirection;
    private Quaternion shipTheta;
    private float lastShipTheta;
    private float delay;
    private float incrimentalTheta;
    private float originalTheta;
    private Vector3 lastMouseLocation;


    private void Start()
    { 
        isLanded = false;
        playerLeavesShip = false;
        moveMan = false;
        liftOff = 0;
        timeStart = 0;
        timeToSpawn = 0;
        xSpawn = 0;
        ySpawn = 0;
        lastMouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        ShipRigidbody = this.GetComponent<Rigidbody2D>();
        ShipRigidbody.velocity = new Vector2(0.0f, 0.0f);
    }


    void FixedUpdate()
    {
        if (!isLanded)
        {
            forceX = ShipRigidbody.velocity.x;
            forceY = ShipRigidbody.velocity.y;

            Thrust();
            Fuel();
            LiftOff();
            Rotate();
            Fire();
        }
        else
        {
            ShipLands();
        }


        if (Mathf.Sqrt (Mathf.Pow (0.0f - this.transform.position.x, 2) + Mathf.Pow (0.0f - this.transform.position.y, 2)) >= 80.0f)
        {
            Debug.LogError("Out of Bounds");
            //Application.LoadLevel(3);
        }

        //Debug.Log("Ship velocity: " + this.GetComponent<Rigidbody2D>().velocity.x + ", " + this.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Update()
    {
        if (!isLanded)
        {
            OutOfFuel();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("This Object is a " + coll.tag);

        if (isLanded == false && coll.gameObject.tag == "planet")
        {
            Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0.0f, 0.0f);

            float xCorr = coll.transform.position.x - this.transform.position.x;
            float yCorr = coll.transform.position.y - this.transform.position.y;
            float landingAngle = Mathf.Atan(yCorr / xCorr);

            landingAngle *= 180f / Mathf.PI; //to degree

            if (xCorr < 0)
                landingAngle += 180;

            this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

            this.transform.Rotate(new Vector3(0f, 0f, landingAngle + 90));

            xSpawn = findXSpawnPoint(coll.gameObject);
            ySpawn = findYSpawnPoint(coll.gameObject);

            //If you want to work properly use spawnPosition.transfrom.position.x or .y respectively bellow
            timeToSpawn = Time.time;

            landingSite = coll.gameObject;
            isLanded = true;
        }
        //else if (coll.gameObject.tag == "collectable")
        //{
        //    Destroy(coll.gameObject);

        //    fuel += 10;

        //}
    }

    private void OnGUI()
    {
        var healthBarLength = 100f;
        // Create one Group to contain both images
        // Adjust the first 2 coordinates to place it somewhere else on-screen
        GUI.BeginGroup(new Rect(0, 0, healthBarLength, 32));

        // Draw the background image
        GUI.Box(new Rect(0, 0, healthBarLength, 32), bgImage);

        // Create a second Group which will be clipped
        // We want to clip the image and not scale it, which is why we need the second Group
        GUI.BeginGroup(new Rect(0, 0, fuel / maxFuel * healthBarLength, 32));

        // Draw the foreground image
        GUI.Box(new Rect(0, 0, healthBarLength, 32), fgImage);

        // End both Groups
        GUI.EndGroup();

        GUI.EndGroup();
    }

    private float findXSpawnPoint (GameObject planet)
    {
        float radius = planet.GetComponent<CircleCollider2D>().radius;
        float angle = Mathf.Atan (this.transform.localPosition.y / this.transform.localPosition.x) * 180 / Mathf.PI;

        return (radius * Mathf.Cos(angle) + planet.transform.position.x);
    }

    private float findYSpawnPoint(GameObject planet)
    {
        float radius = planet.GetComponent<CircleCollider2D>().radius;
        float angle = Mathf.Atan(this.transform.localPosition.y / this.transform.localPosition.x) * 180 / Mathf.PI;

        return Mathf.Abs(radius * Mathf.Sin(angle) + planet.transform.position.y);
    }

    private void Fire()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(shipLaserPrefab, new Vector2(fireTubeOne.transform.position.x, fireTubeOne.transform.position.y), 
                transform.rotation);
            Instantiate(shipLaserPrefab, new Vector2(fireTubeTwo.transform.position.x, fireTubeTwo.transform.position.y), 
                transform.rotation);
        }
    }


    private void Rotate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (fuel > 0)
        {
            mousePos = new Vector3((mousePos.x - transform.position.x), (mousePos.y - transform.position.y), mousePos.z);
            ////transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
            mousePos.Normalize();
            mousePos = new Vector3(mousePos.x * 10, mousePos.y * 10, mousePos.z * 10);

            Quaternion newTheta = Quaternion.Euler(0.0f, 0.0f, (Mathf.Rad2Deg * Mathf.Atan2(mousePos.y, mousePos.x)) - 90);

            transform.rotation = shipTheta = Quaternion.Lerp(this.transform.rotation, newTheta, Mathf.Pow(-rotationSpeed * Time.deltaTime, 2.0f));
        }
    }


    void Thrust()
    {
        if (fuel > 0)
        {
            if (Input.GetMouseButton(1))
            {
                GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * thrustForce);
            }
            /*else if (Input.GetKey(KeyCode.S))
            {
                GetComponent<Rigidbody2D>().AddForce(Input.GetAxis("Vertical") * transform.up * thrustForce);
            }*/
            
        }
    }

    void Fuel()
    {
        var travelConsumption = 0;

        if (Input.GetKey(KeyCode.W))
            fuel -= travelConsumption;
        if (Input.GetKey(KeyCode.S))
            fuel -= travelConsumption;
        if (Input.GetKey(KeyCode.A))
            fuel -= travelConsumption;
        if (Input.GetKey(KeyCode.D))
            fuel -= travelConsumption;
    }

    void LiftOff()
    {
        if (liftOff == 1)
        {
            this.GetComponent<Rigidbody2D>().AddForce(transform.up * thrustForce * 75);
            liftOff = 0;
            moveMan = false;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                EnemyAttack enemyAttack;
                enemyAttack = enemy.GetComponent<EnemyAttack>();
                enemyAttack.tomExists = false;
            }
        }
    }

    void ShipLands()
    {
        float spawnPoint = 0.16f;
        float timeFromLanding = Time.time;
        moveMan = true;

        ShipRigidbody.velocity = new Vector2(0.0f, 0.0f);

        if (Mathf.Abs(timeToSpawn - timeFromLanding) >= 1.0f && !playerLeavesShip)
        {
            float angle = Mathf.Atan(ySpawn / xSpawn);
            Quaternion newRotation = new Quaternion(0.0f, 0.0f, angle * 180 / Mathf.PI, 0.0f);

            playerPosition = new Vector3(this.transform.position.x, this.transform.position.y - spawnPoint, this.transform.position.z);

            //playerActual = Instantiate(playerPrefab, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, -2.0f), this.transform.rotation);
            //playerActual = Instantiate(playerPrefab, new Vector3(this.transform.position.x, this.transform.position.y - spawnPoint, this.transform.position.z), this.transform.rotation);
            playerActual = Instantiate(playerPrefab, new Vector3(playerPosition.x, playerPosition.y, playerPosition.z), this.transform.rotation);
            playerLeavesShip = true;



            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                EnemyAttack enemyAttack;
                enemyAttack = enemy.GetComponent<EnemyAttack>();
                enemyAttack.TomExists();
            }
        }
    }


    // Loss by lack of fuel
    void OutOfFuel()
    {
        if (fuel <= 0 && timeStart == 0)
        {
            timeStart = Time.time;
        }
        else if (Time.time - timeStart >= 10 && fuel <= 0)
        {
            //Application.LoadLevel(3);
        }
    }
        
    public bool getMoveMan ()
    {
        return moveMan;
    }


    public void setMoveMan (bool check)
    {
        moveMan = check;
    }


    public GameObject getLandingSite ()
    {
        return landingSite;
    }


    public void setLandingSite (GameObject newSite)
    {
        landingSite = newSite;
    }


    public bool getIsLanded ()
    {
        return isLanded;
    }


    public void setIsLanded (bool check)
    {
        isLanded = check;
    }


    public int getLiftOff ()
    {
        return liftOff;
    }


    public void setLiftOff (int check)
    {
        liftOff = check;
    }


    public float getFuel ()
    {
        return fuel;
    }


    public void setFuel (float reFuel)
    {
        fuel = reFuel;
    }
}
