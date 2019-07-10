using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

	public GameObject prefab;
	public GameObject prefab1;
	public GameObject prefab2;
	public float interval;  //Is the frequency of spawning - currently every approx 1 seconds.
	float time = 0.0f; //Keeps the time. Spawns when time gets to interval. Then its reset to 0.
	float fTime = 0f;
	float fInterval = 10;
	float bTime = 0f;
	float bInterval = 7;

	public static float wave = 1f;
	float wavetime = 0f;
	float waveInterval = 20f;
	public float wavepause = 5f;
	bool changewave = false;
	bool changed = false;

	public float spawnerRad;
	bool spawnFast = false;
	bool spawnBulky = false;

	// Update is called once per frame
	void Update () {
		if (player.paused == false) {
			wavetime += Time.deltaTime;
			checkwaves ();

			if (changewave == false) {
				time += Time.deltaTime;
				if (spawnFast == true) {
					fTime += Time.deltaTime;
				}
				if (spawnBulky == true) {
					bTime += Time.deltaTime;
				}
				onSpawn ();
			}
		}
	}

	void onSpawn() {
		
		if (time >= interval) {
			Vector2 spawnRad = Random.insideUnitCircle * spawnerRad;
			Vector3 spawnPos = new Vector3 (transform.position.x + spawnRad.x, transform.position.y + spawnRad.y, transform.position.z);
			GameObject.Instantiate(prefab, spawnPos, transform.rotation);
			time = 0f;
		}

		if (fTime >= fInterval) {
			Vector2 fspawnRad = Random.insideUnitCircle * spawnerRad;
			Vector3 fspawnPos = new Vector3 (transform.position.x + fspawnRad.x, transform.position.y + fspawnRad.y, transform.position.z);
			GameObject.Instantiate(prefab1, fspawnPos, transform.rotation);
			fTime = 0f;
		}

		if (bTime >= bInterval) {
			Vector2 bspawnRad = Random.insideUnitCircle * spawnerRad;
			Vector3 bspawnPos = new Vector3 (transform.position.x + bspawnRad.x, transform.position.y + bspawnRad.y, transform.position.z);
			GameObject.Instantiate(prefab2, bspawnPos, transform.rotation);
			bTime = 0f;
		}
	}

	public static float enmHealth = 1;
	public static float enmSpeed = 1;

	void checkwaves() {
		if (wavetime >= waveInterval) {
			changewave = true;

			if (player.numEnemies == 0) {
				wavepause -= Time.deltaTime;
				if (wavepause <= 2) {
					if (changed == false) {
							spawner.wave++;
							changed = true;
					}
				}
				if (wavepause <= 0) {
					changewave = false;
					interval *= .95f;
					fInterval *= .95f;
					bInterval *= .95f;
					wavetime = 0f;
					waveInterval *= 1.05f;
					wavepause = 10f;
					changed = false;
					spawner.enmSpeed *= 1.05f;
					spawner.enmHealth *= 1.25f;
				}
			}
		}

		if (spawner.wave >= 5) {
			spawnFast = true;
		}

		if (spawner.wave >= 7) {
			spawnBulky = true;
		}
	}
}
