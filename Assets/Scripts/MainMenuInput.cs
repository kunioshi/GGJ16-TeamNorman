using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class MainMenuInput : MonoBehaviour
{
    
	public AudioSource click;

	void Start ()
	{

	}



	public void ToGame ()
	{
		click.enabled = true;
		SceneManager.LoadScene ("NightScene");
	}


	public void Exit ()
	{
		click.enabled = true;
		Application.Quit ();
	}
    
	
	// Update is called once per frame
	void Update ()
	{
        
	}
}
