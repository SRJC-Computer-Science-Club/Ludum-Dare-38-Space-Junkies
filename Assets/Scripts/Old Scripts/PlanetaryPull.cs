using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryPull : MonoBehaviour
{
    public bool homePlanet = false;

    private const float GRAV_PULL = 10.0f;
    private GameObject shipInField;
    private Rigidbody2D shipInFieldRb;
    private float radius;
    private float xPull;
    private float yPull;
    private float zPull;


    public static bool crashed;


    void Start ()
    {
        crashed = false;
	}
	
	

	void Update ()
    {
        if (shipInField && !crashed)
        {
            Vector2 fieldPos = shipInField.transform.localPosition;
            Debug.Log("This is the local position of ship: " + fieldPos.x + " " + fieldPos.y);

            if (shipInField.transform.localPosition.x > 0)
            {
                xPull = -GRAV_PULL;
            }
            else if (shipInField.transform.localPosition.x < 0)
            {
                xPull = GRAV_PULL;
            }
            //else if (shipinfield.transform.position.x == 0)
            //{
            //    xpull = 0.0f;
            //}

            if (shipInField.transform.localPosition.y > 0)
            {
                yPull = -GRAV_PULL;
            }
            else if (shipInField.transform.localPosition.y < 0)
            {
                yPull = GRAV_PULL;
            }
            //else if (shipInField.transform.position.y == 0)
            //{
            //    yPull = 0.0f;
            //}

            shipInFieldRb.AddForce (new Vector2(xPull, yPull));
        }
        else if (crashed && shipInField)
        {
            shipInFieldRb.velocity = new Vector2(0.0f, 0.0f);
        }
	}



    public void OnTriggerEnter2D (Collider2D col)
    {
        Debug.Log("Something is in my field!");
        shipInField = col.gameObject;
        shipInField.transform.SetParent(this.transform);

        shipInFieldRb = shipInField.GetComponent<Rigidbody2D> ();
    }



    public void OnTriggerExit2D(Collider2D col)
    {
        if (shipInField != null)
        {
            Debug.Log("Something left my field");
            shipInField.transform.parent = null;
            shipInField = null;
        }
    }



    public void OnCollisionEnter2D (Collision2D col)
    {
        crashed = true;

        if (homePlanet)
        {
            Application.LoadLevel(2);
        }
    }
}
