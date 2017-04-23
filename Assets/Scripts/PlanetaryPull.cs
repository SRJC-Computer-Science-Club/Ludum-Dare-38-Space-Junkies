using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryPull : MonoBehaviour
{
    private const float GRAV_PULL = 0.60f;
    private GameObject shipInField;
    private Rigidbody2D shipInFieldRb;
    private float xPull;
    private float yPull;
    private float zPull;


    public bool crashed;


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

            if (shipInField.transform.position.x > 0)
            {
                xPull = -GRAV_PULL;
            }
            else
            {
                xPull = GRAV_PULL;
            }

            if (shipInField.transform.position.y > 0)
            {
                yPull = -GRAV_PULL;
            }
            else
            {
                yPull = GRAV_PULL;
            }

            shipInFieldRb.AddRelativeForce(new Vector2(Mathf.Abs(fieldPos.x) * xPull, Mathf.Abs(fieldPos.y) * yPull));
        }
        else if (crashed && shipInField)
        {
            shipInFieldRb.velocity = new Vector2(0.0f, 0.0f);
        }
	}



    void OnTriggerEnter2D (Collider2D col)
    {
        Debug.Log("Something is in my field!");
        shipInField = col.gameObject;
        shipInField.transform.SetParent(this.transform);

        shipInFieldRb = shipInField.GetComponent<Rigidbody2D> ();
    }



    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Something left my field");
        shipInField.transform.parent = null;
        shipInField = null;
    }



    void OnCollisionEnter2D (Collision2D col)
    {
        crashed = true;
    }
}
