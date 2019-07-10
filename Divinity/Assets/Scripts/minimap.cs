using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour {

	private Vector3 playerPos;
	private Vector3 newPos;
	private Vector3 velocity = Vector3.one;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate()
	{
		if (player.playerHealth > 0) {
			playerPos = GameObject.Find ("player").transform.position;
			newPos = new Vector3 (playerPos.x, playerPos.y, transform.position.z);
			transform.position = Vector3.SmoothDamp (transform.position, newPos, ref velocity, .05f);
		}
	}
}
