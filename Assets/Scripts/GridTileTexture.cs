using UnityEngine;
using System.Collections;

public class GridTileTexture : MonoBehaviour {
	public SpriteRenderer sprRender;

	public Sprite hoverTexture;

	public bool enabled { get; private set; }

	// Use this for initialization
	void Start () {
		enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DisableGridTile () {
		sprRender.sprite = hoverTexture;
		enabled = false;
	}
}
