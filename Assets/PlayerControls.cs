using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public float rotationSpeed;
    public float thrustForce;

    void FixedUpdate()
    {

        // Rotate the ship if necessary
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") *
            rotationSpeed * Time.deltaTime);

        // Thrust the ship if necessary
        GetComponent<Rigidbody2D>().
            AddForce(transform.up * thrustForce *
                Input.GetAxis("Vertical"));

    }

}
