using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menu : MonoBehaviour {

    public Font font;
	int x = 0;
	float y = 0;
    string highScores = "";

    // Use this for initialization
    void Start () {
        HighScoreData hds = new HighScoreData("Divinity");
        highScores = hds.getScoreList();
    }

    void OnGUI()
    {
        //  GUI.skin.box.alignment = TextAnchor.MiddleCenter;

        GUI.BeginGroup(new Rect(-90, 180, Screen.width - 20, Screen.height - 20));

        // All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

        // We'll make a box so you can see where the group is on-screen.
        GUI.skin.box.alignment = TextAnchor.UpperCenter;
        GUI.color = Color.white;
        GUI.skin.label.alignment = TextAnchor.UpperCenter;
        GUI.skin.label.fontSize = 50;
        GUI.skin.label.font = font;

        GUI.Label(new Rect(Screen.width / 2 - 60, 340, 300, 300), highScores);
        GUI.color = Color.white;
        GUI.skin.box.fontSize = 12;
        /*
        if (GUI.Button(new Rect(Screen.width / 2 - 100, 170, 200, 30), "Start"))
        {
            //Application.LoadLevel("main");
            SceneManager.LoadScene("main");
        }
        */
        // End the group we started above. This is very important to remember!
        GUI.EndGroup();

    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (x == 0) {
				gameObject.transform.position = new Vector3 (0f, -.611f, -8f);
				x++;
			}else if (x == 1) {
				gameObject.transform.position = new Vector3 (0f, -.297f, -8f);
				x=0;
			}
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (x == 0) {
				gameObject.transform.position = new Vector3 (0f, -.611f, -8f);
				x++;
			}else if (x == 1) {
				gameObject.transform.position = new Vector3 (0f, -.297f, -8f);
				x=0;
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (-.297f == transform.position.y) {
				SceneManager.LoadScene (1);
			}
			if (-.611f == transform.position.y) {
				Application.Quit ();
			}
		}

		gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + (Mathf.Sin(y)*.001f), gameObject.transform.localScale.y + (Mathf.Sin(y)*.001f), gameObject.transform.localScale.z + (Mathf.Sin(y)*.001f));
		y += .1f;
	}
}
