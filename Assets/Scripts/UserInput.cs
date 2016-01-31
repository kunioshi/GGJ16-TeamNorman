using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UserInput : MonoBehaviour
{
	public float inputHorizontal = 0f;
	public float inputVertical = 0f;
	public bool inputMouseButton0 = false;

	public GameObject mainCharacter;
	private PlayerBehaviour playerBehave;

	//	private List<Tile> movePath = new List<Tile> ();
	//	private int curPathIndex = 0;
	
	public TileManager tileManager;
	public PlayerStatus playerStatus;
	public MapLoader mapLoader;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		inputHorizontal = Input.GetAxisRaw ("Horizontal");
		inputVertical = Input.GetAxisRaw ("Vertical");
//		inputMouseButton0 = Input.GetMouseButton (0);

		// Move the player, if possible
		if (playerBehave != null && !playerBehave.isMoving) {
//			if (curPathIndex < movePath.Count) {
//				Direction dir;
//				Vector2i pathTile = movePath [curPathIndex].Position;
//
//				if (pathTile != playerStatus.playerGridPosition) {
//					if (pathTile.x != playerStatus.playerGridPosition.x) {
//						dir = pathTile.x > playerStatus.playerGridPosition.x ? Direction.Right : Direction.Left;
//					} else {
//						dir = pathTile.y > playerStatus.playerGridPosition.y ? Direction.Up : Direction.Down;
//					}
//
//					// TODO: Refactor to change the other movements too
//					switch (dir) {
//					case Direction.Right:
//						playerStatus.playerGridPosition.x += 1;
//						break;
//					case Direction.Left:
//						playerStatus.playerGridPosition.x -= 1;
//						break;
//					case Direction.Up:
//						playerStatus.playerGridPosition.y += 1;
//						break;
//					case Direction.Down:
//						playerStatus.playerGridPosition.y -= 1;
//						break;
//					}
//
//					// Start the move animation
//					playerBehave.MoveDirection (dir);
//					LoadMapOnWalk ();
//				}
//
//				curPathIndex++;
//			} else 
			Vector2i newPosition = playerStatus.playerGridPosition;
			if (inputHorizontal != 0) {
				Direction dir = inputHorizontal > 0 ? Direction.Right : Direction.Left;
				newPosition.x += (int)inputHorizontal;


				if (tryToMoveTo (newPosition)) {
					// Start the move animation
					playerBehave.MoveDirection (dir);
					LoadMapOnWalk ();
				}
			} else if (inputVertical != 0) {
				Direction dir = inputVertical > 0 ? Direction.Up : Direction.Down;
				newPosition.y += (int)inputVertical;

				if (tryToMoveTo (newPosition)) {
					// Start the move animation
					playerBehave.MoveDirection (dir);
					LoadMapOnWalk ();
				}
			}
//			else if (inputMouseButton0) {
//				GetMouseClickPosition ();
//			}
		}
	}

	void OnLevelWasLoaded (int level)
	{
		// When the DayScene(1) is loaded, get the MainCharacter
		if (level == SceneManager.GetSceneByName ("DayScene").buildIndex) {
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

	void LoadMapOnWalk ()
	{
		mapLoader.LoadMapForWalk ();
		mapLoader.DisablePlayerGridTile ();
	}

	//	void GetMouseClickPosition () {
	//		// Converting Mouse Pos to 2D (Vector2) World Pos
	//		Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
	//		RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);
	//
	//		if (hit) {
	//			Vector2i hitPos = new Vector2i ((int)hit.transform.position.x, (int)hit.transform.position.y);
	//
	//			// Convert 3D position into Grid Position
	//			hitPos.x /= (int)hit.collider.gameObject.GetComponent<SpriteRenderer> ().bounds.size.x;
	//			hitPos.y /= (int)hit.collider.gameObject.GetComponent<SpriteRenderer> ().bounds.size.y;
	//
	//			// Pathfinder
	//			movePath = tileManager.getPath (playerStatus.playerGridPosition, hitPos, playerStatus.playerEnergy);
	//			curPathIndex = 0;
	//		}
	//	}

	bool tryToMoveTo (Vector2i newPosition)
	{
		Tile currTile = tileManager.getTile (newPosition);
		float deathBonus = 1;
		if (playerStatus.bonuses [2]) {
			deathBonus = 2f / 3f;
		}
		int cost = 0;
		int reward = 0;
		switch (currTile.Type) {
		case Tile.TileType.Plains:
			cost = Mathf.RoundToInt (Tile.defaultTerrainPenalties [(int)Tile.TileType.Plains] * deathBonus);
			if (playerStatus.playerEnergy >= cost) {
				playerStatus.playerEnergy -= cost;
				playerStatus.playerGridPosition = newPosition;
				return true;
			}
			return false;
		case Tile.TileType.Mountain:
			cost = Mathf.RoundToInt (Tile.defaultTerrainPenalties [(int)Tile.TileType.Mountain] * deathBonus);
			if (playerStatus.playerEnergy >= cost) {
				playerStatus.playerEnergy -= cost;
				playerStatus.playerGridPosition = newPosition;
				if (currTile.gridTile.GetComponent<GridTileTexture> ().enabled) {
					switch (Random.Range (0, 3)) {
					case 0:
						reward = 2;
						break;
					case 1:
						reward = 3;
						break;
					case 2:
						reward = 4;
						break;
					default:
						Debug.Assert (false);
						break;
					}
					if (playerStatus.bonuses [(int)RuneId.Fire]) {
						reward *= Random.Range (1, 3);
					}
					playerStatus.runeCounts [(int)RuneId.Earth] += reward;
				}
				return true;
			}
			return false;
		case Tile.TileType.Cave:
			cost = Mathf.RoundToInt (Tile.defaultTerrainPenalties [(int)Tile.TileType.Cave] * deathBonus);
			if (playerStatus.playerEnergy >= cost) {
				playerStatus.playerEnergy -= cost;
				playerStatus.playerGridPosition = newPosition;
				if (currTile.gridTile.GetComponent<GridTileTexture> ().enabled) {
					reward = 2;
					if (playerStatus.bonuses [(int)RuneId.Fire]) {
						reward *= Random.Range (1, 3);
					}
					playerStatus.runeCounts [(int)RuneId.Life] += reward;
				}
				return true;
			}
			return false;
		case Tile.TileType.Graveyard:
			cost = Mathf.RoundToInt (Tile.defaultTerrainPenalties [(int)Tile.TileType.Graveyard] * deathBonus);
			if (playerStatus.playerEnergy >= cost) {
				playerStatus.playerEnergy -= cost;
				playerStatus.playerGridPosition = newPosition;
				if (currTile.gridTile.GetComponent<GridTileTexture> ().enabled) {
					switch (Random.Range (0, 2)) {
					case 0:
						reward = 3;
						break;
					case 1:
						reward = 7;
						break;
					default:
						Debug.Assert (false);
						break;
					}
					if (playerStatus.bonuses [(int)RuneId.Fire]) {
						reward *= Random.Range (1, 3);
					}
					playerStatus.runeCounts [(int)RuneId.Death] += reward;
				}
				return true;
			}
			return false;
		case Tile.TileType.Volcano:
			cost = Mathf.RoundToInt (Tile.defaultTerrainPenalties [(int)Tile.TileType.Volcano] * deathBonus);
			if (playerStatus.playerEnergy >= cost) {
				playerStatus.playerEnergy -= cost;
				playerStatus.playerGridPosition = newPosition;
				if (currTile.gridTile.GetComponent<GridTileTexture> ().enabled) {
					switch (Random.Range (0, 3)) {
					case 0:
						reward = 2;
						break;
					case 1:
						reward = 6;
						break;
					case 2:
						reward = 10;
						break;
					default:
						Debug.Assert (false);
						break;
					}
					if (playerStatus.bonuses [(int)RuneId.Fire]) {
						reward *= Random.Range (1, 3);
					}
					playerStatus.runeCounts [(int)RuneId.Fire] += reward;
				}
				return true;
			}
			return false;
		default:
			Debug.Assert (false);
			return false;
		}
	}
}
 