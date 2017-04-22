using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoxCollideController : MonoBehaviour {
   
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "basicWorld")
        {
            
            Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0.0f, 0.0f);
            PlayerControls.playerStop = true;
            Debug.Log("Ouch! Fucker!");
        }
    }
}
