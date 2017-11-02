using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalForces : MonoBehaviour
{
    public const float GRAV_CONSTANT = 6.0f;
    public const float MAX_DISTANCE = 20;
    public static Vector2 totalForceReferance;
    public static float angleReferance;

    private GameObject[] planetsInGalaxy;
    private string thisTag;
    private List<GameObject> planetsInRange;
    private float strongestPlanetDistance;
    private float lastMag;
    private float mass;
    private float massPlanet;
    private Rigidbody2D thisRigidbody;
    private CameraControls cameraControls;
    private bool testingMan;

    // private GameObject strongestPlanet;



    public void Start ()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        thisTag = this.gameObject.tag;
    }



    public void FixedUpdate ()
    {
        Vector2 totalForce = new Vector2(0, 0);

        if ((thisTag == "Ship" && !ShipController.moveMan) ||
            (thisTag == "Player" && ShipController.moveMan))
        {
            foreach (GameObject planet in GameObject.FindGameObjectsWithTag("planet"))
            {
                PlanetInfo planetInfo = planet.GetComponent<PlanetInfo>();
                Vector2 force = planet.transform.position - this.transform.position;
                float d = force.magnitude * 1;
                float magnitude = GRAV_CONSTANT * (planetInfo.getMass() * thisRigidbody.mass / (d * d));

                //float magnitude = 200.0f / (d * d);

                force.Normalize();
                force *= magnitude;

                totalForce += force;
            }

            ///Debug.Log(totalForce);

            thisRigidbody.AddForce(totalForce);
            totalForceReferance = totalForce;

            //Debug.Log("Magnitude: " + totalForce.magnitude);
            if (totalForce.magnitude > 10.0f && ShipController.moveMan)
            {
                float angle = Mathf.Atan2(totalForce.y, totalForce.x) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            }
        }
    }
}






// Old Code

    // From FixedUpdate ()
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


