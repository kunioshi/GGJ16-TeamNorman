using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehaviour : MonoBehaviour {
	public CharacterMovement movementScript;

	[Tooltip("The distance, in Unity units, that the character will move.")]
	public float moveDistance = 3.0f;
	[Tooltip("How fast the character will move.")]
	public float moveSpeed = 0.1f;

	public bool isMoving { get { return movementScript.isMoving; } private set {} }

	private List<Tile> pathTiles = new List<Tile> ();
	private int currentPathIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// If it still have a path to walk, continue the path
		if (!isMoving && currentPathIndex++ < pathTiles.Count) {
			Vector3 tilePosition = new Vector3 (pathTiles[currentPathIndex].Position.x, pathTiles[currentPathIndex].Position.y);
			movementScript.MoveToPosition (tilePosition, moveSpeed);
		}
	}

	public void MoveDirection (Direction dir) {
		Vector3 moveTo = transform.position;

		// Find out the direction it is moving
		switch (dir) {
			case Direction.Up:
				moveTo.y += moveDistance;
				break;
			case Direction.Down:
				moveTo.y -= moveDistance;
				break;
			case Direction.Right:
				moveTo.x += moveDistance;
				break;
			case Direction.Left:
				moveTo.x -= moveDistance;
				break;
		}

		movementScript.MoveToPosition (moveTo, moveSpeed);
	}

	public void MoveThroughPath (List<Tile> path) {
		pathTiles = path;
	}
}
