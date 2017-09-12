using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    private float gravitationalPull;
    

    public float getGravity ()
    {
        return gravitationalPull;
    }


    public float getMass ()
    {
        return GetComponent<Rigidbody2D>().mass;
    }
}
