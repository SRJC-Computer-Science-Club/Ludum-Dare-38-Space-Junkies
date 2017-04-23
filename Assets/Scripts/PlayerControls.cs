using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerActual;
    private static bool isLanded;
    public static bool moveMan;
    private float rotationSpeed = 100;
    public float thrustForce;

    
    private void Start()
    {
        isLanded = false;
        moveMan = false;
    }
    void Update()
    {
        if (isLanded == false)
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
            moveMan = true;
            //Debug.Log("This works, hurray!");
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (isLanded == false && coll.gameObject.tag == "basicWorld")
        {
            Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0.0f, 0.0f);

            float xCorr = coll.transform.position.x -this.transform.position.x;
            float yCorr = coll.transform.position.y -this.transform.position.y;

            var landingAngle = Mathf.Atan(yCorr / xCorr);

            landingAngle *= 180f / Mathf.PI; //to degree

            if (xCorr < 0)
                landingAngle += 180;

            this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

            this.transform.Rotate(new Vector3(0f,0f,landingAngle +90));

            playerActual = Instantiate(playerPrefab, new Vector3(this.transform.position.x, this.transform.position.y - 0.50f, 0.0f), Quaternion.identity);

            isLanded = true;
        }
    }
} 
