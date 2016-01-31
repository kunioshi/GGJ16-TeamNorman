using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class MainMenuInput : MonoBehaviour {
	public AudioSource clickSound;
	void Start () {
        
    }

    public void ToGame()
    {
		clickSound.Play ();
        SceneManager.LoadScene("NightScene");
    }


    public void Exit()
	{
		clickSound.Play ();
        Application.Quit();
    }
    
	
	// Update is called once per frame
	void Update () {
        
	}
}
