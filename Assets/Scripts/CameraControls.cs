using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public GameObject target;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame but the last in line compared to void Update()
    void LateUpdate()
    {
        transform.position = target.transform.position + offset;
    }
}
