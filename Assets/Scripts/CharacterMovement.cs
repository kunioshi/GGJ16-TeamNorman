using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	public bool isMoving { get; private set; }

	private Vector3 moveToPosition;
	private float moveSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving) {
			Move ();
		}
	}

	public void MoveToPosition (Vector3 targetPosition, float movementSpeed) {
		moveToPosition = targetPosition;
		moveSpeed = movementSpeed;

		isMoving = true;
	}

	public void Move () {
		transform.position = Vector3.MoveTowards (transform.position, moveToPosition, moveSpeed * Time.deltaTime);

		if (transform.position == moveToPosition) {
			isMoving = false;
		}
	}
}
