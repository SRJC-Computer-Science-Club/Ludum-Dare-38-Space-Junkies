using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour {

    public float value = 1.0f;
    public GameObject partent;

    public int width = 132;
    public int height = 32;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (partent)
            transform.position = partent.transform.position;
        
        Texture2D texture = new Texture2D(width, height);
        texture.SetPixel(0, 0, Color.red);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(new Rect(transform.position, new Vector2(width, height)), GUIContent.none);

    }
}
