﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    
    public static GameObject landingSite;
    public static bool isLanded;
    public static bool moveMan;
    public static int liftOff;
    public static float fuel = 100f;
    public GameObject playerPrefab;
    public GameObject playerActual;
    public GameObject spawnPoint;
    public float thrustForce;
    public float xSpawn;
    public float ySpawn;
    public float forceX;
    public float forceY;
    public Texture2D bgImage;
    public Texture2D fgImage;
    

    private Rigidbody2D ShipRigidbody;
    private const float halfPlayer = 0.84f;
    private float maxFuel = 100f;
    private float rotationSpeed = 100;
    private float timeStart;
    private float timeToSpawn;
    private float lastDirection;


    private void Start()
    { 
        isLanded = false;
        moveMan = false;
        liftOff = 0;
        timeStart = 0;
        timeToSpawn = 0;
        xSpawn = 0;
        ySpawn = 0;

        ShipRigidbody = this.GetComponent<Rigidbody2D>();
        ShipRigidbody.velocity = new Vector2(0.0f, 0.0f);
    }


    void Update()
    {
        if (!isLanded)
        {
            forceX = ShipRigidbody.velocity.x;
            forceY = ShipRigidbody.velocity.y;

            Rotate();
            Thrust();
            Fuel();
            LiftOff();

            // Loss by lack of fuel
            if (fuel <= 0 && timeStart == 0)
            {
                timeStart = Time.time;
            }
            else if (Time.time - timeStart >= 10 && fuel <= 0)
            {
                Application.LoadLevel(3);
            }
        }
        else
        {
            ShipLands();
        }


        if (Mathf.Sqrt (Mathf.Pow (0.0f - this.transform.position.x, 2) + Mathf.Pow (0.0f - this.transform.position.y, 2)) >= 80.0f)
        {
            //Debug.LogError("Die");
            Application.LoadLevel(3);
        }

        //Debug.Log("Ship velocity " + this.GetComponent<Rigidbody2D>().velocity);
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
        Debug.Log("angle " + (radius * Mathf.Cos(angle)));

        return (radius * Mathf.Cos(angle) + planet.transform.position.x);
    }

    private float findYSpawnPoint(GameObject planet)
    {
        float radius = planet.GetComponent<CircleCollider2D>().radius;
        float angle = Mathf.Atan(this.transform.localPosition.y / this.transform.localPosition.x) * 180 / Mathf.PI;

        return Mathf.Abs(radius * Mathf.Sin(angle) + planet.transform.position.y);
    }

    //private float findAngle (GameObject

    void Rotate()
    {
        if (fuel > 0)
        {
            transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        }
    }
    void Thrust()
    {
        if (fuel > 0)
        {
            if (Input.GetKey(KeyCode.W))
            {
                GetComponent<Rigidbody2D>().AddForce(Input.GetAxis("Vertical") * transform.up * thrustForce);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                GetComponent<Rigidbody2D>().AddForce(Input.GetAxis("Vertical") * transform.up * thrustForce);
            }
            //else
            //{
            //    if (forceX > 0)
            //    {
            //        forceX -= 1.0f * Time.deltaTime;

            //        if (forceX < 0.25f)
            //        {
            //            forceX = 0;
            //        }
            //    }
            //    else if (forceX < 0)
            //    {
            //        forceX += 1.0f * Time.deltaTime;

            //        if (forceX > -1.0f)
            //        {
            //            forceX = 0;
            //        }
            //    }

            //    if (forceY > 0)
            //    {
            //        forceY -= 1.0f * Time.deltaTime;

            //        if (forceY < 1.0f)
            //        {
            //            forceY = 0;
            //        }
            //    }
            //    else if (forceY < 0)
            //    {
            //        forceY += 1.0f * Time.deltaTime;

            //        if (forceY > -1.0f)
            //        {
            //            forceY = 0;
            //        }
            //    }

            //    rigidbody2D.velocity = new Vector2(forceX, forceY);
            //}

            //GetComponent<Rigidbody2D>().AddForce(Input.GetAxis("Vertical") * transform.up * thrustForce);
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
        }
    }

    void ShipLands()
    {
        float timeFromLanding = Time.time;

        ShipRigidbody.velocity = new Vector2(0.0f, 0.0f);

        if (Mathf.Abs(timeToSpawn - timeFromLanding) >= 1.0f && !moveMan)
        {
            float angle = Mathf.Atan(ySpawn / xSpawn);
            Quaternion newRotation = new Quaternion(0.0f, 0.0f, angle * 180 / Mathf.PI, 0.0f);

            playerActual = Instantiate(playerPrefab, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, -2.0f), this.transform.rotation);
            moveMan = true;
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
