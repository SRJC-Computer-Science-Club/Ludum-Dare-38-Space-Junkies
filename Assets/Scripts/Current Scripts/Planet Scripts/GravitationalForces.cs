using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalForces : MonoBehaviour
{
    public const float GRAV_CONSTANT = 6.0f;
    public const float MAX_DISTANCE = 20;
    public static Vector2 totalForceReferance;
    private string thisTag;
    private Rigidbody2D thisRigidbody;
    private bool testingMan;


    public void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        thisTag = this.gameObject.tag;
    }

    public void FixedUpdate()
    {
        if ((thisTag == "Ship" && !PlayerControls.playerLeavesShip) || 
           (thisTag == "Player" && PlayerControls.playerLeavesShip) ||
           (thisTag == "Enemy"))
        {
            Gravity();
        }
    }

    void Gravity()
    {
        Vector2 totalForce = new Vector2(0, 0);


        foreach (GameObject planet in GameObject.FindGameObjectsWithTag("planet"))
        {
            PlanetInfo planetInfo = planet.GetComponent<PlanetInfo>();

            

            Vector2 force = planet.transform.position - this.transform.position;

            float magnitude = GRAV_CONSTANT * (planetInfo.getMass() * thisRigidbody.mass / (force.magnitude * force.magnitude));
            //                      6                planet mass            rigidbody mass              force.magnitude squared

            force.Normalize();

            force *= magnitude;

            totalForce += force;
        }


        thisRigidbody.AddForce(totalForce);
        totalForceReferance = totalForce;

        //if (totalForce.magnitude > 10.0f && PlayerControls.playerLeavesShip)
        if (totalForce.magnitude > 10.0f && (thisTag != "Ship"))
        {
            float angle = Mathf.Atan2(totalForce.y, totalForce.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }
}