using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerActual;
    public GameObject spawnPoint;
    private float timeStart;
    public static GameObject landingSite;
    public static bool isLanded;
    private bool spawn;
    public static bool moveMan;
    private float rotationSpeed = 100;
    public float thrustForce = 2.0f;
    public static int liftOff;

    public static float fuel = 100f;
    private float maxFuel = 100f;

    private void Start()
    {
        isLanded = false;
        spawn = false;
        moveMan = false;
        thrustForce = 5.0f;
        liftOff = 0;
        timeStart = 0;

        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);

    }
    void Update()
    {
        if (isLanded == false)
        {
            // Rotate
            if (fuel > 0)
                transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

            // Thrust
            if (fuel > 0)
                GetComponent<Rigidbody2D>().AddForce(Input.GetAxis ("Vertical") * transform.up * thrustForce);

            //Fuel
            var travelConsumption = 0.125f;

            if (Input.GetKey(KeyCode.W))
                fuel -= travelConsumption;
            if (Input.GetKey(KeyCode.S))
                fuel -= travelConsumption;
            if (Input.GetKey(KeyCode.A))
                fuel -= travelConsumption;
            if (Input.GetKey(KeyCode.D))
                fuel -= travelConsumption;

            if (liftOff == 1)
            {
                this.GetComponent<Rigidbody2D>().AddForce(transform.up * thrustForce * 10);
                liftOff = 0;
            }

            if (fuel <= 0 && timeStart == 0)
            {
                timeStart = Time.time;
            }
            else if (Time.time - timeStart >= 10 && fuel <= 0)
            {
                Application.LoadLevel(3);
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

        if (Mathf.Sqrt (Mathf.Pow (0.0f - this.transform.position.x, 2) + Mathf.Pow (0.0f - this.transform.position.y, 2)) >= 80.0f)
        {
            Debug.LogError("Die");
            Application.LoadLevel(3);
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
        //else if (coll.gameObject.tag == "collectable")
        //{
        //    Destroy(coll.gameObject);

        //    fuel += 10;

        //}
    }

    public Texture2D bgImage;
    public Texture2D fgImage;

    private void OnGUI()
    {
        var healthBarLength = 100f;
        // Create one Group to contain both images
        // Adjust the first 2 coordinates to place it somewhere else on-screen
        GUI.BeginGroup(new Rect(0, 0, healthBarLength, 32));

        // Draw the background image
        GUI.Box(new Rect(0, 0, healthBarLength, 32), bgImage);

        // Create a second Group which will be clipped
        // We want to clip the image and not scale it, which is why we need the second Group
        GUI.BeginGroup(new Rect(0, 0, fuel / maxFuel * healthBarLength, 32));

        // Draw the foreground image
        GUI.Box(new Rect(0, 0, healthBarLength, 32), fgImage);

        // End both Groups
        GUI.EndGroup();

        GUI.EndGroup();
    }
}
