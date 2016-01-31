using UnityEngine;
using System.Collections;

public class PersistentObject : MonoBehaviour {
	void Awake () {
		DontDestroyOnLoad (gameObject);

		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}
}
