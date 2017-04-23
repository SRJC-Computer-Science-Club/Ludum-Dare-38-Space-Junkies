using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerActual;
    public static bool playerStop;
    public static bool moveMan;
    public static bool stopSpawn;
    private float rotationSpeed = 100;
    public float thrustForce;

    
    private void Start()
    {
        playerStop = false;
        moveMan = false;
        stopSpawn = false;
    }
    void Update()
    {
        if (!playerStop)
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

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "basicWorld")
        {
            Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0.0f, 0.0f);
            this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            playerStop = true;

            if(stopSpawn == false)
            {
                playerActual = Instantiate(playerPrefab, new Vector3(this.transform.position.x, this.transform.position.y - 0.50f, 0.0f), Quaternion.identity);
                stopSpawn = true;
            }
        }
    }
} 
