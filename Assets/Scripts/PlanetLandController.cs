using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLandController : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ship")
        {
            Debug.Log("Bye Bye!");
            PlayerControls.hasHit = false;
        }
    }
}