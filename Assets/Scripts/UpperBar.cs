using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpperBar : MonoBehaviour
{
	public Image[] textShadows;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < 4; i++) {
			textShadows [i].transform.position = new Vector3 (
				Screen.width * 0.1f + i * Screen.width * 0.12f,
				Screen.height * 0.96f,
				0f);
			textShadows [i].GetComponentInChildren<Text> ().transform.position = textShadows [i].transform.position + new Vector3 (0, -10, 0);
			;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
