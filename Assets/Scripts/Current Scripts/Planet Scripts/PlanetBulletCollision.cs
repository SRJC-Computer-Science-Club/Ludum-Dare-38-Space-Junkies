using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBulletCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "playerShot")
        {
            Destroy(other.gameObject);
        }
    }
}