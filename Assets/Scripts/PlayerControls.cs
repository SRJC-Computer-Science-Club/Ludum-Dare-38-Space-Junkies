using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public static bool playerStop;
    public float rotationSpeed;
    public float thrustForce;

    private void Start()
    {
        playerStop = false;
    }
    void Update()
    {
        if(!playerStop)
        {
            // Rotate
            transform.Rotate(0, 0, -Input.GetAxis("Horizontal") *
                rotationSpeed * Time.deltaTime);

            // Thrust
            GetComponent<Rigidbody2D>().
           AddForce(transform.up * thrustForce *
               Input.GetAxis("Vertical"));
        }
        else
        {
            Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0.0f, 0.0f);
            Debug.Log("This works, hurray!");
        }
    }
}
