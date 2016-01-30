using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour {
	public PlayerStatus playerStatus;
	public TileManager tileManager;

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
		tileManager.getVisibleTiles (playerStatus.playerGridPosition);
	}
}
