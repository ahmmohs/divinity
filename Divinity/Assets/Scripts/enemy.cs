using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {


	public float enemySpeed;
	public static float enemyDmg = 1;
	public static float bulDmg =1;
	public static float sDmg = .5f;
	public float enemyHealth;
	public float maxV;
	public float maxH;
	private Vector3 playerPos;
	private Vector2 playerDir;
	private float xDiff;
	private float yDiff;
	public Vector2 maxSpeed;

	public int chanceToDrop;
	Rigidbody2D rb;
	Rigidbody2D pb2;

	//dropping prefabs
	public GameObject drop; //for the bullet
	public GameObject drop1; //for the bullet
	public GameObject drop2; //for the bullet
	bool killedBysword = false;

	AudioSource audio;

	//camera
	GameObject camObj;
	camera Camera;
	public AudioClip impactClip;
	public AudioClip deathSound;

	public GameObject[] drops;

	// Use this for initialization
	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();
		enemyHealth = enemyHealth + spawner.enmHealth;
		enemySpeed = enemySpeed + spawner.enmSpeed;
		if (enemyHealth >= maxH) {
			enemyHealth = maxH;
		}
		if (tutorial.isTutorial == false) {
			player.numEnemies++;
		}
		rb = gameObject.GetComponent<Rigidbody2D> ();
		if (player.playerHealth > 0) {
			pb2 = GameObject.Find ("player").GetComponent<Rigidbody2D> ();
		}
		chanceToDrop = Random.Range(0, 7);

		camObj = GameObject.Find ("Main Camera");
		Camera = camObj.GetComponent<camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.paused == false) {
			if (player.playerHealth > 0) {
				playerTracking ();
				onDeath ();
			}
		}
	}

	float deathTime = 0f;
	float deathInterval = .75f;

	void onDeath() {
		if (enemyHealth <= 0) {
			if (tutorial.isTutorial == true) {
				tutorial.numEnemies--;
			}

			if (tutorial.isTutorial == false) {
				player.numEnemies--;
				player.score += 100;
				player.exp += 75;
				if (killedBysword == true) {
					player.shotsRemain += 3;
				}
			}

				Destroy (gameObject);


			if (tutorial.isTutorial == false) {
				if (chanceToDrop < 1) {
					int dropwhat = Random.Range (0, 10);
					Vector3 spawnPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
					if (dropwhat <= 5) {
						GameObject.Instantiate (drop, spawnPos, Quaternion.identity);
					}
					if (dropwhat == 6 || dropwhat == 7) {
						GameObject.Instantiate (drop1, spawnPos, Quaternion.identity);
					}
					if (dropwhat == 9 || dropwhat == 8) {
						GameObject.Instantiate (drop2, spawnPos, Quaternion.identity);
					}
				}
			}
		}
	}




	void playerTracking() {
		//sets playerpos to the location of the player (will be used to track to)

		if (enemySpeed >= maxV) {
			enemySpeed = maxV;
		}
		playerPos = GameObject.Find ("player").transform.position;

		//calculates the difference from the enenemies x and y to the players x and y
		xDiff = playerPos.x - transform.position.x;
		yDiff = playerPos.y - transform.position.y;
		playerDir = new Vector2 (xDiff, yDiff);
		if (xDiff < 0) {
			transform.localScale = new Vector3 (-1, transform.localScale.y, transform.localScale.z);
		} else if (xDiff > 0) {
			transform.localScale = new Vector3 (1, transform.localScale.y, transform.localScale.z);
		}

		rb.AddForce (playerDir.normalized * enemySpeed);
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "bullet")
		{
			audio.PlayOneShot (impactClip, .6f);
			Vector2 bulSpeed;
			enemyHealth -= enemy.bulDmg;
			bulSpeed = col.GetComponent<Rigidbody2D>().velocity;


			rb.velocity = bulSpeed/5;
			Destroy(col.gameObject);
		}

		if (col.gameObject.name == "melee")
		{
			audio.PlayOneShot (impactClip, .6f);

			if (player.direction == 0) {
				rb.velocity = (transform.up * 4);
			}
			if (player.direction == 1) {
				rb.velocity = (transform.up * -4);
			}
			if (player.direction == 2) {
				rb.velocity = (transform.right * 4);
			}
			if (player.direction == 3) {
				rb.velocity = (transform.right * -4);
			}

			enemyHealth -= enemy.sDmg;
			if (enemyHealth < 0) {
				killedBysword = true;
			}
		}



		if(col.gameObject.name == "player" || col.gameObject.name == "fColl")
		{

			pb2.GetComponent<Rigidbody2D>().velocity = new Vector2((rb.velocity.x * 10), (rb.velocity.y * 10));
			if (tutorial.isTutorial == false) {
				Camera.cameraShake (.04f, .15f);
				player.flashing = true;

			}

			if(pb2.velocity.x > maxSpeed.x)
			{
				pb2.GetComponent<Rigidbody2D>().velocity = new Vector2((maxSpeed.x), (pb2.velocity.y));
			}
			if (pb2.velocity.y > maxSpeed.y)
			{
				pb2.GetComponent<Rigidbody2D>().velocity = new Vector2((pb2.velocity.x), (maxSpeed.y));
			}
			//checking if the player gets too much velocity on the knockback
			if (pb2.velocity.x < -maxSpeed.x)
			{
				pb2.GetComponent<Rigidbody2D>().velocity = new Vector2((-maxSpeed.x), (pb2.velocity.y));
			}
			if (pb2.velocity.y < -maxSpeed.y)
			{
				pb2.GetComponent<Rigidbody2D>().velocity = new Vector2((pb2.velocity.x), (-maxSpeed.y));
			} 

			if (tutorial.isTutorial == false) {
				player.playerHealth = player.playerHealth - enemy.enemyDmg;
			}
		}

	}
}
