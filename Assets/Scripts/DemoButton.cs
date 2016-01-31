using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DemoButton : MonoBehaviour
{
	
	// Use this for initialization
	void Start ()
	{
	
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
		Destroy (GameObject.Find ("GameController"));
        SceneManager.LoadScene("MainMenuScene");
	}

	public void ToNight()
	{
		SceneManager.LoadScene("NightScene");
	}

	public void ToDay()
	{
		SceneManager.LoadScene("DayScene");
	}
}
