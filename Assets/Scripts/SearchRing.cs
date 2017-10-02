using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchRing : MonoBehaviour {
    private Vector3 nearestPlanetCenter;
    private Vector3 distance;
    private bool inSpace;
    private GameObject ring;
    private Transform ringTransform;
    private GameObject[] planets;
    private Transform[] planetTransforms;
    private Vector3 ringAngle;

	// Use this for initialization
	void Start () {
        inSpace = false;
        ringTransform = this.transform.GetChild(0);
        ring = ringTransform.gameObject;
        ring.SetActive(false);
        planets = GameObject.FindGameObjectsWithTag("planet");
        planetTransforms = new Transform[planets.Length];
        int count = 0;
        foreach (GameObject p in planets)
        {
            planetTransforms[count] = p.transform;
            count++;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerControls.moveMan == true && GravitationalForces.totalForceReferance.magnitude > 10.0f)
        {
            if (ring.activeSelf == true) ring.SetActive(false);
        }
        else
        {
            if (ring.activeSelf == false) ring.SetActive(true);
            ringAngle = setAngle(calcAngle(getClosestPlanet(planetTransforms)) * -1);
            ringTransform.rotation = Quaternion.Euler(ringAngle);
            Debug.Log("Now my rotation should be" + ringAngle.z);
        }
		
	}

    private Vector3 setAngle(float input)
    {
        return new Vector3(0, 0, input);
    }

    private float calcAngle(Transform target)
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
