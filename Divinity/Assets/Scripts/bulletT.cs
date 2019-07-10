using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletT : MonoBehaviour {

	//bullet components
	Rigidbody2D rb;

	public float bulletSpeed;
	float time = 0f;
	float despawntime = 2f;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();



		if (playerT.directionT == 0) {
			transform.localScale = new Vector2 (0.0807231f, 15.29459f);
			rb.velocity = new Vector2 (0f, bulletSpeed);
		}
		if (playerT.directionT == 1) {
			transform.localScale = new Vector2 (0.0807231f, 15.29459f);
			rb.velocity = new Vector2 (0f, -bulletSpeed);
		}
		if (playerT.directionT == 2) {
			rb.velocity = new Vector2 (bulletSpeed, 0f);
		}
		if (playerT.directionT == 3) {
			rb.velocity = new Vector2 (-bulletSpeed, 0f);
		}
	}


	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if (time >= despawntime) {
			Destroy (gameObject);
		}

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "walls") {
			Destroy (gameObject);
		}
		if (col.gameObject.tag == "behindable") {
			transform.position = new Vector3 (transform.position.x, transform.position.y, 3);
		}
	}
}
