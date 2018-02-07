using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLayer : MonoBehaviour
{
	public BackgroundTile tilePrefab;
	public float depth;

	private List<BackgroundTile> tiles = new List<BackgroundTile>();

	// Use this for initialization
	void Start()
	{
	}
	
	public void CameraUpdate(Vector3 camPos)
	{
		transform.position = depth * new Vector3(camPos.x, camPos.y, 0);

		int tileX = Mathf.FloorToInt((camPos.x - transform.position.x) * tilePrefab.pixelsPerUnit / tilePrefab.width);
		int tileY = Mathf.FloorToInt((camPos.y - transform.position.y) * tilePrefab.pixelsPerUnit / tilePrefab.height);

		instantiateTiles(tileX, tileY);
		clearTiles(tileX, tileY);

		Debug.Log("(" + tileX + ", " + tileY + ")");
	}

	void instantiateTiles(int tileX, int tileY)
	{
		instantiateTile(tileX - 1, tileY - 1); instantiateTile(tileX + 0, tileY - 1); instantiateTile(tileX + 1, tileY - 1);
		instantiateTile(tileX - 1, tileY + 0); instantiateTile(tileX + 0, tileY + 0); instantiateTile(tileX + 1, tileY + 0);
		instantiateTile(tileX - 1, tileY + 1); instantiateTile(tileX + 0, tileY + 1); instantiateTile(tileX + 1, tileY + 1);
	}

	void instantiateTile(int tileX, int tileY)
	{
		foreach(BackgroundTile bgt in tiles)
		{
			if(bgt.tileX == tileX && bgt.tileY == tileY)
			{
				return;
			}
		}

		BackgroundTile tile = Instantiate(tilePrefab.gameObject).GetComponent<BackgroundTile>();
		tile.tileX = tileX;
		tile.tileY = tileY;
		tile.transform.localPosition = new Vector3(tileX * tile.width / tile.pixelsPerUnit, tileY * tile.height / tile.pixelsPerUnit, 0);
		tile.transform.SetParent(transform, false);

		tiles.Add(tile);
	}

	void clearTiles(int tileX, int tileY)
	{
		List<BackgroundTile> clearedTiles = new List<BackgroundTile>();

		foreach(BackgroundTile tile in tiles)
		{
			if(tile.tileX < tileX - 1 || tile.tileX > tileX + 1 || tile.tileY < tileY - 1 || tile.tileY > tileY + 1)
			{
				Destroy(tile.gameObject);
				clearedTiles.Add(tile);
			}
		}

		foreach(BackgroundTile tile in clearedTiles)
		{
			tiles.Remove(tile);
		}
	}
}
