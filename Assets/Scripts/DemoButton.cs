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

	public void ChangeScene ()
	{
		SceneManager.LoadScene (1);
	}
}
