using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drops : MonoBehaviour {

	float x = 0f;
	float time = 0f;
	float interval = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		dropFloat();
		dropDestroy ();
	}



	void dropFloat() {
		transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(x)*.005f), transform.position.z);
		x += .1f;
	}

	void dropDestroy() {
		time += Time.deltaTime;
		if (time >= interval) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "player" && gameObject.name == "heart(Clone)")
		{
			if (player.playerHealth < 10) {
				player.playerHealth++;
			}
			player.score += 25;
			Destroy(gameObject);
		
		}
		if (col.gameObject.name == "player" && gameObject.name == "boost(Clone)")
		{
			player.powerup = true;
			player.speedBoost = true;
			player.score += 25;
			Destroy(gameObject);
		}
		if (col.gameObject.name == "player" && gameObject.name == "smash(Clone)")
		{
			player.smash = true;
			Destroy(gameObject);
		}
		if (col.gameObject.tag == "behindable") {
			transform.position = new Vector3 (transform.position.x, transform.position.y, 3);
		}
	}

}
