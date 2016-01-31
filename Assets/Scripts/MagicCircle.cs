using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MagicCircle : MonoBehaviour
{
	float edgeLength;
	// a list of each type of rune
	public Rune[] runeList;
	Vector3[] runePositions = new Vector3[4];

	private Rune current;
	public RuneSlot[] slots;
	public RuneSlot[] minorSlots;
	public GameObject gameController;
	public PlayerStatus playerStatus;
	public Text[] runeCountTexts;
	public AudioSource[] runeSFX;
	public GameObject topLayer;
	// Use this for initialization
	void Start ()
	{
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		playerStatus = gameController.GetComponent<PlayerStatus> ();

		for (int i = 0; i < runeList.Length; i++) {


			if (i == 0 || i == 3) {
				runeList [i].transform.position = new Vector3 (
					Screen.width * 0.1f,
					Screen.height * (0.7f - i * 0.16f),
					runeList [i].transform.position.z
				);
				runeCountTexts [i].transform.position = runeList [i].transform.position + new Vector3 (70, -50, 0);

			} else {
				runeList [i].transform.position = new Vector3 (
					Screen.width * 0.9f,
					Screen.height * (1.18f - i * 0.48f),
					runeList [i].transform.position.z);

				runeCountTexts [i].transform.position = runeList [i].transform.position + new Vector3 (-100, -50, 0);
			}
			runePositions [i] = runeList [i].transform.position;
			setTextByIndex (i);
			runeSFX [i].enabled = false;
		}
		RectTransform rectTrans = gameObject.GetComponent<RectTransform> ();
		edgeLength = rectTrans.rect.height * 0.36f;

		//top
		setReletivePosition (slots [0], 0f, 1f);
		//top right
		setReletivePosition (slots [1], 0.707f, 0.707f);
		//right
		setReletivePosition (slots [2], 1f, 0f);
		//bottom right
		setReletivePosition (slots [3], 0.707f, -0.707f);
		//bottom
		setReletivePosition (slots [4], 0f, -1f);
		//buttom left
		setReletivePosition (slots [5], -0.707f, -0.707f);
		//left
		setReletivePosition (slots [6], -1f, 0f);
		//top left
		setReletivePosition (slots [7], -0.707f, 0.707f);


		//minor slots
		//top top right
		setReletivePosition (minorSlots [0], 0.291f, 0.707f);
		//top right right
		setReletivePosition (minorSlots [1], 0.707f, 0.291f);
		//bottom right right
		setReletivePosition (minorSlots [2], 0.707f, -0.291f);
		//bottom bottom right
		setReletivePosition (minorSlots [3], 0.291f, -0.707f);
		//bottom bottom left
		setReletivePosition (minorSlots [4], -0.291f, -0.707f);
		//bottom left left
		setReletivePosition (minorSlots [5], -0.707f, -0.291f);
		//top left left
		setReletivePosition (minorSlots [6], -0.707f, 0.291f);
		//top top left
		setReletivePosition (minorSlots [7], -0.291f, 0.707f);

	}
	// updated for the new background, has an extra transform in -y direction
	void setReletivePosition (RuneSlot slot, float x, float y)
	{
		slot.gameObject.transform.position = new Vector3 (
			gameObject.transform.position.x + edgeLength * x,
			gameObject.transform.position.y + edgeLength * y - Screen.height * 0.072f,
			slot.transform.position.z);
	}
	
	// Update is called once per frame
	void Update ()
	{

		//put in new rune
		if (Input.GetMouseButtonDown (0)) {
			//click on inventory
			for (int i = 0; i < 4; i++) {
				if (isNear (runeList [i].transform.position, Input.mousePosition) && playerStatus.runeCounts [i] > 0) {
					current = runeList [i];
					//pick up a rune: -1
					playerStatus.RemoveRuneFromInventory (current.id);
					break;
				}
			}
			// click on major slots
			for (int i = 0; i < 8; i++) {
				if (isNear (slots [i], Input.mousePosition) && slots [i].rune != null) {
					current = slots [i].rune;
					slots [i].rune = null;
					break;
				}
			}

			// click on minor slots
			for (int i = 0; i < 8; i++) {
				//Debug.Log("Distance :")
				if (isNear (minorSlots [i], Input.mousePosition) && minorSlots [i].rune != null) {
					current = minorSlots [i].rune;
//					sfx.enabled = true;
					minorSlots [i].rune = null;
					break;
				}
			}
			if (current != null) {
				runeSFX [current.id].enabled = true;
			}
		}

		if (Input.GetMouseButton (0) && current != null) {
			//make sure the rune is always on top, use water symbol since it's the last element on canvas
			current.img.transform.SetParent (topLayer.transform);
			current.transform.position = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp (0)) { 
			if (current != null) {
				runeSFX [current.id].enabled = false;
				//check minor slots
				for (int i = 0; i < 8; i++) {
					if (isNear (minorSlots [i], Input.mousePosition)) {
						Rune temp = (Rune)Instantiate (current, minorSlots [i].transform.position, Quaternion.identity);
						temp.transform.SetParent (minorSlots [i].transform);
						if (minorSlots [i].rune != null) {
							playerStatus.AddRuneToInventory (minorSlots [i].rune.id);
							Debug.Log (playerStatus.runeCounts [current.id] + "minor replace");

							Destroy (minorSlots [i].rune.gameObject);
						}
						minorSlots [i].rune = temp;
						//	holding--;
						current.transform.position = runePositions [current.id];
						current = null;
						break;
					}		
				}
				//check major slots
				for (int i = 0; i < 8; i++) {
					if (isNear (slots [i], Input.mousePosition)) {
						Rune temp = (Rune)Instantiate (current, slots [i].transform.position, Quaternion.identity);
						temp.transform.SetParent (slots [i].transform);
						if (slots [i].rune != null) {
							//move the rune on the magical circle back to inventory
							playerStatus.AddRuneToInventory (slots [i].rune.id);
							Destroy (slots [i].rune.gameObject);
						}
						slots [i].rune = temp;
						current.transform.position = runePositions [current.id];
						current = null;
						break;
					}		
				}



			}
			//release rune from hand will always increase the inventory, no matter where it goes, handle the drop on circle somewhere else
			if (current != null) {
				playerStatus.AddRuneToInventory (current.id);
				current.transform.position = runePositions [current.id];
				current = null;
			}

			for (int i = 0; i < 4; i++) {
				setTextByIndex (i);
			}
		}

	}

	//given a position and a runslot position, return ture if they are close to each other
	public bool isNear (RuneSlot rs, Vector3 position)
	{
		return isNear (rs.transform.position, position);
	}

	//given 2 positions, return ture if they are close to each other
	public bool isNear (Vector3 position0, Vector3 position)
	{
		return (position0 - position).magnitude < 35;
	}


	//helper function for getting the bonus index
	//please only use this function for major rune slot
	int bothConnected (int i)
	{
		if (slots [i].rune == null) {
			Debug.Log ('A');
			return -1;

		}
		//adjacent minor rune slot
		int k;
		//handling null cases

		if (i > 0) {
			k = i - 1;
		} else
			k = 7;
		if (minorSlots [i].rune == null || minorSlots [k].rune == null) {//
			Debug.Log ("B");
			return -1;

		}

		//check triangle
		if (slots [i].rune.id == minorSlots [i].rune.id && slots [i].rune.id == minorSlots [k].rune.id) {
			return slots [i].rune.id;
		}
		Debug.Log ("C");
		return -1;

	}
	//set up the text base on the index
	void setTextByIndex (int i)
	{
		if (i == 0 || i == 3) {
			runeCountTexts [i].text = "x" + playerStatus.runeCounts [i];
		} else {
			runeCountTexts [i].text = playerStatus.runeCounts [i] + "x";
		}
	}


	//set up the bonus and the engery for the next day
	//shall be call pressing the button to go to the next day
	public void applyReward ()
	{
		bool[] bonuses = new bool[4]{ false, false, false, false };
		int index, majorRuneCounter = 0;
		for (int i = 0; i < 8; i++) {
			index = bothConnected (i);
			Debug.Log ("slotIndex: " + i + "bonusIndex" + index);
			if (slots [i].rune != null) {
				majorRuneCounter++;
			}
			if (index != -1) {
				bonuses [index] = true;
			}
		}
		playerStatus.bonuses = bonuses;
		float energyMultiplier = 1;
		if (playerStatus.bonuses [1] == true) {
			energyMultiplier = 1.5f;
		}
		int bonusVision = 0;
		if (playerStatus.bonuses[0] == true) {
			bonusVision = 1;
		}
		//new energy in the next day default energy * runes in outter ring * (1.5 with life bonus or 1 without)  
		playerStatus.playerEnergy = Mathf.RoundToInt ((float)PlayerStatus.DefaultEnergy * (float)majorRuneCounter / 8f * energyMultiplier);
<<<<<<< HEAD
=======
		playerStatus.playerVisionRange = PlayerStatus.DefaultVision + bonusVision;
		Debug.Log ("Life: " + bonuses [0] + "Death: " + bonuses [1] + "Earth: " + bonuses [2] + "Fire: " + bonuses [3] + "\nEnergy: " + playerStatus.playerEnergy);
		Debug.Log ("Vision: " + playerStatus.playerVisionRange);
>>>>>>> 911f4d4345f8e7bb231cbd55ae47d7b0fd14fdfd
	}
}
