using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class camera : MonoBehaviour {

	//HUD
	public Font font;
	public Sprite[] hearts;
	public Sprite[] exp;
	public Sprite[] powerBar;
	public Image heartsUI;
	public Image expUI;
	public Text nextWave;
	public Text levelUp;
	public Text levelNum;
	public Text waveNum;
	public Image[] pluses;
	public Image updateBar1;
	public Image updateBar2;
	public Image updateBar3;
	public Image[] pauseImages;
	public Text[] pauseText;
	public Image fadeB;

	//Tracking
	private Vector3 playerPos;
	private Vector3 newPos;
	private Vector3 velocity = Vector3.one;

	//Camera shake
	public static float shakeAmount;
	public static float shakeTime;

	//waves
	public float curwave = 1f;
	public float curlev = 1f;
	float flashtime = 0f;
	float flashinterval = .25f;
	float totaltime = 0f;
	float totalinterval = 2f;


	float Lflashtime = 0f;
	float Lflashinterval = .25f;
	float Ltotaltime = 0f;
	float Ltotalinterval = 2f;

	float Pflashtime = 0f;
	float Pflashinterval = .75f;

	float disabledSw = 9f;
	float disabledE = 9f;
	float disabledS = 9f;

	int p = 0;

    float timeT = 0f;
    float timeI = 3f;


	void Start () {
		fadeB.CrossFadeAlpha (0f, 3f, false);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (p == 0) {
				player.paused = true;
				for (int i = 0; i < pauseImages.Length; i++){
					pauseImages [i].enabled = true;
					Debug.Log ("hello");
					Debug.Log (player.paused);
				}
				for (int i = 0; i < pauseText.Length; i++){
					pauseText [i].enabled = true;
				}
				p = 1;
			}else if (p == 1) {
				player.paused = false;
				for (int i = 0; i < pauseImages.Length; i++){
					pauseImages [i].enabled = false;
					Debug.Log ("hello");
					Debug.Log (player.paused);
				}
				for (int i = 0; i < pauseText.Length; i++){
					pauseText [i].enabled = false;
				}
				p = 0;
			}
		}

		if (player.paused == false) {

		if (shakeTime > 0)
		{
			Vector2 shakePos = Random.insideUnitCircle *shakeAmount;
			transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
			shakeTime -= Time.deltaTime;
		}
		hud ();
		}
		if (player.playerHealth <= 0) {
			fadeB.CrossFadeAlpha (1f, 3f, false);
            timeT += Time.deltaTime;
            if (timeT >= timeI)
            {
                SceneManager.LoadScene(3);
            }
		}
	}



	private void FixedUpdate()
	{
		if (player.paused == false) {
			if (player.viewEnter == false) {
				if (player.playerHealth > 0) {
					HUDtext ();
					playerPos = GameObject.Find ("player").transform.position;
					newPos = new Vector3 (playerPos.x, playerPos.y, transform.position.z);
					transform.position = Vector3.SmoothDamp (transform.position, newPos, ref velocity, .1f);
				}
			} else if (player.viewEnter == true) {
				HUDtext ();
				Vector3 horizonPos = GameObject.Find ("horizon").transform.position;
				newPos = new Vector3 (horizonPos.x, horizonPos.y, transform.position.z);
				transform.position = Vector3.SmoothDamp (transform.position, newPos, ref velocity, 1f);
			}
		}


	}

	public void cameraShake(float amount, float time) {
		shakeAmount = amount;
		shakeTime = time;

	}

	void hud() {
		string x = curwave.ToString ();
		waveNum.text = x;
		string y = curlev.ToString ();
		levelNum.text = y;

		//healthbar
		if (player.playerHealth > 8) {
			heartsUI.sprite = hearts [0];
		} else if (player.playerHealth > 6) {
			heartsUI.sprite = hearts [1];
		} else if (player.playerHealth > 4) {
			heartsUI.sprite = hearts [2];
		} else if (player.playerHealth > 2) {
			heartsUI.sprite = hearts [3];
		} else if (player.playerHealth > 0) {
			heartsUI.sprite = hearts [4];
		} else if (player.playerHealth <= 0) {
			heartsUI.sprite = hearts [5];
		}

		//xp bar
		if (player.exp == 0) {
			expUI.sprite = exp [0];
	    } else if (player.exp <= .10 * player.maxexp) {
			expUI.sprite = exp [1];
		} else if (player.exp <= .20 * player.maxexp) {
			expUI.sprite = exp [2];
		} else if (player.exp <= .30 * player.maxexp) {
			expUI.sprite = exp [3];
		} else if (player.exp <= .40 * player.maxexp) {
			expUI.sprite = exp [4];
		} else if (player.exp <= .50 * player.maxexp) {
			expUI.sprite = exp [5];
		} else if (player.exp <= .60 * player.maxexp) {
			expUI.sprite = exp [6];
		} else if (player.exp <= .70 * player.maxexp) {
			expUI.sprite = exp [7];
		} else if (player.exp <= .80 * player.maxexp) {
			expUI.sprite = exp [8];
		} else if (player.exp <= .90 * player.maxexp) {
			expUI.sprite = exp [9];
		} else if (player.exp <= player.maxexp) {
			expUI.sprite = exp [10];
		}

		updateBars ();

	}
	void updateBars(){
		if (player.swordlvl == 0) {
			updateBar1.sprite = powerBar [0];
		} else if (player.swordlvl <= 1) {
			updateBar1.sprite = powerBar [1];
		} else if (player.swordlvl <= 2) {
			updateBar1.sprite = powerBar [2];
		} else if (player.swordlvl <= 3) {
			updateBar1.sprite = powerBar [3];
		} else if (player.swordlvl <= 4) {
			updateBar1.sprite = powerBar [4];
		} else if (player.swordlvl <= 5) {
			updateBar1.sprite = powerBar [5];
		} else if (player.swordlvl <= 6) {
			updateBar1.sprite = powerBar [6];
			disabledSw = 0;
			player.canUpSword = false;
		}

		if (player.bullvl == 0) {
			updateBar2.sprite = powerBar [0];
		} else if (player.bullvl <= 1) {
			updateBar2.sprite = powerBar [1];
		} else if (player.bullvl <= 2) {
			updateBar2.sprite = powerBar [2];
		} else if (player.bullvl <= 3) {
			updateBar2.sprite = powerBar [3];
		} else if (player.bullvl <= 4) {
			updateBar2.sprite = powerBar [4];
		} else if (player.bullvl <= 5) {
			updateBar2.sprite = powerBar [5];
		} else if (player.bullvl <= 6) {
			updateBar2.sprite = powerBar [6];
			disabledE = 1;
			player.canUpShoot = false;
		} 

		if (player.speedlvl == 0) {
			updateBar3.sprite = powerBar [0];
		} else if (player.speedlvl <= 1) {
			updateBar3.sprite = powerBar [1];
		} else if (player.speedlvl <= 2) {
			updateBar3.sprite = powerBar [2];
		} else if (player.speedlvl <= 3) {
			updateBar3.sprite = powerBar [3];
		} else if (player.speedlvl <= 4) {
			updateBar3.sprite = powerBar [4];
		} else if (player.speedlvl <= 5) {
			updateBar3.sprite = powerBar [5];
		} else if (player.speedlvl <= 6) {
			updateBar3.sprite = powerBar [6];
			disabledS = 2;
			player.canUpSpeed = false;
		}
	}

	void HUDtext() {
		if (spawner.wave > curwave) {
			totaltime += Time.deltaTime;
			flashtime += Time.deltaTime;

			if (flashtime >= flashinterval) {
				nextWave.enabled = true;
			}
			if (flashtime >= flashinterval * 2) {
				nextWave.enabled = false;
				flashtime = 0;
			}

			if (totaltime >= totalinterval) {
				curwave = spawner.wave;
				player.score += 500;
				nextWave.enabled = false;
				totaltime = 0;
			}
		}

		if (player.level > curlev) {
			Ltotaltime += Time.deltaTime;
			Lflashtime += Time.deltaTime;

			if (Lflashtime >= Lflashinterval) {
				levelUp.enabled = true;
			}
			if (Lflashtime >= Lflashinterval * 2) {
				levelUp.enabled = false;
				Lflashtime = 0;
			}

			if (Ltotaltime >= Ltotalinterval) {
				curlev = player.level;
				levelUp.enabled = false;	
				Ltotaltime = 0;
			}
		}
		if (player.canUpdate == false) {
			for (int i = 0; i < pluses.Length; i++) {
					pluses [i].enabled = false;
			}
		}

		if (player.canUpdate == true) {
			player.updated = false;
			if (player.updated == false) {
				Pflashtime += Time.deltaTime;

				if (Pflashtime >= Pflashinterval) {
					for (int i = 0; i < pluses.Length; i++) {
						if (i == 0 && i != disabledSw) {
							pluses [i].enabled = true;
						}
						if (i == 1 && i != disabledE) {
							if (i == disabledE) {
								pluses [i].enabled = false;
							} else
							pluses [i].enabled = true;
						}
						if (i == 2 && i != disabledS) {
							pluses [i].enabled = true;
						}
						if (i == 3) {
							pluses [i].enabled = true;
						}
					}

				}
				if (Pflashtime >= Pflashinterval * 2) {
					for (int i = 0; i < pluses.Length; i++) {
						if (i == 0 && i != disabledSw) {
							pluses [i].enabled = false;
						}
						if (i == 1 && i != disabledE) {
							pluses [i].enabled = false;
						} 
						if (i == 2 && i != disabledS) {
							pluses [i].enabled = false;
						}
						if (i == 3) {
							pluses [i].enabled = false;
						}
					}

					Pflashtime = 0;
				}
			}
		}


	}

	void OnGUI()
	{
		GUI.skin.label.fontSize = 32;
		GUI.skin.label.font = font;
		GUI.skin.label.alignment = TextAnchor.LowerLeft;
		GUI.Label(new Rect(30, 10, 1000, 50), "Score: " + player.score);
		GUI.Label(new Rect(30, 30, 1000, 50), "Shots Remaining: " + player.shotsRemain);

	}
}
