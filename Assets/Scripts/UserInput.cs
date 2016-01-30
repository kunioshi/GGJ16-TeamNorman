using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserInput : MonoBehaviour {
	public float inputHorizontal = 0f;
	public float inputVertical = 0f;
	public bool inputMouseButton0 = false;

	public GameObject mainCharacter;
	private PlayerBehaviour playerBehave;
	
	public TileManager tileManager;
	public PlayerStatus playerStatus;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		inputHorizontal = Input.GetAxisRaw ("Horizontal");
		inputVertical = Input.GetAxisRaw ("Vertical");
		inputMouseButton0 = Input.GetMouseButton (0);

		// Move the player, if possible
		if (playerBehave != null && !playerBehave.isMoving) {
			if (inputHorizontal != 0) {
				Direction dir = inputHorizontal > 0 ? Direction.Right : Direction.Left;

				// Update the player's position on the grid
				playerStatus.playerGridPosition.x += (int)inputHorizontal;

				// Start the move animation
				playerBehave.MoveDirection (dir);
			} else if (inputVertical != 0) {
				Direction dir = inputVertical > 0 ? Direction.Up : Direction.Down;

				// Update the player's position on the grid
				playerStatus.playerGridPosition.y += (int)inputVertical;

				// Start the move animation
				playerBehave.MoveDirection (dir);
			} else if (inputMouseButton0) {
				GetMouseClickPosition ();
			}
		}
	}

	void OnLevelWasLoaded (int level) {
		// When the DayScene(1) is loaded, get the MainCharacter
		if (level == 1) {
			mainCharacter = GameObject.Find ("MainCharacter");

			if (mainCharacter == null) {
				Debug.LogError ("No 'MainCharacter' was found in the scene " + level + ".");
			} else {
				playerBehave = mainCharacter.GetComponent<PlayerBehaviour> ();

				if (playerBehave == null) {
					Debug.LogError ("No PlayerBehaviour script was found in the 'mainCharacter'.");
				}
			}
		}
	}

	void GetMouseClickPosition () {
		//Converting Mouse Pos to 2D (vector2) World Pos
		Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

		if (hit) {
			Vector2i hitPos = new Vector2i ((int)hit.transform.position.x, (int)hit.transform.position.y);
			hitPos.x /= (int)hit.collider.gameObject.GetComponent<SpriteRenderer> ().bounds.size.x;
			hitPos.y /= (int)hit.collider.gameObject.GetComponent<SpriteRenderer> ().bounds.size.y;

//			tileManager.getPath (playerStatus.playerGridPosition, hitPos, playerStatus.playerEnergy);

//			if (path != null) {
//				playerBehave.MoveThroughPath (path);
//			}
		}
	}
}
 