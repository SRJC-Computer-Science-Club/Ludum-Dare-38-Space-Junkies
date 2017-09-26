using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchRing : MonoBehaviour {
    private Vector3 nearestPlanetCenter;
    private Vector3 distance;
    private bool inSpace;
//    public GameObject

	// Use this for initialization
	void Start () {
        inSpace = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerControls.moveMan == true && GravitationalForces.totalForceReferance.magnitude > 10.0f)
        {

        }
		
	}

    private float angle(Transform target)
    {
        Vector3 dir = target.position - this.transform.position;
        float result = Mathf.Atan(dir.x / dir.y) * Mathf.Rad2Deg;
        return result;
    }

    Transform getClosestPlanet(Transform[] planets)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 curPos = transform.position;
        foreach (Transform t in planets)
        {
            float dist = Vector3.Distance(t.position, curPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
