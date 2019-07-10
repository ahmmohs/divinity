using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;


public class guiEnd : MonoBehaviour {
	private string message = "hi";
	private string newName = "";
	private string display = "";
	private int score;
    string localhighScores = "";
    private bool alreadyChecked = false;
	private bool highScore = false;
	//	private TouchScreenKeyboard keyboard;
	private HighScoreData hsd;

	void OnGUI(){
	//	GUI.BeginGroup (new Rect (Screen.width / 2 - 149, Screen.height / 2 - 149, 300, 300));
        GUI.BeginGroup(new Rect(10, 10, Screen.width-20, Screen.height - 20));
        GUI.skin.box.alignment = TextAnchor.UpperLeft;
        GUI.color = Color.white;
      //  GUI.skin.box.alignment = TextAnchor.UpperLeft;
        GUI.skin.box.fontSize = 14;

     //   GUI.Label(new Rect(Screen.width / 2 - 100, 20, 200, 170), "TOP SCORES\n\n" + localhighScores);
        GUI.Box(new Rect(Screen.width / 2 - 50, 40, 100, 170), "TOP SCORES\n\n" + localhighScores);
        GUI.skin.box.alignment = TextAnchor.UpperCenter;
        GUI.skin.box.fontSize = 20;
        GUI.color = Color.yellow;
        display = "Game Over\nScore: " + score + "\n" + message + "\n" + newName;
		GUI.Box (new Rect (Screen.width/2 - 150, 240,300,260), display);
        GUI.color = Color.white;
        //int labelWidth = 200;
       // if (GUI.Button(new Rect(Screen.width/2 - 120, 460,100,20), "PLAY AGAIN")) {
            if (GUI.Button(new Rect(Screen.width / 2 - 220, 460, 200, 30), "PLAY AGAIN"))
            {

                //Screen.width / 2 - 100, 90, 200, 30
                if (hsd.checkScore_HighIsGood(score)) {		
				hsd.insertHighScore_HighIsGood(score, newName);
				hsd.writeFile();
			}
		//	guiMain.score = 0;        //RESET MAIN SCORE
		//	Application.LoadLevel(1);
            SceneManager.LoadScene(0);
			player.score = 0;
			player.playerHealth = 10;
			enemy.bulDmg = 1;
			enemy.sDmg = 2;
			spawner.wave = 1;
			subspawners.wave = 1;
			player.exp = 0;
			player.maxexp = 1000;
			player.level = 1;
			player.paused = false;
			player.swordlvl = 0;
			player.bullvl = 0;
			player.speedlvl = 0;
			spawner.enmHealth = 1;
			spawner.enmSpeed = 1;

		}
        //	if(GUI.Button(new Rect(Screen.width/2 +20, 460,100,20), "EXIT GAME")) {
        if (GUI.Button(new Rect(Screen.width / 2 + 20, 460, 200, 30), "EXIT GAME"))
        {
            if (hsd.checkScore_HighIsGood(score)) {
				hsd.insertHighScore_HighIsGood(score, newName);
				hsd.writeFile();
			}
			Application.Quit();		
		}

     //   GUI.Box(new Rect(Screen.width/2 - 70, 10, 140, 200), "TOP 5 LOCALLY\n\n" + localhighScores);  //FROM FILE FOR CC
        GUI.EndGroup ();	
	}
	
	void Start () {
			score = (int) player.score;
			hsd = new HighScoreData ("Divinity");
        localhighScores = hsd.getScoreList();
        if (hsd.checkScore_HighIsGood (score)) {
			highScore = true;
				}			
		}
	void Update () {
		if(highScore){

			message = "YOU HAVE A HIGH SCORE!\nEnter your initials";
			if (Application.platform == RuntimePlatform.Android) {
	//			keyboard = TouchScreenKeyboard.Open ("", TouchScreenKeyboardType.Default);						
			} 
			else {				
				if (Input.GetKey (KeyCode.Delete) || Input.GetKey (KeyCode.Backspace)) {					
					newName = "";					
				} 
				else {
					if (newName.Length < 3 && !Input.GetKey (KeyCode.Return))
						newName += Input.inputString;
				} 								
			} 
		}
		else {
			message = "GAME OVER";	
			
		}
	//	if (keyboard != null) {
	//		newName = keyboard.text;
	//	};
	}

	public string pathForDocumentsFile( string filename ) 
	{ 
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			string path = Application.dataPath.Substring( 0, Application.dataPath.Length - 5 );
			path = path.Substring( 0, path.LastIndexOf( '/' ) );
			return Path.Combine( Path.Combine( path, "Documents" ), filename );
		}
		
		else if(Application.platform == RuntimePlatform.Android)
		{
			string path = Application.persistentDataPath;	
			path = path.Substring(0, path.LastIndexOf( '/' ) );	
			return Path.Combine (path, filename);
		}	
		
		else 
		{
			string path = Application.dataPath;	
			path = path.Substring(0, path.LastIndexOf( '/' ) );
			return Path.Combine (path, filename);
		}
	}




}
