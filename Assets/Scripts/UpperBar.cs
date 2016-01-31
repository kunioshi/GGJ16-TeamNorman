using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpperBar : MonoBehaviour
{
	public Image[] textShadows;
	Text[] runeCounts = new Text[4];
	public Text dayText;
	public Text energyText;
	public PlayerStatus playerStatus;
	// Use this for initialization
	void Start ()
	{
		playerStatus = GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerStatus> ();
		//set up inventory text position
		for (int i = 0; i < 4; i++) {
			textShadows [i].transform.position = new Vector3 (
				Screen.width * 0.1f + i * Screen.width * 0.12f,
				Screen.height * 0.96f,
				0f);
			runeCounts [i] = textShadows [i].GetComponentInChildren<Text> ();
			runeCounts [i].transform.position = textShadows [i].transform.position + new Vector3 (0, -10, 0);
			;
		}

		//set up day text
		dayText.transform.position = new Vector3 (
			Screen.width * 0.675f,
			Screen.height * 0.91f,
			0f);

		//set up energy text
		energyText.transform.position = new Vector3 (
			Screen.width * 0.925f,
			Screen.height * 0.91f,
			0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		updateText ();
	}

	void updateText ()
	{
		for (int i = 0; i < 4; i++) {
			runeCounts [i].text = playerStatus.runeCounts [i].ToString ();
		}
		energyText.text = playerStatus.playerEnergy.ToString ();
	}
}
