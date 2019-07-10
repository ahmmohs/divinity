using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	public static bool viewEnter = false;
	public static int shotsRemain = 50;
	int maxshots = 50;
	//movement variables
	public static float direction = 0;
	public int multiDir = 0;
	public float playerSpeed;
	public float minSpeed;

	//health
	public static float playerHealth = 10f;
	public float minHealth;
	public float maxHealth;

	//animation variables
	public bool moving;
	public bool movingup;
	public bool shooting;
	public bool smashing;
	public bool shootwalking;
	public bool slashing;
	public static bool flashing = false;
	float flashtime = 0f;
	float maxflash = 1.75f;

	//upgrading variables
	public static bool canUpdate = false;
	public static bool updated = false;

	//player components
	Rigidbody2D rb;
	Animator anim;
	AudioSource audio;

	//powerups
	public static bool powerup = false;
	float poweruptime = 0f;
	float powerupinterval = 10f;
	public float boostSpeed;
	public static bool speedBoost = false;
	public static bool healthboost = false;
	public static bool smash = false;
	float smashWait = 0f;

	//shooting intervals
	float shoottime = 0f;
	public float shootinterval;

	//dashing interval
	bool dashcooldown = false;
	float dashtime = 0f;
	public float dashinterval = 1f;

	//powerups
	public static float exp = 0;
	public static float maxexp = 1000;
	public float upgradesRem = 0f;
	public static float level = 1;
	public static float score;
	public static float numEnemies;

	public static bool paused = false;
	public AudioClip smashSound;
	public AudioClip shootSound;
	public AudioClip drawSound;
	//prefabs
	public GameObject bullet;

	GameObject camObj;
	camera Camera;

	GameObject melee;
	BoxCollider2D meleeC;

	bool swordCount = true;
	bool slashCount = false;
	float swordtime = 0f;
	float swordInt = .5f;

	float slashTime = 0f;
	float slashInt = .25f;

	float shotRTime = 0;
	float shotRInt = 3f;

	// Use this for initialization
	void Start () {


		rb = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		audio = gameObject.GetComponent<AudioSource> ();
		shoottime = shootinterval;
		camObj = GameObject.Find ("Main Camera");
		melee = GameObject.Find ("melee");
		meleeC = melee.GetComponent<BoxCollider2D> ();
		Camera = camObj.GetComponent<camera> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (player.shotsRemain != maxshots) {
			shotRTime += Time.deltaTime;
			if (shotRTime >= shotRInt) {
				player.shotsRemain++;
				shotRTime = 0f;
			}
		}
		if (player.shotsRemain >= maxshots) {
			player.shotsRemain = maxshots;
		}
		if (paused == false) {
			Controller ();
			Powerup ();
			charFlash ();
			death ();
			levelup ();
		}
		if (swordCount == true) {
			swordtime++;
		}
	}

	void Controller() {
		
		//movement
		if (smashing == false) {
			if (slashing == false) {
				if (Input.GetKeyDown (KeyCode.UpArrow)) {
					multiDir++;
				}
				if (Input.GetKey (KeyCode.UpArrow)) {
					direction = 0;
					rb.AddForce (transform.up * playerSpeed);
					movingup = true;
				}
				if (Input.GetKeyUp (KeyCode.UpArrow)) {
					multiDir = 0;
					//rb.velocity = new Vector2 (0f, 0f);
				}
				if (Input.GetKeyDown (KeyCode.DownArrow)) {
					multiDir++;
				}
				if (Input.GetKey (KeyCode.DownArrow)) {
					direction = 1;
					rb.AddForce (transform.up * -playerSpeed);
					movingup = false;
					moving = true;
				}
				if (Input.GetKeyUp (KeyCode.DownArrow)) {
					moving = false;
					multiDir = 0;
					//rb.velocity = new Vector2 (0f, 0f);
				}
				if (Input.GetKeyDown (KeyCode.RightArrow)) {
					multiDir++;
				}
				if (Input.GetKey (KeyCode.RightArrow)) {
					direction = 2;
					rb.AddForce (transform.right * playerSpeed); 
					movingup = false;
					moving = true;
					transform.localScale = new Vector2 (1f, 1f);
				}
				if (Input.GetKeyUp (KeyCode.RightArrow)) {
					moving = false;
					multiDir = 0;
					//rb.velocity = new Vector2 (0f, 0f);
				}
				if (Input.GetKeyDown (KeyCode.LeftArrow)) {
					multiDir++;
				}
				if (Input.GetKey (KeyCode.LeftArrow)) {
					direction = 3;
					rb.AddForce (transform.right * -playerSpeed);
					movingup = false;
					moving = true;
					transform.localScale = new Vector2 (-1f, 1f);
				}
				if (Input.GetKeyUp (KeyCode.LeftArrow)) {
					moving = false;
					multiDir = 0;
					//rb.velocity = new Vector2 (0f, 0f);
				}


				//shooting
				if (shotsRemain > 0) {
					if (Input.GetKeyDown (KeyCode.X)) {
						shoottime = shootinterval;
					}
					if (Input.GetKey (KeyCode.X)) {
						shoottime += Time.deltaTime;
						shooting = true;

						/*if (firstshot == true) {
					if (moving == true && shooting == true) {
						shootwalking = true;
					} else if (movingup == true && shooting == true) {
						shootwalking = false;
						shooting = false;
					} else
						shootwalking = false;
					Vector3 spawnPos = new Vector3 (0f, 0f, 0f);
					if (direction == 0) {
						spawnPos = new Vector3 (transform.position.x, transform.position.y + .67f, transform.position.z);
					}
					if (direction == 1) {
						spawnPos = new Vector3 (transform.position.x, transform.position.y - .67f, transform.position.z);
					}

					if (direction == 2) {
						spawnPos = new Vector3 (transform.position.x + .1f, transform.position.y + .27f, 0);
					}
					if (direction == 3) {
						spawnPos = new Vector3 (transform.position.x - .1f, transform.position.y + .27f, 0);
					}
					GameObject.Instantiate (bullet, spawnPos, Quaternion.identity);
					firstshot = false;
				}*/

						if (shoottime >= shootinterval) {
							if (moving == true && shooting == true) {
								shootwalking = true;
							} else if (movingup == true && shooting == true) {
								shootwalking = false;
								shooting = false;
							} else
								shootwalking = false;
							Vector3 spawnPos = new Vector3 (0f, 0f, 0f);
							if (direction == 0) {
								spawnPos = new Vector3 (transform.position.x, transform.position.y + .67f, transform.position.z);
							}
							if (direction == 1) {
								spawnPos = new Vector3 (transform.position.x, transform.position.y - .67f, transform.position.z);
							}

							if (direction == 2) {
								spawnPos = new Vector3 (transform.position.x + .1f, transform.position.y + .27f, 0);
							}
							if (direction == 3) {
								spawnPos = new Vector3 (transform.position.x - .1f, transform.position.y + .27f, 0);
							}
							audio.PlayOneShot (shootSound, .1f);
							GameObject.Instantiate (bullet, spawnPos, Quaternion.identity);
							player.shotsRemain--;
							shoottime = 0f;
						}
					}
				} 

				if (Input.GetKeyUp (KeyCode.X)) {
					shooting = false;
					shootwalking = false;
				}
			}

			if (swordtime >= swordInt) {
				swordCount = false;
				if (slashCount == true) {
					slashTime += Time.deltaTime;
				}
				if (slashTime >= slashInt) {
					slashing = false;
					swordtime = 0;
					slashTime = 0;
					swordCount = true;
					slashCount = false;
					meleeC.enabled = false;
				}
				if (Input.GetKeyDown (KeyCode.Z)) {
					slashing = true;
					slashCount = true;
					meleeC.enabled = true;
					Camera.cameraShake (.025f, .25f);
				}
			}


			//dashing
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (dashcooldown == false) {
					if (multiDir == 2) {
						rb.AddForce (transform.up * rb.velocity.y * 100);
						rb.AddForce (transform.right * rb.velocity.x * 100);
					} else {
						rb.AddForce (transform.up * rb.velocity.y * 200);
						rb.AddForce (transform.right * rb.velocity.x * 200);
					}
					dashcooldown = true;
				}
			}

			if (dashcooldown == true) {
				dashtime += Time.deltaTime;
				if (dashtime >= dashinterval) {
					dashtime = 0f;
					dashcooldown = false;
				}
			}
		}
		anim.SetBool ("isShooting", shooting);
		anim.SetBool ("isShootwalking", shootwalking);
		anim.SetBool ("isMoving", moving);
		anim.SetBool ("isSmashing", smashing);
		anim.SetBool ("isMovingup", movingup);
		anim.SetBool ("isSlashing", slashing);
	}

	void death()
	{
		if(player.playerHealth <= 0)
		{
			Destroy(gameObject);
		}
	}

	void Powerup() {
		if (powerup == true) {

			/*float regentime = 0f;
			float regeninterval = .5f;
			regentime += Time.deltaTime;
			if (regentime >= regeninterval) {
				regentime = 0f;
				player.playerHealth += 1;
				if (player.playerHealth >= maxHealth) {
					player.playerHealth = maxHealth;
				}
			} */

			if (healthboost == true) {
				player.playerHealth = maxHealth;
			}

			if (speedBoost == true) {
				playerSpeed = boostSpeed;
			}

			poweruptime += Time.deltaTime;
			if (poweruptime >= powerupinterval) {
				powerup = false;
				poweruptime = 0;
				playerSpeed = minSpeed;
			}
		}



		if (smash == true) {
			if (smashWait == 0) {
				audio.PlayOneShot (drawSound, .2f);
			}
			smashing = true;
			moving = false;
			movingup = false;
			shooting = false;
			shootwalking = false;
			smashWait += Time.deltaTime;
			if (smashWait >= 1.15f) {
				audio.PlayOneShot (smashSound, 1f);
				GameObject[] gameObjects;
				gameObjects = GameObject.FindGameObjectsWithTag ("enemy");
				Camera.cameraShake (.25f, .5f);
				for (var i = 0; i < gameObjects.Length; i++) {
					Destroy (gameObjects [i]);
					player.score += 125;
					player.exp += 25;
				}
				smashing = false;
				smashWait = 0f;
				smash = false;
				player.numEnemies = 0;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "viewDetector") {
			player.viewEnter = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == "viewDetector") {
			player.viewEnter = false;
		}
	}

	public void charFlash(){

		if (flashing == true) {
			flashtime += Time.deltaTime;
			int enemyLayer = LayerMask.NameToLayer ("Enemy");
			int playerLayer = LayerMask.NameToLayer ("Player");
			Physics2D.IgnoreLayerCollision (enemyLayer, playerLayer);
			anim.SetLayerWeight (1, 1);
			if (flashtime >= maxflash) {
				flashtime = 0;
				Physics2D.IgnoreLayerCollision (enemyLayer, playerLayer, false);
				anim.SetLayerWeight (1, 0);
				flashing = false;
			}

		}
	}

	public static int swordlvl = 0;
	public static int bullvl= 0;
	public static int speedlvl= 0;

	public static bool canUpSword = true;
	public static bool canUpShoot = true;
	public static bool canUpSpeed = true;
	void levelup() {
		if (exp >= maxexp) {
			maxexp *= 1.25f;
			level++;
			maxshots += 15;
			exp = 0;
			upgradesRem++;
			player.canUpdate = true;
		}

		if (player.canUpdate == true) {
			if (player.canUpSword == true) {
				if (Input.GetKeyDown (KeyCode.Alpha1)) {
					enemy.sDmg *= 1.5f;
					if (upgradesRem == 1f) {
						player.updated = true;
						player.canUpdate = false;
					}
					player.swordlvl++;
					upgradesRem--;
				}
			}

			if (player.canUpShoot == true) {
				if (Input.GetKeyDown (KeyCode.Alpha2)) {
					enemy.bulDmg *= 1.75f;
					if (upgradesRem == 1f) {
						player.updated = true;
						player.canUpdate = false;
					}

					player.bullvl++;

					upgradesRem--;
				}
			}

			if (player.canUpSpeed == true) {
				if (Input.GetKeyDown (KeyCode.Alpha3)) {
					if (upgradesRem == 1f) {
						player.updated = true;
						player.canUpdate = false;
					}
					player.speedlvl++;
					minSpeed += 2;
					playerSpeed = minSpeed;
					upgradesRem--;
				}
			}
		}
	}

}
