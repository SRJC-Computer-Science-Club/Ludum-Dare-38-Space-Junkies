using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningUI : MonoBehaviour
{
    public int turnRate = 30;
    public bool right = true;



    // Update is called once per frame
    void Update()
    {
        Transform imageMover = this.GetComponent<Transform>();

        if (right)
        {
            imageMover.Rotate(new Vector3(0.0f, 0.0f, turnRate * Time.deltaTime));
        }
        else
        {
            imageMover.Rotate(new Vector3(0.0f, 0.0f, -turnRate * Time.deltaTime));
        }
    }
}
