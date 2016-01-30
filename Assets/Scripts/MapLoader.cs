using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapLoader : MonoBehaviour {
	public PlayerStatus playerStatus;
	public TileManager tileManager;

	public GameObject[] plainPrefabs;
	public GameObject[] forestPrefabs;
	public GameObject[] mountainPrefabs;
	public GameObject[] volcanoPrefabs;

	private List<Tile> loadedTiles = new List<Tile> ();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnLevelWasLoaded (int level) {
		if (level == 1) {
			PrintInitialMap ();
		}
	}

	void PrintInitialMap () {
		List<Tile> visibleTiles = tileManager.getVisibleTiles (playerStatus.playerGridPosition);

		foreach (Tile tile in visibleTiles) {
			CreateGridTile (tile);
		}

		// Save/Reset the already loaded tiles
		loadedTiles = visibleTiles;
	}

	public void LoadMapForWalk () {
		List<Tile> newVisibleTiles = tileManager.getVisibleTiles (playerStatus.playerGridPosition);

		foreach (Tile tile in newVisibleTiles) {
			if (!CheckLoadedTile (tile)) {
				CreateGridTile (tile);

				loadedTiles.Add (tile);
			}
		}
	}

	bool CheckLoadedTile (Tile tile) {
		foreach (Tile t in loadedTiles) {
			if (t.Position == tile.Position) {
				return true;
			}
		}

		return false;
	}

	void CreateGridTile (Tile tile) {
		GameObject gridTile = plainPrefabs [0];
		Vector3 tile3DPosition = Vector3.zero;

		switch (tile.Type) {
		case Tile.TileType.Plains:
			gridTile = plainPrefabs [Random.Range (0, plainPrefabs.Length)];
			tile3DPosition = new Vector3 (tile.Position.x, tile.Position.y, 1);
			break;
		case Tile.TileType.Forest:
			gridTile = forestPrefabs [Random.Range (0, forestPrefabs.Length)];
			tile3DPosition = new Vector3 (tile.Position.x, tile.Position.y, 1);
			break;
		case Tile.TileType.Mountain:
			gridTile = mountainPrefabs [Random.Range (0, mountainPrefabs.Length)];
			tile3DPosition = new Vector3 (tile.Position.x, tile.Position.y, 1);
			break;
		case Tile.TileType.Volcano:
			gridTile = volcanoPrefabs [Random.Range (0, volcanoPrefabs.Length)];
			tile3DPosition = new Vector3 (tile.Position.x, tile.Position.y, 1);
			break;
		default:
			gridTile = plainPrefabs [Random.Range (0, plainPrefabs.Length)];
			tile3DPosition = new Vector3 (tile.Position.x, tile.Position.y, 1);
			break;
		}

		// Include the size of the prefab in the math of the 3D position
		tile3DPosition *= gridTile.GetComponent<SpriteRenderer> ().bounds.size.x;

		Instantiate (gridTile, tile3DPosition, transform.rotation);
	}
}
