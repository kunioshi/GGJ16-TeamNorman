using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class MainMenuInput : MonoBehaviour {
    
	void Start () {
        
    }

    public void ToGame()
    {
        SceneManager.LoadScene("NightScene");
    }


    public void Exit()
    {
        Application.Quit();
    }
    
	
	// Update is called once per frame
	void Update () {
        
	}
}
