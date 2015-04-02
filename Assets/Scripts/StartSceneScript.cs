using UnityEngine;
using System.Collections;

public class StartSceneScript : MonoBehaviour {

	public GUISkin guiSkin;
	public Texture2D background, LOGO;
	public string[] AboutTextLines = new string[0];
	
	private int levelToPlay = 1;
	private string clicked = "", MessageDisplayOnAbout = "About \n ";

	private void Start()
	{
		for (int x = 0; x < AboutTextLines.Length;x++ )
		{
			MessageDisplayOnAbout += AboutTextLines[x] + " \n ";
		}
		MessageDisplayOnAbout += "Maze By Myo YAY\n";
		MessageDisplayOnAbout += "Press Esc To Go Back\n";

	}
	
	private void OnGUI()
	{
		if (background != null)
			GUI.DrawTexture(new Rect(0,0,Screen.width , Screen.height),background);
		if (LOGO != null && clicked != "about")
			GUI.DrawTexture(new Rect((Screen.width / 2) - 100, 30, 200, 200), LOGO);
		
		GUI.skin = guiSkin;

		if (GUI.Button (new Rect ((Screen.width / 10), (float) 0.25 * Screen.height, (Screen.width / 6), (Screen.width / 20)), "Level 1"))
		{
			levelToPlay  = 1;
		}
		if (GUI.Button (new Rect ((Screen.width / 3), (float) 0.25 * Screen.height, (Screen.width / 6), (Screen.width / 20)), "Level 2"))
		{
			levelToPlay = 2;
		}
		string birdEyeStats = "Turn Bird-Eye-View ON";
		if (CharactorController.can_switch == true)
		{
			birdEyeStats = "Turn Bird-Eye-View OFF";
		}
		if (GUI.Button (new Rect ((Screen.width / 10), (float) 0.40 * Screen.height, (float) 0.4 * Screen.width, (Screen.width / 20)), birdEyeStats))
		{
			CharactorController.can_switch = !CharactorController.can_switch;
		}
		string displayStr = "Level " + levelToPlay + " selected with Bird-Eye-view ON";
		if (CharactorController.can_switch == false)
		{
			displayStr = "Level " + levelToPlay + " selected with Bird-Eye-view OFF";
		}
		GUI.TextField(new Rect((Screen.width / 10), (float) 0.55 * Screen.height, (float) 0.4 * Screen.width, (Screen.width / 20)), displayStr);
		if (GUI.Button (new Rect ((Screen.width / 10), (float) 0.70 * Screen.height, (float) 0.4 * Screen.width, (Screen.width / 20)), "Start!"))
		{
			LevelOneScript.levelToLoad = levelToPlay;
			Application.LoadLevel(3);
		}
		if (GUI.Button (new Rect ((7 * Screen.width / 10), (float) 0.70 * Screen.height, (Screen.width / 6), (Screen.width / 20)), "Quit"))
		{
			Application.Quit();
		}

	}

}
