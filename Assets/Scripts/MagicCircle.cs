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
	AudioSource sfx;
	// Use this for initialization
	void Start ()
	{
		sfx = GetComponent<AudioSource> ();
		sfx.enabled = false;
		for (int i = 0; i < runeList.Length; i++) {
			runePositions [i] = runeList [i].transform.position;
		}
		RectTransform rectTrans = gameObject.GetComponent<RectTransform> ();
		edgeLength = rectTrans.rect.height * 0.425f;

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

	void setReletivePosition (RuneSlot slot, float x, float y)
	{
		slot.gameObject.transform.position = new Vector3 (
			gameObject.transform.position.x + edgeLength * x,
			gameObject.transform.position.y + edgeLength * y,
			slot.transform.position.z);
	}
	
	// Update is called once per frame
	void Update ()
	{

		//put in new rune
		if (Input.GetMouseButtonDown (0)) {
			for (int i = 0; i < 4; i++) {
				//Debug.Log("Distance :")
				if (isNear (runeList [i].transform.position, Input.mousePosition)) {
					current = runeList [i];
					sfx.enabled = true;
				}
			}
		}

		if (Input.GetMouseButton (0) && current != null) {
			current.transform.position = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp (0)) { 
			sfx.enabled = false;
			if (current != null) {
				//check minor slots
				for (int i = 0; i < 8; i++) {
					if (isNear (minorSlots [i], Input.mousePosition)) {
						Rune temp = (Rune)Instantiate (current, minorSlots [i].transform.position, Quaternion.identity);
						temp.transform.SetParent (minorSlots [i].transform);
						if (minorSlots [i].rune != null) {
							Destroy (minorSlots [i].rune.gameObject);
						}
						minorSlots [i].rune = temp;
					}		
				}
				//check major slots
				for (int i = 0; i < 8; i++) {
					if (isNear (slots [i], Input.mousePosition)) {
						Rune temp = (Rune)Instantiate (current, slots [i].transform.position, Quaternion.identity);
						temp.transform.SetParent (slots [i].transform);
						if (slots [i].rune != null) {
							Destroy (slots [i].rune.gameObject);
						}
						slots [i].rune = temp;
					}		
				}

				current.transform.position = runePositions [current.id];
			}

			current = null;
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
		return (position0 - position).magnitude < 20;
	}
}
