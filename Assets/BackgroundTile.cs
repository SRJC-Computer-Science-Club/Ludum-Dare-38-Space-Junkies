using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BackgroundTile : MonoBehaviour
{
	public const int A_BIG_INTEGER = 1000;

	public int width, height, pixelsPerUnit;
	public int tileX, tileY;
	public float noiseScale;

	Color[] colors = null;
	bool makeSprite = false;

	// Use this for initialization
	void Start()
	{
		generateTexture();
	}

	// Update is called once per frame
	void Update()
	{
		if(makeSprite)
		{
			makeSprite = false;

			Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
			tex.filterMode = FilterMode.Point;
			tex.SetPixels(colors);
			tex.Apply();

			Sprite sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0, 0), pixelsPerUnit);
			GetComponent<SpriteRenderer>().sprite = sprite;
		}
	}

	void generateTexture()
	{
		new Thread(() =>
		{
			colors = new Color[width * height];
			for(int y = 0; y < height; y++)
			{
				for(int x = 0; x < width; x++)
				{
					int i = y * width + x;

					// The noise function is symmetric for negative values,
					// so a big number must be added to keep everything happy and positive! :)
					float noise = Mathf.PerlinNoise(
						( ( x ) + ( width * tileX ) ) / noiseScale + A_BIG_INTEGER,
						( ( y ) + ( height * tileY ) ) / noiseScale + A_BIG_INTEGER);

					colors[i] = new Color(1, 1, 1, Mathf.Max(0.5f * noise - 0.25f, 0));
					//Debug.Log(i);
					//Debug.Log(colors[i]);
				}
			}

			makeSprite = true;
		}
		).Start();
	}
}
