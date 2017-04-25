using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyBounds : MonoBehaviour
{
    public void OnTriggerExit2D(Collider2D col)
    {
        Debug.LogError("Die");
        Application.LoadLevel(3);
    }	
}
