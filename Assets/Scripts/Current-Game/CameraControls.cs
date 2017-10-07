using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public GameObject target;
    public GameObject dudeTarget;
    public bool testMan;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        if (!testMan)
        {
            offset = transform.position - target.transform.position;
        }
        else
        {
            Debug.Log("player is cameras target player");

            offset = transform.position - dudeTarget.transform.position;
        }
    }

    // Update is called once per frame but the last in line compared to void Update()
    void LateUpdate()
    {
        if (!PlayerControls.moveMan && !testMan)
        {
            transform.position = target.transform.position + offset;
        }
        else if (testMan)
        {
            Debug.Log("player is cameras target player");

            transform.position = new Vector3 (dudeTarget.transform.position.x, dudeTarget.transform.position.y, -10.0f);
        }
        else
        {
            Debug.Log("player is cameras target player");
            dudeTarget = GameObject.FindGameObjectWithTag("Player");

            transform.position = new Vector3 (dudeTarget.transform.position.x, dudeTarget.transform.position.y, -10.0f);
        }
    }
}
