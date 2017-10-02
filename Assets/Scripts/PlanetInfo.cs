using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    public float mass;
    public int atmosphere;


    void Start ()
    {
        mass = (this.transform.lossyScale.x * 25);
        atmosphere = (int)(20 / this.transform.lossyScale.x);
    }


    public float getMass()
    {
        return mass;
    }


    public float getAtmosphere ()
    {
        return atmosphere;
    }
}
