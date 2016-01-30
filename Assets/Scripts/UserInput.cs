using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour {
	public float inputHorizontal = 0f;
	public float inputVertical = 0f;

	public GameObject mainCharacter;
	private PlayerBehaviour playerBehave;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		inputHorizontal = Input.GetAxisRaw ("Horizontal");
		inputVertical = Input.GetAxisRaw ("Vertical");

		if (playerBehave != null && !playerBehave.isMoving) {
			if (inputHorizontal != 0) {
				Direction dir = inputHorizontal > 0 ? Direction.Right : Direction.Left;
				playerBehave.MoveDirection (dir);
			} else if (inputVertical != 0) {
				Direction dir = inputVertical > 0 ? Direction.Up : Direction.Down;
				playerBehave.MoveDirection (dir);
			}
		}
	}

	void OnLevelWasLoaded (int level) {
		// When the DayScene is loaded, get the MainCharacter
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
}
