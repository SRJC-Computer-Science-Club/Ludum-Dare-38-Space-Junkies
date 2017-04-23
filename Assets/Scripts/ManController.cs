using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManController : MonoBehaviour {

    public float speed = 1;
    

    void Update () {
        if(PlayerControls.moveMan == true)
        {
            this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

            Vector3 move = Vector3.zero;
            move.x = Input.GetAxis("Horizontal");
            move.y = Input.GetAxis("Vertical");
            transform.position += move * speed * Time.deltaTime;
        }
    }
}
