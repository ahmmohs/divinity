using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 

public class tutorial : MonoBehaviour {

	public Text[] Movement;
	public Text[] Attacking;
	public Text[] Boosting;
	public Text[] Upgrading;
	public Image UI;
	public Image B1;
	public Image B2;
	public Image B3;
	public Image F1;
	public Image F2;
	public Image F3;
	public Image fadeB;
	public Text[] PowerUps;
	public Image[] PP;


	public static float shakeAmountT;
	public static float shakeTimeT;

	float waitTime = 0f;
	float waitInt = 3f;

	float loadTime = 0f;
	float waitLoad = 2f;

	bool mComp = false;
	bool aComp = false;
	bool bComp = false;
	bool pComp = false;

	bool uS = false;
	bool uE = false;
	bool uSp = false;

	float Pflashtime = 0f;
	float Pflashinterval = .75f;

	float Cflashtime = 0f;
	float Cflashinterval = .75f;

	float Rflashtime = 0f;
	float Rflashinterval = .75f;

	float spawnTime = 0f;
	float spawnInt = .5f;

	public GameObject enm;
	public static bool isTutorial = false;
	public static int numEnemies = 0;
	int lastNum = 0;

	bool count = true;
	// Use this for initialization
	void Start () {
		fadeB.CrossFadeAlpha (0f, 3f, false);
		tutorial.isTutorial = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			pComp = true;
			waitTime = 5;
			loadTime = waitLoad;
		}

		if (shakeTimeT > 0)
		{
			Vector2 shakePos = Random.insideUnitCircle *shakeAmountT;
			transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
			shakeTimeT -= Time.deltaTime;
		}

		if (playerT.MovedUp == true && playerT.MovedDown == true && playerT.MovedLeft == true && playerT.MovedRight == true && mComp == false) {
			if (count == true) {
				waitTime += Time.deltaTime;
			}
			if (waitTime >= waitInt) {
				count = false;
				playerT.allShoot = true;
				if (tutorial.numEnemies == 0) {
					
					spawnTime += Time.deltaTime;
					if (spawnTime >= spawnInt) {
						Vector3 spawnPos = new Vector3 (3.35f, 1.95f, 0f);
						GameObject.Instantiate (enm, spawnPos, transform.rotation);
						spawnTime = 0f;
						tutorial.numEnemies++;
						lastNum++;
					}
				} 

				else if (lastNum == 5) {
					/*Vector3 spawnPos = new Vector3 (3.35f, 1.95f, 0f);
					GameObject.Instantiate (enm, spawnPos, transform.rotation);
					lastNum = 1;*/
					waitTime = 0f;
					mComp = true;
				}
				for (int i = 0; i < Movement.Length; i++) {
					Movement [i].enabled = false;
				}
				for (int i = 0; i < Attacking.Length; i++) {
					Attacking [i].enabled = true;
				}
			}
		}

		if (lastNum == 5 && aComp == false) {
			waitTime += Time.deltaTime;
			if (waitTime >= 5f) {
				
				playerT.allBoost = true;
				for (int i = 0; i < Attacking.Length; i++) {
					Attacking [i].enabled = false;
				}
				for (int i = 0; i < Boosting.Length; i++) {
					Boosting [i].enabled = true;
				}
				waitTime = 0f;
				aComp = true;
			}
		}

		if (playerT.hasBoosted == true && bComp == false) {
			waitTime += Time.deltaTime;
			if (waitTime >= 5f) {
				playerT.canUpdate = true;
				for (int i = 0; i < Boosting.Length; i++) {
					Boosting [i].enabled = false;
				}
				for (int i = 0; i < Upgrading.Length; i++) {
					Upgrading [i].enabled = true;
				}
				waitTime = 0f;
				bComp = true;
			}
		}

		if (playerT.canUpdate == true) {
			UI.enabled = true;
			if (uS == false) {
				Pflashtime += Time.deltaTime;

				if (Pflashtime >= Pflashinterval) {
					B1.enabled = true;
				}
					
				if (Pflashtime >= Pflashinterval * 2) {
					B1.enabled = false;
					Pflashtime = 0;
				}
			}

			if (uE == false) {
				Cflashtime += Time.deltaTime;

				if (Cflashtime >= Cflashinterval) {
					B2.enabled = true;
				}

				if (Cflashtime >= Cflashinterval * 2) {
					B2.enabled = false;
					Cflashtime = 0;
				}
			}

			if (uSp == false) {
				Rflashtime += Time.deltaTime;

				if (Rflashtime >= Rflashinterval) {
					B3.enabled = true;
				}

				if (Rflashtime >= Rflashinterval * 2) {
					B3.enabled = false;
					Rflashtime = 0;
				}
			}


			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				uS = true;
				B1.enabled = false;
				F1.enabled = true;
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				uE = true;
				B2.enabled = false;
				F2.enabled = true;
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				uSp = true;
				B3.enabled = false;
				F3.enabled = true;
			}

		}

		if (uS == true && uE == true && uSp == true && pComp == false) {
			waitTime += Time.deltaTime;
			if (waitTime >= waitInt) {
				for (int i = 0; i < Upgrading.Length; i++) {
					Upgrading [i].enabled = false;
				}
				for (int i = 0; i < PowerUps.Length; i++) {
					PowerUps [i].enabled = true;
				}
				for (int i = 0; i < PP.Length; i++) {
					PP [i].enabled = true;
				}
				waitTime = 0f;
				pComp = true;
			}
		}

		if (pComp == true) {
			
			waitTime += Time.deltaTime;
			if (waitTime >= 5) {
				fadeB.CrossFadeAlpha (1f, 1f, false);
				loadTime += Time.deltaTime;
				if (loadTime >= waitLoad) {
					tutorial.isTutorial = false;
					SceneManager.LoadScene (2);
				}
			}
		}

	}



	public void cameraShake(float amount, float time) {
		shakeAmountT = amount;
		shakeTimeT = time;

	}
}
