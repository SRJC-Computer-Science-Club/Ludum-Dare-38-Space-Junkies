using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassPoint : MonoBehaviour
{
    public GameObject homeTarget;
    public GameObject target;
    private Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        offset = transform.position - target.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = target.transform.position + offset;

        float xHome = homeTarget.transform.position.x;
        float yHome = homeTarget.transform.position.y;
        float xMe = this.transform.localPosition.x;
        float yMe = this.transform.localPosition.y;

        float distance = Mathf.Sqrt(Mathf.Pow(xHome - xMe, 2) + Mathf.Pow(yHome - yMe, 2));

        float rotation = Mathf.Atan (yMe / xMe);
        rotation = (rotation * (180 / Mathf.PI));

        if (xMe > 0)
        {
            rotation -= 270;
        }
        else if (xMe < 0)
        {
            rotation += 270;
        }

        this.transform.rotation = Quaternion.AngleAxis (rotation, Vector3.forward);
        Debug.Log("Compass Rotation: " + rotation + " Distance: " + distance);
	}
}
