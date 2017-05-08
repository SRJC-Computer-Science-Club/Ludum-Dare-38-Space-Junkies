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
    private Vector2 totalForce;
    private GameObject strongestPlanet;
    private float strongestPlanetDistance;
    private float lastMag;



    public void Start ()
    {
        int planetCount = 1;

        mass = this.GetComponent<Rigidbody2D>().mass;
        lastMag = 0;
        thisRB = this.GetComponent<Rigidbody2D>();
        totalForce = Vector2.zero;
        planetsInGalaxy = GameObject.FindGameObjectsWithTag("planet");
        planetsInRange = new List<GameObject>();

        foreach (GameObject planets in planetsInGalaxy)
        {
            Debug.Log("This is the " + planetCount + " planet in this galaxy" +
                      "\nI am located at (" + planets.transform.position.x + ", "
                      + planets.transform.position.y + ")");
            planetCount++;
        }
    }



    public void Update ()
    {
        
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
            

            foreach (GameObject planets in planetsInRange)
            {
                Vector2 force = planets.transform.position - this.transform.position;
                float magnitude = gravConstant * mass - planetMass / Mathf.Pow(force.magnitude, 2);

                force.Normalize();
                force *= magnitude;

                if (planetsInRange.Count == 1)
                {
                    totalForce = force;
                }
                else if (planetsInRange.Count > 1)
                {
                    totalForce += force;
                }
            }

            if (planetsInRange.Count < 1)
            {
                totalForce = Vector2.zero;
            }

            thisRB.AddForce(totalForce);

            if (strongestPlanet && Vector2.Distance (strongestPlanet.transform.position, this.transform.position) < strongestPlanetDistance)
            {
                this.transform.SetParent(strongestPlanet.transform);

                Vector2 localPositions = this.transform.localPosition;
                float angle = Mathf.Atan2(localPositions.y, localPositions.x) * Mathf.Rad2Deg;
                this.transform.SetParent(null);

                //Debug.Log("x: " + localPositions.x + " y: " + localPositions.x + " angle: " + (angle - 90));
                this.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }
 
}