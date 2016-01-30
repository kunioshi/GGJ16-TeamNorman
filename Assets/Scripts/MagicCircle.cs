using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MagicCircle : MonoBehaviour
{
	float edgeLength;
	public RuneSlot[] slots;
	public RuneSlot[] minorSlots;
	// Use this for initialization
	void Start ()
	{
		Debug.Log ("slots array size: " + slots.Length);
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
	
	}
}
