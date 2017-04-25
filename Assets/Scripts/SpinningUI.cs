using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningUI : MonoBehaviour
{
    private float turnFun;

	// Use this for initialization
	void Start ()
    {
        turnFun = 0;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Transform imageMover = this.GetComponent<Transform>();

        imageMover.Rotate (new Vector3(0.0f, 0.0f, 10.0f * Time.deltaTime));
        turnFun += 1.0f;
	}
}
