using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerActual;
    public GameObject spawnPoint;
    public static GameObject landingSite;
    public static bool isLanded;
    private bool spawn;
    public static bool moveMan;
    private float rotationSpeed = 100;
    public float thrustForce = 2.0f;
    public static int liftOff;


    private void Start()
    {
        isLanded = false;
        spawn = false;
        moveMan = false;
        thrustForce = 5.0f;
        liftOff = 0;

        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
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
           AddForce(Input.GetAxis ("Vertical") * transform.up * thrustForce);

            

            if (liftOff == 1)
            {
                this.GetComponent<Rigidbody2D>().AddForce(transform.up * thrustForce * 10);
                liftOff = 0;
            }
        }
        else
        {
            Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0.0f, 0.0f);
            moveMan = true;
            //Debug.Log("This works, hurray!");
        }

        if (spawn)
        {
            playerActual = Instantiate(playerPrefab, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, -2.0f), this.transform.rotation);
            playerActual.transform.SetParent(this.transform);

            spawn = false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (isLanded == false && coll.gameObject.tag == "basicWorld")
        {
            Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0.0f, 0.0f);

            float xCorr = coll.transform.position.x - this.transform.position.x;
            float yCorr = coll.transform.position.y - this.transform.position.y;

            var landingAngle = Mathf.Atan(yCorr / xCorr);

            landingAngle *= 180f / Mathf.PI; //to degree

            if (xCorr < 0)
                landingAngle += 180;

            this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

            this.transform.Rotate(new Vector3(0f, 0f, landingAngle + 90));

            landingSite = coll.gameObject;
            isLanded = true;
            spawn = true;
        }
    }



    public void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Hey! You are leaving the known world");

        if (col.gameObject.tag == "Galaxy")
        {
            Debug.LogError("Die");
            Application.LoadLevel(3);
        }
    }
}
