using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midstairs : MonoBehaviour {

	bool above = true;
	public int normVal;
	public int behVal;
	public string colName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (above == true) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, normVal);
		} else
			transform.position = new Vector3 (transform.position.x, transform.position.y, behVal);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == colName) {
			above = false;
		} 
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == colName) {
			above = true;
		} 
	}
}
