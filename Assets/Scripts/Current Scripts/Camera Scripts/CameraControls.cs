using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public GameObject target;
    public GameObject dudeTarget;
	public BackgroundLayer[] backgroundLayers;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame but the last in line compared to void Update()
    void LateUpdate()
    {
        if (!PlayerControls.moveMan)
        {
            transform.position = target.transform.position + offset;
        }
        else
        {
            dudeTarget = GameObject.FindGameObjectWithTag("Player");

            transform.position = new Vector3 (dudeTarget.transform.position.x, dudeTarget.transform.position.y, -10.0f);
        }

		foreach(BackgroundLayer layer in backgroundLayers)
		{
			if(layer)
			{
				layer.CameraUpdate(transform.position);
			}
		}
    }
}
