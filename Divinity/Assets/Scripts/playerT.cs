using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerT : MonoBehaviour {

	public static bool viewEnter = false;
	//movement variables
	public static float directionT = 0;
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
	public static bool flashing = false;

	//upgrading variables
	public static bool canUpdate = false;
	public static bool updated = false;

	//player components
	Rigidbody2D rb;
	Animator anim;
	AudioSource audio;

	//powerups
	public float boostSpeed;
	public static bool speedBoost = false;
	public static bool healthboost = false;
	public static bool smash = false;

	//shooting intervals
	float shoottime = 0f;
	public float shootinterval;

	//dashing interval
	bool dashcooldown = false;
	float dashtime = 0f;
	public float dashinterval = 1f;

	//powerups
	public static float exp;
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



	public static bool MovedLeft = false;
	public static bool MovedRight = false;
	public static bool MovedUp = false;
	public static bool MovedDown = false;
	public static bool allShoot = false;
	public static bool hasAttacked = false;
	public static bool allBoost = false;
	public static bool hasBoosted = false;
	public static bool allowUpgrades = false;

	bool swordCount = true;

	public bool slashing;
	bool slashCount = false;
	float swordtime = 0f;
	float swordInt = .5f;
	GameObject melee;
	BoxCollider2D meleeC;

	float slashTime = 0f;
	float slashInt = .25f;

	// Use this for initialization
	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		melee = GameObject.Find ("melee");
		meleeC = melee.GetComponent<BoxCollider2D> ();
		shoottime = shootinterval;

	}

	// Update is called once per frame
	void Update () {
		if (paused == false) {
			Controller ();
		}
		if (swordCount == true) {
			swordtime++;
		}
	}

	void Controller() {

		//movement
		if (smashing == false) {
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				multiDir++;
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				playerT.MovedUp = true;
				directionT = 0;
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
				playerT.MovedDown = true;
				directionT = 1;
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
				playerT.MovedRight = true;
				directionT = 2;
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
				playerT.MovedLeft = true;
				directionT = 3;
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
			if (allShoot == true) {
				if (slashing == false) {
					if (Input.GetKeyDown (KeyCode.X)) {
						shoottime = shootinterval;
						playerT.hasAttacked = true;
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
							if (directionT == 0) {
								spawnPos = new Vector3 (transform.position.x, transform.position.y + .67f, transform.position.z);
							}
							if (directionT == 1) {
								spawnPos = new Vector3 (transform.position.x, transform.position.y - .67f, transform.position.z);
							}

							if (directionT == 2) {
								spawnPos = new Vector3 (transform.position.x + .1f, transform.position.y + .27f, 0);
							}
							if (directionT == 3) {
								spawnPos = new Vector3 (transform.position.x - .1f, transform.position.y + .27f, 0);
							}
							audio.PlayOneShot (shootSound, .1f);
							GameObject.Instantiate (bullet, spawnPos, Quaternion.identity);
							shoottime = 0f;
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
					Debug.Log ("here");
					slashTime += Time.deltaTime;
				}
				if (slashTime >= slashInt) {
					Debug.Log ("here");
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
				}
			}
			}

			if (allBoost == true) {
				//dashing
				if (Input.GetKeyDown (KeyCode.Space)) {
					if (dashcooldown == false) {
						playerT.hasBoosted = true;
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
		}
		anim.SetBool ("isShooting", shooting);
		anim.SetBool ("isShootwalking", shootwalking);
		anim.SetBool ("isMoving", moving);
		anim.SetBool ("isSmashing", smashing);
		anim.SetBool ("isMovingup", movingup);
		anim.SetBool ("isSlashing", slashing);
	}

		
}
