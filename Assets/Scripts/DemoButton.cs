using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DemoButton : MonoBehaviour
{

	public GameObject gameController;
	public PlayerStatus playerStatus;
	public TileManager tileManager;

	// Use this for initialization
	void Start ()
	{
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		playerStatus = gameController.GetComponent<PlayerStatus> ();
		tileManager = gameController.GetComponent<TileManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void ChangeScene (int levelIndex)
	{
		SceneManager.LoadScene (levelIndex);
	}

    public void ToMenu()
    {
		DestroyGameController ();
        SceneManager.LoadScene("MainMenuScene");
	}

	public void ToNight()
	{
		SceneManager.LoadScene("NightScene");
	}

	public void ToDay()
	{
		playerStatus.day += 1;
		tileManager.probabilities[0] += 1;
		Debug.Log (tileManager.probabilities[0]);
		SceneManager.LoadScene("DayScene");
	}

	public void DestroyGameController ()
	{
		Destroy (GameObject.Find ("GameController"));
	}

	public void ExitProgram () {
		Application.Quit();
	}
}
