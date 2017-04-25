using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{
    private int resourceLimit = 1;
    public GameObject resource;


	// Use this for initialization
	void Start ()
    {
        CircleCollider2D radiusFinder = this.GetComponent<CircleCollider2D>();
        float radius = radiusFinder.radius;


        for (int placed = 0; placed <= resourceLimit; placed++)
        {
            int placeDegree = Random.Range(0, 360);

           GameObject coal = Instantiate(resource, new Vector3(this.transform.position.x + radius * Mathf.Cos(placeDegree),
                                        this.transform.position.y + radius * Mathf.Sin(placeDegree)), Quaternion.identity);

            coal.transform.SetParent(this.transform);
        }
		
	}
}
