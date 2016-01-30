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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveDirection (Direction dir) {
		Vector3 moveTo = transform.position;

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
}
