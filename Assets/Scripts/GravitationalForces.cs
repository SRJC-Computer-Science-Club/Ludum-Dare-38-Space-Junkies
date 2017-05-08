﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalForces : MonoBehaviour
{
    private float mass;
    private Rigidbody2D thisRB;

    public float gravConstant;
    public const float MAX_DISTANCE = 20;
    public float planetMass;

    private GameObject[] planetsInGalaxy;
    private List<GameObject> planetsInRange;
    private GameObject strongestPlanet;
    private float strongestPlanetDistance;
    private float lastMag;



    public void Start ()
    {
        thisRB = GetComponent<Rigidbody2D>();
        /*
        int planetCount = 1;

        mass = this.GetComponent<Rigidbody2D>().mass;
        lastMag = 0;
        thisRB = this.GetComponent<Rigidbody2D>();
        planetsInGalaxy = GameObject.FindGameObjectsWithTag("planet");
        planetsInRange = new List<GameObject>();

        foreach (GameObject planets in planetsInGalaxy)
        {
            Debug.Log("This is the " + planetCount + " planet in this galaxy" +
                      "\nI am located at (" + planets.transform.position.x + ", "
                      + planets.transform.position.y + ")");
            planetCount++;
        }
        */
    }



    public void FixedUpdate ()
    {
        Debug.Log("Hello World");

        /*
        foreach (GameObject planets in planetsInGalaxy)
        {
            float distance = Vector2.Distance(planets.transform.position, this.gameObject.transform.position);

            if (distance <= MAX_DISTANCE && !planetsInRange.Contains (planets))
            {
                planetsInRange.Add(planets);
                float radius = planets.GetComponent<CircleCollider2D>().radius + 1;

                if (distance <= radius + 2.0f)
                {
                    strongestPlanet = planets;
                    strongestPlanetDistance = radius + 2.0f;
                }
            }
            else if (distance > MAX_DISTANCE && planetsInRange.Contains (planets))
            {
                planetsInRange.Remove(planets);
                Debug.Log("I have left the field");
            }
        }
        */

        Vector2 totalForce = new Vector2(0, 0);

        foreach (GameObject planet in GameObject.FindGameObjectsWithTag("planet"))
        {
            
            Vector2 force = planet.transform.position - this.transform.position;
            float d = force.magnitude * 1;
            float magnitude = 200.0f /(d*d);

            force.Normalize();
            force *= magnitude;

            totalForce += force;
            
        }

        Debug.Log(totalForce);

        thisRB.AddForce(totalForce);

        Debug.Log("Magnitude" + totalForce.magnitude);
        if (totalForce.magnitude > 1.0f && PlayerControls.moveMan)
        {
            float angle = Mathf.Atan2(totalForce.y, totalForce.x);
            this.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg + 90);
        }


        /*
        if (strongestPlanet && Vector2.Distance (strongestPlanet.transform.position, this.transform.position) < strongestPlanetDistance)
        {
            this.transform.SetParent(strongestPlanet.transform);

            Vector2 localPositions = this.transform.localPosition;
            float angle = Mathf.Atan2(localPositions.y, localPositions.x) * Mathf.Rad2Deg;
            this.transform.SetParent(null);

            //Debug.Log("x: " + localPositions.x + " y: " + localPositions.x + " angle: " + (angle - 90));
            this.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        */
    }
 
}
