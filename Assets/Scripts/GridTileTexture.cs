using UnityEngine;
using System.Collections;

public class GridTileTexture : MonoBehaviour {
	public SpriteRenderer sprRender;

	public Sprite hoverTexture;

//	public bool Enabled { get; private set; }

	// Use this for initialization
	void Start () {
//		Enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DisableGridTile () {
		sprRender.sprite = hoverTexture;
//		Enabled = false;
	}
}
