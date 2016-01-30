using UnityEngine;
using System.Collections;

public class DemoTile : MonoBehaviour {
	public GameObject grassPrefab;

	// Use this for initialization
	void Start () {
		Vector3 mySize = GetComponent<SpriteRenderer> ().bounds.size;
		Vector3 myPos = transform.position;
		myPos.x = 2 * mySize.x;

		Instantiate (grassPrefab, new Vector3 (myPos.x + mySize.x, myPos.y, myPos.z), transform.rotation);
		Instantiate (grassPrefab, new Vector3 (myPos.x - mySize.x, myPos.y, myPos.z), transform.rotation);
		Instantiate (grassPrefab, new Vector3 (myPos.x, myPos.y + mySize.y, myPos.z), transform.rotation);
		Instantiate (grassPrefab, new Vector3 (myPos.x, myPos.y - mySize.y, myPos.z), transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
