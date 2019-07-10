using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clouds : MonoBehaviour {

	Rigidbody2D cb;

	public float maxCloudPos;
	public float startCloudPos;
	// Use this for initialization
	void Start () {
		cb = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		cb.velocity = new Vector2 (-1f, 0f);

		if (transform.position.x <= maxCloudPos) {
			transform.position = new Vector3 (startCloudPos, transform.position.y, transform.position.z);
		}
	}
}
