using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enmBehind : MonoBehaviour {

	GameObject enm;
	// Use this for initialization
	void Start () {
		enm = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "behindable") {
			enm.transform.position = new Vector3 (transform.position.x, transform.position.y, 3);
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == "behindable") {
			enm.transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
		}
	}
}
	
